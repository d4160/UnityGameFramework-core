using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class OpenAISettings
    {
        public string apiKey;

        public OpenAISettings(string apiKey)
        {
            this.apiKey = apiKey;
        }
    }
}