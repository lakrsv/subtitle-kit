// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureAuthenticatorTest.cs" author="Lars-Kristian Svenoy">
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software Foundation,
//  Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301  USA
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

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
            var kv = new KeyVaultClient(azureAuthenticator.GetToken);

            await Assert.ThrowsAsync<KeyVaultErrorException>(
                async () => { await kv.GetSecretAsync(new Secrets().SecretId); });
        }

        [Fact]
        public async void Authenticator_WithValidCredentials_ProducesTokenAsync()
        {
            var azureAuthenticator = new AzureAuthenticator();
            var kv = new KeyVaultClient(azureAuthenticator.GetToken);
            var sec = await kv.GetSecretAsync(new Secrets().SecretId);

            Assert.NotNull(sec);
        }
    }
}