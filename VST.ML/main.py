import tensorflow as tf
import numpy as np
import glob
import os
import librosa
import pandas as pd
import pydot
import shutil
import random

from PIL import Image
from IPython.display import SVG
from pydub import AudioSegment

#SCIPY
import scipy
from scipy import misc

#KERAS
from keras import layers
from keras.layers import (Input, Add, Dense, Activation, ZeroPadding2D, BatchNormalization, Flatten, 
                          Conv2D, AveragePooling2D, MaxPooling2D, GlobalMaxPooling2D)
from keras.models import Model, load_model
from keras.preprocessing import image
from keras.preprocessing.image import ImageDataGenerator
from keras.utils import layer_utils
from keras.utils.vis_utils import plot_model
from keras.utils.vis_utils import model_to_dot
from tensorflow.keras.optimizers import Adam
from keras.initializers import glorot_uniform

#Dataset utils
import FMA.utils
from utils import Helper

#Define audio processing values
SAMPLING_RATE = 44100
N_FFT = 64
FREQS = librosa.fft_frequencies(sr=SAMPLING_RATE, n_fft=N_FFT)

#Directory where mp3 are stored
AUDIO_DIR = 'D:\\MGR\\fma\\fma_small'
METADATA_DIR = 'D:\\MGR\\fma\\fma_metadata'
WORKSPACE_DIR = 'D:\\MGR\\_workspace'

#Import dataset metadata
tracks = FMA.utils.load(METADATA_DIR + '\\tracks.csv')
features = FMA.utils.load(METADATA_DIR + '\\features.csv')
echonest = FMA.utils.load(METADATA_DIR + '\\echonest.csv')

#Select metadata of FMA small
subset = tracks.index[tracks['set', 'subset'] <= 'small']
assert subset.isin(tracks.index).all()
assert subset.isin(features.index).all()

#Join echonest features
all_features = features.join(echonest, how='inner').sort_index(axis=1)

#Select only those rows, that are indicated by subset
tracks = tracks.loc[subset]
all_features = features.loc[subset]

#Join Multindex into one index
tracks.columns = [' '.join(col).strip() for col in tracks.columns.values]

#Select only columns important for the task
tracks = tracks[['artist name', 'track title', 'track genre_top']]

#There was some problem with file id=99134, so it has been removed
tracks = tracks.drop(index=99134)
tracks = tracks.drop(index=108925)
tracks = tracks.drop(index=133297)

#Iterate through audio files
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

            #Calculate and save tempo
            tempo = librosa.beat.tempo(y=y, sr=sr)
            tempos.append(tempo)
            #print(tempo)

            #Calculate each band power and add them to df
            bandpower = [Helper.bandpower(y,sr,11,22),Helper.bandpower(y,sr,22,44),Helper.bandpower(y,sr,44,88),
                         Helper.bandpower(y,sr,88,177),Helper.bandpower(y,sr,177,355),Helper.bandpower(y,sr,355,710),
                         Helper.bandpower(y,sr,710,1420),Helper.bandpower(y,sr,1420,2840),Helper.bandpower(y,sr,2840,5680),
                         Helper.bandpower(y,sr,5680,11360),Helper.bandpower(y,sr,11360,22720)]
            bandpowers.append(bandpower)
            #print(bandpower)

#Add filenames to dataframe
tracks['track filename'] = filenames;
tracks['track tempo'] = tempos;
tracks['track bandpowers'] = bandpowers;

print(tracks.head())