using d4160.Runtime.OpenAI.API;
using UnityEngine;

namespace d4160.Runtime.OpenAI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "OpenAISettings", menuName = "d4160/OpenAI/API/Settings")]
    public class OpenAISettingsSO : ScriptableObject
    {
        [SerializeField] private string _apiKey;
        [SerializeField] private float _checksDelay = 0.5f;

        public string ApiKey => _apiKey;
        public float ChecksDelay => _checksDelay;

        public OpenAISettings GetSettings() => new(_apiKey);
    }
}