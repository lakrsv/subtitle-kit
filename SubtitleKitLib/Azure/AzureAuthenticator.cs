// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AzureAuthenticator.cs" author="Lars-Kristian Svenoy">
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