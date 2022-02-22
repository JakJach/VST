
#!/usr/bin/env python
# coding: utf-8

# # MGR

# ## 1. Data treatment

import numpy as np
import os
import librosa
from utils import Helper


# Frequency band limit and center frequencies were taken as suggested on website:
# https://www.engineeringtoolbox.com/octave-bands-frequency-limits-d_1602.html

# In[2]:


SAMPLING_RATE = 44100

BAND_LIMITS = [(11,22),(22,44),(44,88),(88,177),(177,355),(355,710),(710,1420),(1420,2840),(2840,5680),(5680,11360),
               (11360,22720)]


# Directories where data is stored:

# In[4]:


AUDIO_DIR = r'D:\MGR\fma\fma_small'
METADATA_DIR = r'D:\MGR\fma\fma_metadata'


# Import dataset metadata:

# In[6]:


tracks = load(METADATA_DIR + '\\tracks.csv')
features = load(METADATA_DIR + '\\features.csv')


# Select metadata of FMA small:

# In[7]:


subset = tracks.index[tracks['set', 'subset'] <= 'small']
assert subset.isin(tracks.index).all()
assert subset.isin(features.index).all()


# Select only those rows, that are indicated by subset:

# In[8]:


tracks = tracks.loc[subset]


# Join Multindex into one index:

# In[9]:


tracks.columns = [' '.join(col).strip() for col in tracks.columns.values]


# Select only columns important for the task:

# In[10]:


tracks = tracks[['artist name', 'track title', 'track genre_top']]


# Get list of all possible genres in dataset and generate dictionary to map them:

# In[11]:


genres = tracks['track genre_top'].unique()
for genre in genres:
    print(genre)


# In[12]:


tracks['genre'] = tracks['track genre_top'].map(
    {'Hip-Hop':1, 'Pop':2, 'Folk':3, 'Experimental':4, 'Rock':5, 'International':6, 'Electronic':7, 
     'Instrumental':8}).astype(int)
    
print(tracks.head())


# There were loading problems with some files, so they have been removed:

# In[13]:


tracks = tracks.drop(index=99134)
tracks = tracks.drop(index=108925)
tracks = tracks.drop(index=133297)


# Iterate through audio files:

# In[15]:


tempos = []
filenames = []
bandpowers = []
powers = []
MFCCs = []
for subdir, dirs, files in os.walk(AUDIO_DIR):
    for file in files:
        if not file.startswith("checksums") and not file.startswith("README"):
            filename = os.path.join(subdir, file)
            filenames.append(filename)
            #print(filename)

            #Load audio file
            y, sr = librosa.load(filename, duration=15, sr=SAMPLING_RATE)

            # Calculate and save tempo
            tempo = librosa.beat.tempo(y=y, sr=sr)
            tempos.append(tempo[0])
            #print(tempo)
            
            mfcc = librosa.feature.mfcc(y=y, sr=sr, n_mfcc=12)
            MFCCs.append(np.mean(mfcc, axis=1))
            
            #Calculate power of the track
            powers.append(Helper.bandpower(y,sr,0,22720))

            # Calculate each band power and add them to df
            bandpower = []
            for limits in BAND_LIMITS:
                bandpower.append(Helper.bandpower(y,sr,limits[0],limits[1]))

            bandpowers.append(bandpower)
            #print(bandpower)


# Add filenames to dataframe:

# In[17]:


tracks['filename'] = filenames
tracks['tempo'] = tempos
tracks['power'] = powers


# Split MFCCs into seperate columns:

# In[18]:


tracks[['mfcc_1','mfcc_2','mfcc_3','mfcc_4','mfcc_5','mfcc_6','mfcc_7','mfcc_8','mfcc_9','mfcc_10','mfcc_11','mfcc_12']] = MFCCs


# Split bandpowers into seperate columns:

# In[19]:


tracks[['band_1','band_2','band_3','band_4','band_5','band_6','band_7','band_8','band_9','band_10','band_11']] = bandpowers


# Export data to csv file:

# In[20]:


print(tracks.head())


# In[21]:


tracks.to_csv('D:\\MGR\\fma\\tracks.csv', header=True)