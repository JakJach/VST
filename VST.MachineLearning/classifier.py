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

#MATPLOTLIB
import matplotlib.pyplot as plt
from matplotlib.backends.backend_agg import FigureCanvasAgg as FigureCanvas

#KERAS
from keras import layers
from keras.layers import (Input, Add, Dense, Activation, ZeroPadding2D, BatchNormalization, Flatten, 
                          Conv2D, AveragePooling2D, MaxPooling2D, GlobalMaxPooling2D)
from keras.models import Model, load_model
from keras.preprocessing import image
from keras.preprocessing.image import ImageDataGenerator
from keras.utils import layer_utils, plot_model
from keras.utils.vis_utils import model_to_dot
from keras.optimizers import Adam
from keras.initializers import glorot_uniform

#Dataset utils
import FMA.utils


#Directory where mp3 are stored
AUDIO_DIR = 'D:\\MGR\\fma\\fma_small'
METADATA_DIR = 'D:\\MGR\\fma\\fma_metadata'

#Import dataset metadata
tracks = FMA.utils.load(METADATA_DIR + '\\tracks.csv')
features = FMA.utils.load(METADATA_DIR + '\\features.csv')
echonest = FMA.utils.load(METADATA_DIR + '\\echonest.csv')

file_name = FMA.utils.get_audio_path(AUDIO_DIR, 2)

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