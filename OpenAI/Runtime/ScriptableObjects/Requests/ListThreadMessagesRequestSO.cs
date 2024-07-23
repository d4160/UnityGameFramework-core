using UnityEngine;
using System;
using d4160.Runtime.OpenAI.API;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

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

        public void SendRequest(Action<MessageListObject> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _settingsSO.ApiKey, null);
        }

        public void SendRequest(string threadId, Action<MessageListObject> onResponse = null, Action<string> onError = null)
        {
            SendRequest(threadId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, string apiKey, Action<MessageListObject> onResponse = null, Action<string> onError = null)
        {
            AssistantsAPI.ListThreadMessages(threadId, apiKey, onError, onResponse);
        }
    }
}
