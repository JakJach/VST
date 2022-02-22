using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using System;

namespace VST.Dsp
{
    /// <summary>
    /// Encapsulated fixed EQ parameters.
    /// </summary>
    internal sealed class EqualizerParameters
    {
        private const string ParameterCategoryName = "EQ";

        /// <summary>
        /// Initializes the paramaters for the EQ component.
        /// </summary>
        /// <param name="parameters"></param>
        public EqualizerParameters(PluginParameters parameters)
        {
            Throw.IfArgumentIsNull(parameters, nameof(parameters));

            InitializeParameters(parameters);
        }

        public VstParameterManager Band01BoostMgr { get; private set; }
        public VstParameterManager Band02BoostMgr { get; private set; }
        public VstParameterManager Band03BoostMgr { get; private set; }
        public VstParameterManager Band04BoostMgr { get; private set; }
        public VstParameterManager Band05BoostMgr { get; private set; }
        public VstParameterManager Band06BoostMgr { get; private set; }
        public VstParameterManager Band07BoostMgr { get; private set; }
        public VstParameterManager Band08BoostMgr { get; private set; }
        public VstParameterManager Band09BoostMgr { get; private set; }
        public VstParameterManager Band10BoostMgr { get; private set; }
        public VstParameterManager Band11BoostMgr { get; private set; }


        // This method initializes the plugin parameters this Dsp component owns.
        private void InitializeParameters(PluginParameters parameters)
        {
            // all parameter definitions are added to a central list.
            VstParameterInfoCollection parameterInfos = parameters.ParameterInfos;

            // retrieve the category for all EQ parameters.
            VstParameterCategory paramCategory =
                parameters.GetParameterCategory(ParameterCategoryName);

            #region Parameter Info Setup
            // 1st band boost parameter
            var paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 1",
                Label = "16 Hz",
                ShortLabel = "16 Hz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band01BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 2nd band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 2",
                Label = "32 Hz",
                ShortLabel = "32 Hz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band02BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 3rd band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 3",
                Label = "63 Hz",
                ShortLabel = "63 Hz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band03BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 4th band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 4",
                Label = "125 Hz",
                ShortLabel = "125 Hz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band04BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 5th band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 5",
                Label = "250 Hz",
                ShortLabel = "250 Hz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band05BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 6th band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 6",
                Label = "500 Hz",
                ShortLabel = "500 Hz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band06BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 7th band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 7",
                Label = "1 kHz",
                ShortLabel = "1 kHz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band07BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 8th band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 8",
                Label = "2 kHz",
                ShortLabel = "2 kHz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band08BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 9th band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 9",
                Label = "4 kHz",
                ShortLabel = "4 kHz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band09BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 10th band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 10",
                Label = "8 kHz",
                ShortLabel = "8 kHz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band10BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);

            // 11th band boost parameter
            paramInfo = new VstParameterInfo
            {
                Category = paramCategory,
                CanBeAutomated = false,
                Name = "Band 11",
                Label = "16 kHz",
                ShortLabel = "16 kHz",
                LargeStepFloat = 0.1f,
                SmallStepFloat = 0.01f,
                StepFloat = 0.05f,
                DefaultValue = 0.5f,
                NullValue = 0.5f
            };

            Band11BoostMgr = paramInfo
                .Normalize()
                .ToManager();

            parameterInfos.Add(paramInfo);
            #endregion
        }
    }
}
