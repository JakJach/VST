using NAudio.Dsp;
using System;

namespace VST.Dsp
{
    internal sealed class Equalizer
    {
        private const int NUMBER_OF_FILTERS = 11;
        private static readonly float[] BAND_CENTER_FREQUENCIES = { 16f, 31.5f, 63f, 125f, 250f, 500f, 1000f, 2000f, 4000f, 8000f, 16000f };

        private readonly EqualizerParameters _parameters;
        private readonly BiQuadFilter[] _filters;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Equalizer(EqualizerParameters parameters)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

            _filters = new BiQuadFilter[NUMBER_OF_FILTERS];
            SetFilters(parameters);

            _parameters.Band01BoostMgr.PropertyChanged += Band01BoostMgr_PropertyChanged;
        }

        private void Band01BoostMgr_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(ReferenceEquals(_parameters.Band01BoostMgr, sender))
            {
                SetFilters(_parameters);
            }
        }

        private float _sampleRate;
        /// <summary>
        /// Gets or sets the sample rate.
        /// </summary>
        public float SampleRate
        {
            get { return _sampleRate; }
            set { _sampleRate = value; }
        }

        /// <summary>
        /// Processes the <paramref name="sample"/> using a EQ effect.
        /// </summary>
        /// <param name="sample">A single sample.</param>
        /// <returns>Returns the new value for the sample.</returns>
        public float ProcessSample(float sample)
        {
            // process output
            float output = 0;

            foreach(var filter in _filters)
            {
                output += filter.Transform(sample);
            }

            return output;
        }

        private void SetFilters(EqualizerParameters parameters)
        {
            if (_filters.Length == 11)
            {
                _filters[0] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[0], 0.5f, parameters.Band01BoostMgr.CurrentValue);
                _filters[1] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[1], 0.5f, parameters.Band02BoostMgr.CurrentValue);
                _filters[2] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[2], 0.5f, parameters.Band03BoostMgr.CurrentValue);
                _filters[3] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[3], 0.5f, parameters.Band04BoostMgr.CurrentValue);
                _filters[4] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[4], 0.5f, parameters.Band05BoostMgr.CurrentValue);
                _filters[5] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[5], 0.5f, parameters.Band06BoostMgr.CurrentValue);
                _filters[6] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[6], 0.5f, parameters.Band07BoostMgr.CurrentValue);
                _filters[7] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[7], 0.5f, parameters.Band08BoostMgr.CurrentValue);
                _filters[8] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[8], 0.5f, parameters.Band09BoostMgr.CurrentValue);
                _filters[9] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[9], 0.5f, parameters.Band10BoostMgr.CurrentValue);
                _filters[10] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[10], 0.5f, parameters.Band11BoostMgr.CurrentValue);
            }
        }
    }
}
