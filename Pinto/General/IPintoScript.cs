using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS.General
{
    public interface IPintoScript
    {
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
