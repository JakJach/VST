import os
import librosa

# Dataset utils
import FMA.utils
from utils import Helper

# Define audio processing values
SAMPLING_RATE = 44100

# Frequency band limit and center frequencies were taken as suggested on website:
# https://www.engineeringtoolbox.com/octave-bands-frequency-limits-d_1602.html
BAND_LIMITS = [(11,22),(22,44),(44,88),(88,177),(177,355),(355,710),(710,1420),(1420,2840),(2840,5680),(5680,11360),(11360,22720)]

# Directory where mp3 are stored
AUDIO_DIR = 'D:\\MGR\\fma\\fma_small'
METADATA_DIR = 'D:\\MGR\\fma\\fma_metadata'

# Import dataset metadata
tracks = FMA.utils.load(METADATA_DIR + '\\tracks.csv')
features = FMA.utils.load(METADATA_DIR + '\\features.csv')

# Select metadata of FMA small
subset = tracks.index[tracks['set', 'subset'] <= 'small']
assert subset.isin(tracks.index).all()
assert subset.isin(features.index).all()

# Select only those rows, that are indicated by subset
tracks = tracks.loc[subset]

# Join Multindex into one index
tracks.columns = [' '.join(col).strip() for col in tracks.columns.values]

# Select only columns important for the task
tracks = tracks[['artist name', 'track title', 'track genre_top']]

# There were loading problems with some files, so they have been removed
tracks = tracks.drop(index=99134)
tracks = tracks.drop(index=108925)
tracks = tracks.drop(index=133297)

# Iterate through audio files
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
            bandpower = []
            for band in BAND_LIMITS:
                bandpower.append(Helper.bandpower(y,sr,band[0],band[1]))

            bandpowers.append(bandpower)
            #print(bandpower)

# Add filenames to dataframe
tracks['track filename'] = filenames
tracks['track tempo'] = tempos

# Split bandpowers into seperate columns
tracks[['track band_1','track band_2','track band_3','track band_4','track band_5','track band_6','track band_7','track band_8','track band_9','track band_10','track band_11']] = bandpowers

# Export data to csv file
tracks.to_csv('D:\\MGR\\fma\\tracks.csv', header=True)

print(tracks.head())