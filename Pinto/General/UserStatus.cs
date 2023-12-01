namespace PintoNS.General
{
    public enum UserStatus
    {
        ONLINE = 0,
        AWAY = 1,
        BUSY = 2,
        INVISIBLE = 3,
        OFFLINE = 4,
        // NEVER SEND THIS TO THE SERVER
        CONNECTING = 5
    }
}
