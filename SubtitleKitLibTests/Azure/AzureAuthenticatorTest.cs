using Microsoft.Azure.KeyVault;
using SubtitleKitLib.Azure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SubtitleKitLibTests.Azure
{
    public class AzureAuthenticatorTest
    {
        [Fact]
        public async void Authenticator_WithCorrectCredentials_ProducesTokenAsync()
        {
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(AzureAuthenticator.GetToken));
            var sec = await kv.GetSecretAsync(new Secrets().SecretId);

            Assert.NotNull(sec);
        }
    }
}
