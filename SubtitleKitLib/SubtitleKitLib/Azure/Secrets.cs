using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleKitLib.Azure
{
    public class Secrets
    {
        public string ClientId;
        public string ClientSecret;
        public string SecretUri;

        private Secrets(string clientId, string clientSecret, string secretUri)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            SecretUri = secretUri;
        }

        public static Secrets GetSecrets()
        {
            return new Secrets(Environment.GetEnvironmentVariable("SubClientId", EnvironmentVariableTarget.Machine), Environment.GetEnvironmentVariable("SubClientSecret", EnvironmentVariableTarget.Machine), Environment.GetEnvironmentVariable("SubSecretUri", EnvironmentVariableTarget.Machine));
        }
    }
}
