using Jacobi.Vst.Core;
using Jacobi.Vst.Plugin.Framework;
using System;
using System.Drawing;
using System.Linq;
using VST.Core.UI;

namespace VST.Core
{
    /// <summary>
    /// This object manages the custom editor (UI) for your plugin.
    /// </summary>
    /// <remarks>
    /// When you do not implement a custom editor, 
    /// your Parameters will be displayed in an editor provided by the host.
    /// </remarks>
    internal sealed class PluginEditor : IVstPluginEditor
    {
        private readonly PluginParameters _parameters;
        private readonly WPFControlWrapper<PluginEditorView> _view;

        public PluginEditor(PluginParameters parameters)
        {
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _view = new WPFControlWrapper<PluginEditorView>(800, 600);
        }

        public Rectangle Bounds
        {
            get
            {
                _view.GetBounds(out Rectangle bounds);
                return bounds;
            }
        }

        public void Close()
        {
            _view.Close();
        }

        public bool KeyDown(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            // empty by design
            return false;
        }

        public bool KeyUp(byte ascii, VstVirtualKey virtualKey, VstModifierKeys modifers)
        {
            // empty by design
            return false;
        }

        public VstKnobMode KnobMode { get; set; }

        public void Open(IntPtr hWnd)
        {
            // make a list of parameters to pass to the dlg.
            var paramList = _parameters.ParameterInfos
                .Where(p => p.ParameterManager != null)
                .Select(p => p.ParameterManager!)
                .ToList();

            //_view.Instance.InitializeParameters(paramList);

            _view.Open(hWnd);
        }

        public void ProcessIdle()
        {
            // keep your processing short!
            _view.Instance.ProcessIdle();
        }
    }
}
