import scipy

#Frequency band limit and center frequencies were taken as suggested on website:
#https://www.engineeringtoolbox.com/octave-bands-frequency-limits-d_1602.html
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
        f, Pxx = scipy.signal.periodogram(x,fs=fs)
        idx_min = scipy.argmax(f > fmin) - 1
        idx_max = scipy.argmax(f > fmax) -1
        return scipy.trapz(Pxx[idx_min: idx_max], f[idx_min: idx_max])