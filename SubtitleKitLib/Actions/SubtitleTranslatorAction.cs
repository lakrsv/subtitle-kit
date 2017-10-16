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