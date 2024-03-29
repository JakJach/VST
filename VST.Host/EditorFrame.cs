﻿using Jacobi.Vst.Core.Host;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VST.Host
{
    public partial class EditorFrame : Form
    {
        public EditorFrame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the Plugin Command Stub.
        /// </summary>
        public IVstPluginCommandStub PluginCommandStub { get; set; }

        /// <summary>
        /// Shows the custom plugin editor UI.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public new DialogResult ShowDialog(IWin32Window owner)
        {
            this.Text = PluginCommandStub.Commands.GetEffectName();

            if (PluginCommandStub.Commands.EditorGetRect(out Rectangle wndRect))
            {
                this.Size = this.SizeFromClientSize(new Size(wndRect.Width, wndRect.Height));
                PluginCommandStub.Commands.EditorOpen(this.Handle);
            }

            return base.ShowDialog(owner);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!e.Cancel)
            {
                PluginCommandStub.Commands.EditorClose();
            }
        }
    }
}
