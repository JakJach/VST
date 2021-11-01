using Jacobi.Vst.Plugin.Framework;
using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VST.UI
{
    public partial class PluginEditorView : UserControl
    {
        private Genres _selectedGenre = Genres.NONE;

        public PluginEditorView()
        {
            InitializeComponent();
        }

        internal bool InitializeParameters(IList<VstParameterManager> parameters)
        {
            if (parameters == null || parameters.Count < 4)
                return false;

            BindParameter(parameters[0], label1, pot1, label12);
            BindParameter(parameters[1], label2, pot2, label13);
            BindParameter(parameters[2], label3, pot3, label14);
            BindParameter(parameters[3], label4, pot4, label15);
            BindParameter(parameters[4], label5, pot5, label16);
            BindParameter(parameters[5], label6, pot6, label17);
            BindParameter(parameters[6], label7, pot7, label18);
            BindParameter(parameters[7], label8, pot8, label19);
            BindParameter(parameters[8], label9, pot9, label20);
            BindParameter(parameters[9], label10, pot10, label21);
            BindParameter(parameters[10], label11, pot11, label22);

            return true;
        }

        private void BindParameter(VstParameterManager paramMgr, Label label, TrackBar trackBar, Label shortLabel)
        {
            label.Text = paramMgr.ParameterInfo.Name;
            shortLabel.Text = paramMgr.ParameterInfo.ShortLabel;

            var factor = InitTrackBar(trackBar, paramMgr.ParameterInfo);
            var paramState = new ParameterControlState(paramMgr, factor);

            // use databinding for VstParameter/Manager changed notifications.
            trackBar.DataBindings.Add("Value", paramState, nameof(ParameterControlState.Value));
            trackBar.ValueChanged += TrackBar_ValueChanged;
            trackBar.Tag = paramState;
        }

        private void BindParameter(VstParameterManager paramMgr, Label label, Pot pot, Label shortLabel)
        {
            label.Text = paramMgr.ParameterInfo.Name;
            shortLabel.Text = paramMgr.ParameterInfo.ShortLabel;

            var factor = InitPot(pot, paramMgr.ParameterInfo);
            var paramState = new ParameterControlState(paramMgr, factor);

            // use databinding for VstParameter/Manager changed notifications.
            pot.DataBindings.Add("Value", paramState, nameof(ParameterControlState.Value));
            pot.ValueChanged += Pot_ValueChanged;
            pot.Tag = paramState;
        }



        private float InitPot(Pot pot, VstParameterInfo parameterInfo)
        {
            float factor = 1.0f;

            if (parameterInfo.IsSwitch)
            {
                pot.Minimum = 0;
                pot.Maximum = 1;
                return factor;
            }

            if (parameterInfo.IsStepFloatValid)
            {
                factor = 1 / parameterInfo.StepFloat;
            }

            if (parameterInfo.IsMinMaxIntegerValid)
            {
                pot.Minimum = (int)(parameterInfo.MinInteger * factor);
                pot.Maximum = (int)(parameterInfo.MaxInteger * factor);
            }
            else
            {
                pot.Minimum = 0;
                pot.Maximum = (int)factor;
            }

            return factor;
        }

        private float InitTrackBar(TrackBar trackBar, VstParameterInfo parameterInfo)
        {
            // A multiplication factor to convert floats (0.0-1.0) 
            // to an integer range for the TrackBar to work with.
            float factor = 1.0f;

            if (parameterInfo.IsSwitch)
            {
                trackBar.Minimum = 0;
                trackBar.Maximum = 1;
                trackBar.LargeChange = 1;
                trackBar.SmallChange = 1;
                return factor;
            }

            if (parameterInfo.IsStepIntegerValid)
            {
                trackBar.LargeChange = parameterInfo.LargeStepInteger;
                trackBar.SmallChange = parameterInfo.StepInteger;
            }
            else if (parameterInfo.IsStepFloatValid)
            {
                factor = 1 / parameterInfo.StepFloat;
                trackBar.LargeChange = (int)(parameterInfo.LargeStepFloat * factor);
                trackBar.SmallChange = (int)(parameterInfo.StepFloat * factor);
            }

            if (parameterInfo.IsMinMaxIntegerValid)
            {
                trackBar.Minimum = (int)(parameterInfo.MinInteger * factor);
                trackBar.Maximum = (int)(parameterInfo.MaxInteger * factor);
            }
            else
            {
                trackBar.Minimum = 0;
                trackBar.Maximum = (int)factor;
            }

            return factor;
        }

        private void TrackBar_ValueChanged(object? sender, EventArgs e)
        {
            var trackBar = (TrackBar?)sender;
            var paramState = (ParameterControlState?)trackBar?.Tag;

            if (trackBar != null &&
                paramState?.ParameterManager.ActiveParameter != null)
            {
                paramState.ParameterManager.ActiveParameter.Value =
                    trackBar.Value / paramState.ValueFactor;
            }
        }

        private void Pot_ValueChanged(object? sender, EventArgs e)
        {
            var pot = (Pot?)sender;
            var paramState = (ParameterControlState?)pot?.Tag;

            if (pot != null &&
                paramState?.ParameterManager.ActiveParameter != null)
            {
                paramState.ParameterManager.ActiveParameter.Value =
                    (float)(pot.Value / paramState.ValueFactor);
            }
        }

        internal void ProcessIdle()
        {
            // TODO: short idle processing here
        }

        /// <summary>
        /// This class converts the parameter value range to a compatible (integer) TrackBar value range.
        /// </summary>
        private sealed class ParameterControlState
        {
            public ParameterControlState(VstParameterManager parameterManager, float valueFactor)
            {
                ParameterManager = parameterManager;
                ValueFactor = valueFactor;
            }

            public VstParameterManager ParameterManager { get; }
            public float ValueFactor { get; }

            public int Value
            {
                get
                {
                    return (int)(ParameterManager.CurrentValue * ValueFactor);
                }
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (sender != null)
                _selectedGenre = (Genres)Enum.Parse(typeof(Genres), sender.ToString(), true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_selectedGenre != Genres.NONE)
            {
                DisablePots();
                //TODO: tuning EQ to the genre
                EnablePots();
            }
        }

        #region Enable / Disable Pots
        private void DisablePots()
        {
            pot1.Enabled = false;
            pot2.Enabled = false;
            pot3.Enabled = false;
            pot4.Enabled = false;
            pot5.Enabled = false;
            pot6.Enabled = false;
            pot7.Enabled = false;
            pot8.Enabled = false;
            pot9.Enabled = false;
            pot10.Enabled = false;
            pot11.Enabled = false;
        }

        private void EnablePots()
        {
            pot1.Enabled = true;
            pot2.Enabled = true;
            pot3.Enabled = true;
            pot4.Enabled = true;
            pot5.Enabled = true;
            pot6.Enabled = true;
            pot7.Enabled = true;
            pot8.Enabled = true;
            pot9.Enabled = true;
            pot10.Enabled = true;
            pot11.Enabled = true;
        }
        #endregion
    }
}
