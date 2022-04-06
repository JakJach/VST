using System;

namespace VST.Core.Dsp
{
    internal sealed class Equalizer
    {
        private const int NUMBER_OF_FILTERS = 11;
        private static readonly float[] BAND_CENTER_FREQUENCIES = { 16f, 31.5f, 63f, 125f, 250f, 500f, 1000f, 2000f, 4000f, 8000f, 16000f };

        private float[] _equalizerBuffer;
        private int _bufferIndex;
        private int _bufferLength;

        private readonly EqualizerParameters _parameters;
        //private readonly BiQuadFilter[] _filters;

        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Equalizer(EqualizerParameters parameters)
        {
            _equalizerBuffer = Array.Empty<float>();
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

            //_filters = new BiQuadFilter[NUMBER_OF_FILTERS];
            SetFilters();

            _parameters.Band01BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band02BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band03BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band04BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band05BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band06BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band07BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band08BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band09BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band10BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
            _parameters.Band11BoostMgr.PropertyChanged += BandBoostMgr_PropertyChanged;
        }

        private void BandBoostMgr_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetFilters();
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
            if (_equalizerBuffer == null)
                return sample;

            //// process output
            //foreach (var filter in _filters)
            //{
            //    sample = filter.Transform(sample);
            //}

            return sample;
        }

        private void SetFilters()
        {
            //if (_filters.Length == 11)
            //{
            //    _filters[0] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[0], 2f, _parameters.Band01BoostMgr.CurrentValue);
            //    _filters[1] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[1], 2f, _parameters.Band02BoostMgr.CurrentValue);
            //    _filters[2] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[2], 2f, _parameters.Band03BoostMgr.CurrentValue);
            //    _filters[3] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[3], 2f, _parameters.Band04BoostMgr.CurrentValue);
            //    _filters[4] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[4], 2f, _parameters.Band05BoostMgr.CurrentValue);
            //    _filters[5] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[5], 2f, _parameters.Band06BoostMgr.CurrentValue);
            //    _filters[6] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[6], 2f, _parameters.Band07BoostMgr.CurrentValue);
            //    _filters[7] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[7], 2f, _parameters.Band08BoostMgr.CurrentValue);
            //    _filters[8] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[8], 2f, _parameters.Band09BoostMgr.CurrentValue);
            //    _filters[9] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[9], 2f, _parameters.Band10BoostMgr.CurrentValue);
            //    _filters[10] = BiQuadFilter.PeakingEQ(SampleRate, BAND_CENTER_FREQUENCIES[10], 2f, _parameters.Band11BoostMgr.CurrentValue);
            //}
        }
    }
}
