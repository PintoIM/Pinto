using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoNS.Networking
{
    internal class PintoConnectionException : Exception
    {
        internal PintoConnectionException(string message) : base(message) { }
    }
}
