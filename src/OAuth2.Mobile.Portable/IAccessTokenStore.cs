using System.Threading;
using System.Threading.Tasks;

namespace StudioDonder.OAuth2.Mobile
{
    public interface IAccessTokenStore
    {
        Task<AccessToken> GetClientAccessToken(string clientId, string serviceId, CancellationToken cancellationToken);
        Task<AccessToken> GetUserAccessToken(string username, string serviceId, CancellationToken cancellationToken);
        Task SaveClientAccessToken(string clientId, string serviceId, AccessToken accessToken, CancellationToken cancellationToken);
        Task SaveUserAccessToken(string username, string serviceId, AccessToken accessToken, CancellationToken cancellationToken);
    }
}