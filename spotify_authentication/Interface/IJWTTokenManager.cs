namespace spotify_authentication.Interface
{
    public interface IJWTTokenManager
    {
        string Authenticate(string username, string password);
    }
}
