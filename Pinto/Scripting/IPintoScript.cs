namespace PintoNS.Scripting
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
