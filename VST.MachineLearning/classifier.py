import pandas as pd
import os
import librosa
import librosa.display
import numpy as np
import IPython.display as ipd
import matplotlib.pyplot as plt

import FMA.utils as utils

os.environ['KAGGLE_CONFIG_DIR'] = "/content"

# Directory where mp3 are stored.
AUDIO_DIR = 'D:\\MGR\\fma\\fma_small'
METADATA_DIR = 'D:\\MGR\\fma\\fma_metadata'

#Import dataset metadata
tracks = utils.load(METADATA_DIR + '\\tracks.csv')
features = utils.load(METADATA_DIR + '\\features.csv')
echonest = utils.load(METADATA_DIR + '\\echonest.csv')

file_name = utils.get_audio_path(AUDIO_DIR, 2)

print(file_name)

audio_data, sampling_rate = librosa.load(file_name)

print(sampling_rate)

print(tracks.head())

print(tracks['track','genre_top'].value_counts())

def features_extractor(file_name):
    audio, sample_rate = librosa.load(file_name, res_type='kaiser_fast') 
    mfccs_features = librosa.feature.mfcc(y=audio, sr=sample_rate, n_mfcc=40)
    mfccs_scaled_features = np.mean(mfccs_features.T,axis=0)
    
    return mfccs_scaled_features

print(features_extractor(file_name))