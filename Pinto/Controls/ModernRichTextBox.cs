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
    public partial class ModernRichTextBox : UserControl
    {
        [Browsable(true)]
        public override string Text { get => rtxtTextBox.Text; set => rtxtTextBox.Text = value; }
        [Browsable(true)]
        public override Color ForeColor { get => rtxtTextBox.ForeColor; set => rtxtTextBox.ForeColor = value; }

        public ModernRichTextBox()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, Width - 1, Height - 1);
        }
    }
}
