import numpy as np
import matplotlib.pyplot as plt

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
