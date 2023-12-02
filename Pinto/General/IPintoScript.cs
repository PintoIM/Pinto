using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PintoNS.General
{
    public interface IPintoScript
    {
        /// <summary>
        /// Information about this script
        /// </summary>
        PintoScriptInfo GetScriptInfo();

        /// <summary>
        /// Called when the script has been loaded
        /// </summary>
        void OnLoad();

        /// <summary>
        /// Called when Pinto! has been fully initialized
        /// </summary>
        void OnPintoInit();
    }
}
