namespace SubtitleKitLib.Azure
{
    // add these using statements
    using System;
    using System.Threading.Tasks;

    using Microsoft.IdentityModel.Clients.ActiveDirectory;

    public class AzureAuthenticator
    {
        // the method that will be provided to the KeyVaultClient
        public virtual async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            var secrets = new Secrets();
            var clientCred = new ClientCredential(secrets.ClientId, secrets.ClientKey);
            var result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null) throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
    }
}