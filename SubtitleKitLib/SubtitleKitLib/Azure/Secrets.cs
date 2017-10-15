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
            return new Secrets(Environment.GetEnvironmentVariable("SubClientId", EnvironmentVariableTarget.User), Environment.GetEnvironmentVariable("SubClientSecret", EnvironmentVariableTarget.User), Environment.GetEnvironmentVariable("SubSecretUri", EnvironmentVariableTarget.User));
        }
    }
}
