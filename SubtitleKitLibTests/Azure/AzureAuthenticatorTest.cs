using Microsoft.Azure.KeyVault;
using SubtitleKitLib.Azure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using NSubstitute;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault.Models;

namespace SubtitleKitLibTests.Azure
{
    public class AzureAuthenticatorTest
    {
        [Fact]
        public async void Authenticator_WithValidCredentials_ProducesTokenAsync()
        {
            var azureAuthenticator = new AzureAuthenticator();
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureAuthenticator.GetToken));
            var sec = await kv.GetSecretAsync(new Secrets().SecretId);

            Assert.NotNull(sec);
        }

        [Fact]
        public async void Authenticator_WithInvalidCredentials_ThrowsKeyVaultErrorException()
        {
            var azureAuthenticator = Substitute.For<AzureAuthenticator>();
            azureAuthenticator.GetToken("a", "b", "c").ReturnsForAnyArgs("Invalid");
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureAuthenticator.GetToken));

            await Assert.ThrowsAsync<KeyVaultErrorException>(async ()=> 
            {
                var sec = await kv.GetSecretAsync(new Secrets().SecretId);
            });
        }
    }
}
