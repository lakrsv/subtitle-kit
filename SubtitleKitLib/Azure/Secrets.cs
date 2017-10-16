namespace SubtitleKitLib.Azure
{
    using System;

    public class Secrets
    {
        public string ClientId => Environment.GetEnvironmentVariable("SubClientId");

        public string ClientKey => Environment.GetEnvironmentVariable("SubClientSecret");

        public string SecretId => Environment.GetEnvironmentVariable("SubSecretUri");
    }
}