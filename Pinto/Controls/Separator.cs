using System;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class Separator : Label
    {
        public Separator()
        {
            InitializeComponent();
            Width = 100;
            OnSizeChanged(EventArgs.Empty);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            BorderStyle = BorderStyle.Fixed3D;
            Height = 2;
            AutoSize = false;
            Text = null;
            base.OnSizeChanged(e);
        }
    }
}
