using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public partial class ModernTextBox : UserControl
    {
        public Color BorderColor { get; set; } = Color.DarkGray;
        public Image Image { get => pbIcon.Image; set => pbIcon.Image = value; }
        [Browsable(true)]
        public override string Text { get => txtTextBox.Text; set => txtTextBox.Text = value; }
        [Browsable(true)]
        public override Color ForeColor { get => txtTextBox.ForeColor; set => txtTextBox.ForeColor = value; }
        
        public ModernTextBox()
        {
            InitializeComponent();
            txtTextBox.GotFocus += (object sender, EventArgs e) => OnGotFocus(e);
            txtTextBox.LostFocus += (object sender, EventArgs e) => OnLostFocus(e);
            txtTextBox.TextChanged += (object sender, EventArgs e) => OnTextChanged(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.DrawRectangle(new Pen(BorderColor, 1), 0, 0, Width - 1, Height - 1);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = 20;
        }

        public new void Focus() => txtTextBox.Focus();
    }
}
