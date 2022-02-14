#!/usr/bin/env python
# coding: utf-8

# # MGR

# In[1]:


import numpy as np
import matplotlib.pyplot as plt


# Frequency band limit and center frequencies were taken as suggested on website:
# https://www.engineeringtoolbox.com/octave-bands-frequency-limits-d_1602.html

# In[2]:


SAMPLING_RATE = 44100

BAND_LIMITS = [(11,22),(22,44),(44,88),(88,177),(177,355),(355,710),(710,1420),(1420,2840),(2840,5680),(5680,11360),
               (11360,22720)]

BAND_CENTERS = [16,31.5,63,125,250,500,1000,2000,4000,8000,16000]


# Define Helper class:

# In[3]:


class Helper:
    def bandpower(x, fs, fmin, fmax):
        # Extracting the length and the half-length of the signal to input to the foruier transform
        sig_length = len(x)
        half_length = np.ceil((sig_length + 1) / 2.0).astype(int)

        # We will now be using the Fourier Transform to form the frequency domain of the signal
        signal_freq = np.fft.fft(x)

        # Normalize the frequency domain and square it
        signal_freq = abs(signal_freq[0:half_length]) / sig_length
        signal_freq **= 2
        transform_len = len(signal_freq)

        # The Fourier transformed signal now needs to be adjusted for both even and odd cases
        if sig_length % 2:
          signal_freq[1:transform_len] *= 2
        else:
          signal_freq[1:transform_len-1] *= 2

        # Extract the signal's strength in decibels (dB)
        exp_signal = 10 * np.log10(signal_freq)
        x_axis = np.arange(0, half_length, 1) * (fs / sig_length)

        idx_min = np.argmax(x_axis > fmin) - 1
        idx_max = np.argmax(x_axis > fmax) - 1

        return np.average(exp_signal[idx_min:idx_max])
    
    def plot_corr(df, size=16):
        corr = df.corr()
        fig, ax = plt.subplots(figsize=(size,size))
        ax.matshow(corr)
        plt.xticks(range(len(corr.columns)), corr.columns)
        plt.yticks(range(len(corr.columns)), corr.columns)


# Packages neccessary for data treatment:

# In[4]:


import os
import librosa


# Directories where data is stored:

# In[5]:


AUDIO_DIR = 'D:\\MGR\\fma\\fma_small'
METADATA_DIR = 'D:\\MGR\\fma\\fma_metadata'


# Import dataset metadata:

# In[6]:


get_ipython().run_line_magic('run', '"D:\\MGR\\fma\\utils.py"')


# In[7]:


tracks = load(METADATA_DIR + '\\tracks.csv')
features = load(METADATA_DIR + '\\features.csv')


# Select metadata of FMA small:

# In[8]:


subset = tracks.index[tracks['set', 'subset'] <= 'small']
assert subset.isin(tracks.index).all()
assert subset.isin(features.index).all()


# Select only those rows, that are indicated by subset:

# In[9]:


tracks = tracks.loc[subset]


# Join Multindex into one index:

# In[10]:


tracks.columns = [' '.join(col).strip() for col in tracks.columns.values]


# Select only columns important for the task:

# In[11]:


tracks = tracks[['artist name', 'track title', 'track genre_top']]


# Get list of all possible genres in dataset and generate dictionary to map them:

# In[12]:


genres = tracks['track genre_top'].unique()
for genre in genres:
    print(genre)


# In[13]:


tracks['track genre_top'] = tracks['track genre_top'].map(
    {'Hip-Hop':1, 'Pop':2, 'Folk':3, 'Experimental':4, 'Rock':5, 'International':6, 'Electronic':7, 
     'Instrumental':8}).astype(int)
    
print(tracks.head())


# There were loading problems with some files, so they have been removed:

# In[14]:


tracks = tracks.drop(index=99134)
tracks = tracks.drop(index=108925)
tracks = tracks.drop(index=133297)


# Iterate through audio files:

# In[15]:


tempos = []
filenames = []
bandpowers = []
for subdir, dirs, files in os.walk(AUDIO_DIR):
    for file in files:
        if not file.startswith("checksums") and not file.startswith("README"):
            filename = os.path.join(subdir, file)
            filenames.append(filename)
            #print(filename)

            #Load audio file
            y, sr = librosa.load(filename, duration=3, sr=SAMPLING_RATE)

            # Calculate and save tempo
            tempo = librosa.beat.tempo(y=y, sr=sr)
            tempos.append(tempo[0])
            #print(tempo)

            # Calculate each band power and add them to df
            bandpower = [Helper.bandpower(y,sr,11,22),Helper.bandpower(y,sr,22,44),Helper.bandpower(y,sr,44,88),
                         Helper.bandpower(y,sr,88,177),Helper.bandpower(y,sr,177,355),Helper.bandpower(y,sr,355,710),
                         Helper.bandpower(y,sr,710,1420),Helper.bandpower(y,sr,1420,2840),Helper.bandpower(y,sr,2840,5680),
                         Helper.bandpower(y,sr,5680,11360),Helper.bandpower(y,sr,11360,22720)]
            bandpowers.append(bandpower)
            #print(bandpower)


# Add filenames to dataframe:

# In[16]:


tracks['track filename'] = filenames
tracks['track tempo'] = tempos


# Split bandpowers into seperate columns:

# In[17]:


tracks[['track band_1','track band_2','track band_3','track band_4','track band_5','track band_6',
        'track band_7','track band_8','track band_9','track band_10','track band_11']] = bandpowers


# Export data to csv file:

# In[18]:


tracks.to_csv('D:\\MGR\\fma\\tracks.csv', header=True)


# In[19]:


print(tracks.head())


# Check correlation:

# In[20]:


Helper.plot_corr(tracks)


# Shuffle data:

# In[21]:


from sklearn.utils import shuffle
tracks = shuffle(tracks)


# Scale and fit data:

# In[22]:


def clean_dataset(df):
    assert isinstance(df, pd.DataFrame), "df needs to be a pd.DataFrame"
    df.dropna(inplace=True)
    indices_to_keep = ~df.isin([np.nan, np.inf, -np.inf]).any(1)
    return df[indices_to_keep].astype(np.float64)


# In[23]:


from sklearn.preprocessing import MinMaxScaler

data = tracks[['track genre_top','track tempo', 'track band_1','track band_2','track band_3','track band_4','track band_5','track band_6',
        'track band_7','track band_8','track band_9','track band_10','track band_11']]

data = clean_dataset(data)

scaler = MinMaxScaler()
scaler.fit_transform(data)


# Select input and result sets from dataframe:

# In[24]:


y = data[['track band_1','track band_2','track band_3','track band_4','track band_5','track band_6',
        'track band_7','track band_8','track band_9','track band_10','track band_11']]
X = data[['track genre_top','track tempo', 'track band_1','track band_2','track band_3','track band_4','track band_5',
          'track band_6','track band_7','track band_8','track band_9','track band_10','track band_11']]


# Split datasets to train (70%) and test (30%):

# In[25]:


from sklearn.model_selection import train_test_split

X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)


# Check shapes of train and test datasets:

# In[26]:


print(X_train.shape)
print(y_train.shape)
print(X_test.shape)
print(y_test.shape)


# Create model:

# In[176]:


from keras.models import Sequential
from keras.layers import Dense

model = Sequential()

model.add(Dense(units=26, input_dim=13, kernel_initializer='normal', activation='relu'))

model.add(Dense(units=52, kernel_initializer='normal', activation='relu'))

model.add(Dense(units=11, kernel_initializer='normal'))

model.compile(loss='mean_squared_error', optimizer='adam', metrics=['accuracy'])


# Train model:

# In[177]:


history = model.fit(X_train, y_train, batch_size=20, epochs = 100, verbose=1)


# Check MSE values:

# In[178]:


scores = model.evaluate(X_train, y_train, verbose=0)
for i in range(len(scores)):
    print("%s: %.2f%%" % (model.metrics_names[i], scores[i]*100))


# Plot loss during training:

# In[179]:


plt.title('Loss / Mean Squared Error')
plt.plot(history.history['loss'], label='train')
plt.legend()
plt.show()


# In[180]:


plt.title('Accuracy / Mean Squared Error')
plt.plot(history.history['accuracy'], label='train')
plt.legend()
plt.show()


# In[181]:


y_predict = model.predict(X_test)


# In[182]:


print(y_predict[0])
print(y_test.head(1))


# In[183]:


y_diff = np.subtract(y_predict, y_test.to_numpy())
print(y_diff)


# In[184]:


col_totals = [ sum(x) for x in y_diff ]
print(max(col_totals))


# Export CNN to H5 file:

# In[185]:


model.save(r"D:\MGR\fma\model.h5")


# In[ ]:




