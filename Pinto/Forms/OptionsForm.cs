using PintoNS.Controls;
using PintoNS.General;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PintoNS.Forms
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            Icon = Program.GetFormIcon();
        }

        private void ChangedValue(FieldInfo field, object value) 
        {
            field.SetValue(null, value);
            Settings.Export(Program.SettingsFile);
        }

        private Control GetControlFromValue(string displayName, FieldInfo field, string helpInfo, 
            int numMin, int numMax) 
        {
            Control control = null;
            Panel p = null;
            Label l = new Label();

            switch (field.FieldType.Name) 
            {
                case nameof(Boolean):
                    CheckBox cb = new CheckBox();
                    cb.Text = displayName;
                    cb.Checked = (bool)field.GetValue(null);
                    cb.AutoSize = true;
                    cb.CheckedChanged += (object sender, EventArgs e) => ChangedValue(field, cb.Checked);
                    control = cb;
                    break;
                case nameof(String):
                    TextBox txt = new TextBox();

                    p = new Panel();
                    p.AutoSize = true;
                    p.Controls.Add(l);
                    p.Controls.Add(txt);

                    l.Text = $"{displayName}:";
                    txt.Text = (string)field.GetValue(null);
                    txt.AutoSize = true;
                    txt.TextChanged += (object sender, EventArgs e) => ChangedValue(field, txt.Text);
                    txt.Location = new Point(0, 20);
                    txt.BringToFront();

                    control = txt;
                    break;
                case nameof(Int32):
                    NumericUpDown nud = new NumericUpDown();

                    p = new Panel();
                    p.AutoSize = true;
                    p.Controls.Add(l);
                    p.Controls.Add(nud);

                    l.Text = $"{displayName}:";
                    nud.Minimum = numMin;
                    nud.Maximum = numMax;
                    nud.Value = (int)field.GetValue(null);
                    nud.AutoSize = true;
                    nud.TextChanged += (object sender, EventArgs e) => ChangedValue(field, (int)nud.Value);
                    nud.Location = new Point(0, 20);
                    nud.BringToFront();

                    control = nud;
                    break;
            }

            if (p != null || control != null)
                hpHelp.SetHelpString(p != null ? p : control, helpInfo);
            return p != null ? p : control;
        }

        private void OptionsForm_Load(object sender, System.EventArgs e)
        {
            Settings.Import(Program.SettingsFile);
            Type type = typeof(Settings);

            foreach (FieldInfo field in type.GetFields())
            {
                object[] attributes = field.GetCustomAttributes(typeof(OptionsDisplayAttribute), false);
                OptionsDisplayAttribute displayAttribute = attributes.Length > 0 ? 
                    (OptionsDisplayAttribute)attributes[0] : null;
                if (displayAttribute != null && displayAttribute.Hidden) continue;

                string category = displayAttribute != null && displayAttribute.Category != null ?
                    displayAttribute.Category : "General";
                string displayName = displayAttribute != null && displayAttribute.DisplayName != null ? 
                    displayAttribute.DisplayName : field.Name;
                string helpInfo = displayAttribute != null && displayAttribute .HelpInfo != null ? 
                    displayAttribute.HelpInfo : "No help available for this item";
                int numMin = displayAttribute != null ? displayAttribute.NumMin : int.MinValue;
                int numMax = displayAttribute != null ? displayAttribute.NumMax : int.MaxValue;

                switch (category) 
                {
                    case "General":
                        flpGeneralContainer.Controls.Add(GetControlFromValue(displayName, field, helpInfo,
                            numMin, numMax));
                        break;
                    case "Privacy":
                        flpPrivacyContainer.Controls.Add(GetControlFromValue(displayName, field, helpInfo,
                            numMin, numMax));
                        break;
                    // Invalid category, do not display
                    default:
                        break;
                }
            }
        }
    }
}
