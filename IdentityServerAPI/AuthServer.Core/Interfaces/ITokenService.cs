namespace AuthServer.Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(string username);
    }
}