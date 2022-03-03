using Jacobi.Vst.Plugin.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace VST.Core.UI
{
    /// <summary>
    /// Interaction logic for PluginEditorView.xaml
    /// </summary>
    public partial class PluginEditorView : UserControl
    {
        public PluginEditorView()
        {
            InitializeComponent();
        }

        public bool InitializeParameters(IList<VstParameterManager> parameters)
        {
            if (parameters == null || parameters.Count < 4)
                return false;

            return true;
        }

        public void ProcessIdle()
        {
            // TODO: short idle processing here
        }
    }
}
