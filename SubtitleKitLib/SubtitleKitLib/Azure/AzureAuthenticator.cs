using System;

namespace SubtitleKitLib.Azure
{
    //add these using statements
    using Microsoft.IdentityModel.Clients.ActiveDirectory;
    using System.Threading.Tasks;
    using Microsoft.Azure.KeyVault;

    public class AzureAuthenticator
    {
        //this is an optional property to hold the secret after it is retrieved
        public static string EncryptSecret { get; set; }

        //the method that will be provided to the KeyVaultClient
        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(Secrets.ClientId, Secrets.ClientKey);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
    }
}
