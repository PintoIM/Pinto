using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS
{
    // Why the fuck was I retarded and put these in Program.cs????
    public static class Utils
    {
        public static IEnumerable<string> SplitStringIntoChunks(string str, int chunkSize)
        {
            for (int i = 0; i < str.Length; i += chunkSize)
                yield return str.Substring(i, Math.Min(chunkSize, str.Length - i));
        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict,
            TKey key, TValue @default)
        {
            return dict.TryGetValue(key, out var value) ? value : @default;
        }

        public static Form ConstructTextOnlyForm(string text, string title) 
        {
            Form form = new Form
            {
                Text = $"Pinto! - {title}",
                Size = new Size(300, 225),
                Icon = Program.GetFormIcon(),
                ShowInTaskbar = false
            };
            RichTextBox textBox = new RichTextBox
            {
                Text = text,
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = SystemColors.Window
            };
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem copyItem = new ToolStripMenuItem("Copy");

            copyItem.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(textBox.SelectedText)) return;
                Clipboard.SetText(textBox.SelectedText);
            };
            contextMenu.Items.Add(copyItem);
            textBox.ContextMenuStrip = contextMenu;
            form.Controls.Add(textBox);

            return form;
        }
    }
}
