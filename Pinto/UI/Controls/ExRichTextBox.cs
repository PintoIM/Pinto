using System.Windows.Forms;

namespace PintoNS.UI.Controls
{
    public class ExRichTextBox : RichTextBox
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;

                if (Program.UseExRichTextBox)
                    createParams.ClassName = "RichEdit50W";

                return createParams;
            }
        }
    }
}
