namespace SubtitleKitLibTests.Azure
{
    using Microsoft.Azure.KeyVault;
    using Microsoft.Azure.KeyVault.Models;

    using NSubstitute;

    using SubtitleKitLib.Azure;

    using Xunit;

    public class AzureAuthenticatorTest
    {
        [Fact]
        public async void Authenticator_WithInvalidCredentials_ThrowsKeyVaultErrorException()
        {
            var azureAuthenticator = Substitute.For<AzureAuthenticator>();
            azureAuthenticator.GetToken("a", "b", "c").ReturnsForAnyArgs("Invalid");
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureAuthenticator.GetToken));

            await Assert.ThrowsAsync<KeyVaultErrorException>(
                async () =>
                    {
                        var sec = await kv.GetSecretAsync(new Secrets().SecretId);
                    });
        }

        [Fact]
        public async void Authenticator_WithValidCredentials_ProducesTokenAsync()
        {
            var azureAuthenticator = new AzureAuthenticator();
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureAuthenticator.GetToken));
            var sec = await kv.GetSecretAsync(new Secrets().SecretId);

            Assert.NotNull(sec);
        }
    }
}