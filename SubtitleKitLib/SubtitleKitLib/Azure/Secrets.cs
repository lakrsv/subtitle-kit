using System;
using System.Collections.Generic;
using System.Text;

namespace SubtitleKitLib.Azure
{
    public class Secrets
    {
        public string ClientId => Environment.GetEnvironmentVariable("SubClientId");
        public string ClientKey => Environment.GetEnvironmentVariable("SubClientSecret");
        public string SecretId => Environment.GetEnvironmentVariable("SubSecretUri");
    }
}
