using UnityEngine;
using System;
using d4160.Runtime.OpenAI.API;


#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace d4160.Runtime.OpenAI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ListThreadMessagesRequest", menuName = "d4160/OpenAI/Requests/ListThreadMessages")]
    public class ListThreadMessagesRequestSO : ScriptableObject
    {
        [SerializeField] private OpenAISettingsSO _settingsSO;

        [Space]

        [SerializeField] private string _threadId;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SendRequest()
        {
            SendRequest(_threadId, _settingsSO.ApiKey);
        }

        public void SendRequest(Action<string> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _settingsSO.ApiKey, null);
        }

        public void SendRequest(string threadId, Action<string> onResponse = null, Action<string> onError = null)
        {
            SendRequest(threadId, _settingsSO.ApiKey, onError, onResponse);
        }

        public void SendRequest(string threadId, string apiKey, Action<string> onResponse = null, Action<string> onError = null)
        {
            AssistantsAPI.ListThreadMessages(threadId, apiKey, onError, onResponse);
        }
    }
}
