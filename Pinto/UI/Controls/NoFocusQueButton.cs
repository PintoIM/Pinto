using System.Windows.Forms;

namespace PintoNS.UI.Controls
{
    // Stolen from https://social.msdn.microsoft.com/Forums/windows/en-US/8d85e6dd-18ad-4503-9f0a-731a8d73d570/removing-the-focus-rectangle-from-a-flat-button-when-the-form-itself-does-not-have-focus?forum=winforms
    public class NoFocusQueButton : Button
    {
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }
    }
}
