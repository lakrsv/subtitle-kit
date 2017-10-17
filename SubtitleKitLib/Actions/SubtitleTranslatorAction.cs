// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SubtitleTranslatorAction.cs" author="Lars-Kristian Svenoy">
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

namespace SubtitleKitLib.Actions
{
    using System;
    using System.Globalization;

    using Microsoft.Azure.KeyVault;

    using SubtitleKitLib.Azure;
    using SubtitleKitLib.Subtitle;

    public class SubtitleTranslatorAction : SubtitleAction
    {
        private readonly CultureInfo _culture;

        public SubtitleTranslatorAction(ISubtitle subtitle, CultureInfo culture)
            : base(subtitle)
        {
            _culture = culture;
        }

        public override void PerformAction(Action onCompleted)
        {
            TranslateSubtitle(onCompleted);
        }

        private async void TranslateSubtitle(Action onCompleted)
        {
            var azureAuthenticator = new AzureAuthenticator();
            var kv = new KeyVaultClient(azureAuthenticator.GetToken);
            var sec = await kv.GetSecretAsync(new Secrets().SecretId);

            var newItems = await TranslationService.TranslateArrayAsync(Subtitle.Items.ToArray(), _culture, sec.Value);

            for (var i = 0; i < Subtitle.Items.Count; i++) Subtitle.Items[i].Lines = newItems[i].Lines;

            onCompleted?.Invoke();
        }
    }
}