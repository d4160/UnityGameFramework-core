using System;
using d4160.Runtime.OpenAI.API;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace d4160.Runtime.OpenAI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CreateThreadRequest", menuName = "d4160/OpenAI/Requests/CreateThread")]
    public class CreateThreadRequestSO : ScriptableObject
    {
        [SerializeField] private OpenAISettingsSO _settingsSO;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SendRequest()
        {
            SendRequest(_settingsSO.ApiKey);
        }

        public void SendRequest(Action<string> onResponse, Action<string> onError = null)
        {
            SendRequest(_settingsSO.ApiKey, null);
        }

        public void SendRequest(string apiKey, Action<string> onResponse = null, Action<string> onError = null)
        {
            AssistantsAPI.CreateThread(apiKey, onError, onResponse);
        }
    }
}
