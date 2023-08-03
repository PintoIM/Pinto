using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public class NoDeselectDataGridView : DataGridView
    {
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control) return;
            base.OnMouseDown(e);
        }
    }
}
