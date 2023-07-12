using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public class ModernTextBoxWithPlaceholderSupport : ModernTextBox
    {
        private bool isFocused = false;
        protected string orgText = "";
        public string PlaceholderText { get; set; }
        public Color TextForeColor { get; set; }
        public Color PlaceholderTextForeColor { get; set; }
        [Browsable(true)]
        public event EventHandler TextChanged2;

        public void ChangeTextDisplayed() 
        {
            if (isFocused)
            {
                Text = orgText;
                ForeColor = TextForeColor;
            }
            else 
            {
                orgText = Text;

                if (string.IsNullOrEmpty(orgText))
                {
                    Text = PlaceholderText;
                    ForeColor = PlaceholderTextForeColor;
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            orgText = Text;
            ChangeTextDisplayed();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            isFocused = true;
            ChangeTextDisplayed();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            isFocused = false;
            ChangeTextDisplayed();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (isFocused && TextChanged2 != null)
                TextChanged2.Invoke(this, e);
            base.OnTextChanged(e);
        }
    }
}
