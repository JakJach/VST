import scipy
import numpy as np

# Frequency band limit and center frequencies were taken as suggested on website:
# https://www.engineeringtoolbox.com/octave-bands-frequency-limits-d_1602.html
BAND_LIMITS = [(11,22),
               (22,44),
               (44,88),
               (88,177),
               (177,355),
               (355,710),
               (710,1420),
               (1420,2840),
               (2840,5680),
               (5680,11360),
               (11360,22720)]

BAND_CENTERS = [16,31.5,63,125,250,500,1000,2000,4000,8000,16000]

class Helper:
    def bandpower(x, fs, fmin, fmax):
        # Extracting the length and the half-length of the signal to input to the foruier transform
        sig_length = len(x)
        half_length = np.ceil((sig_length + 1) / 2.0).astype(np.int)

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

        idx_min = scipy.argmax(x_axis > fmin) - 1
        idx_max = scipy.argmax(x_axis > fmax) - 1

        return np.average(exp_signal[idx_min:idx_max])
