namespace SubtitleKitLib.Actions
{
    using System;
    using System.Globalization;

    using Microsoft.Azure.KeyVault;

    using SubtitleKitLib.Azure;
    using SubtitleKitLib.Subtitle;

    public class SubtitleTranslatorAction : SubtitleAction
    {
        private CultureInfo _culture;

        public SubtitleTranslatorAction(ISubtitle subtitle, CultureInfo culture)
            : base(subtitle)
        {
            this._culture = culture;
        }

        public override void PerformAction(Action onCompleted)
        {
            this.TranslateSubtitle(onCompleted);
        }

        private async void TranslateSubtitle(Action onCompleted)
        {
            var azureAuthenticator = new AzureAuthenticator();
            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureAuthenticator.GetToken));
            var sec = await kv.GetSecretAsync(new Secrets().SecretId);

            var newItems = await TranslationService.TranslateArrayAsync(
                               this.Subtitle.Items.ToArray(),
                               this._culture,
                               sec.Value);

            for (int i = 0; i < this.Subtitle.Items.Count; i++)
            {
                this.Subtitle.Items[i].Lines = newItems[i].Lines;
            }

            onCompleted?.Invoke();
        }
    }
}