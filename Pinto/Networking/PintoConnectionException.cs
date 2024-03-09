using System;

namespace PintoNS.Networking
{
    internal class PintoConnectionException : Exception
    {
        internal PintoConnectionException(string message) : base(message) { }
    }
}
