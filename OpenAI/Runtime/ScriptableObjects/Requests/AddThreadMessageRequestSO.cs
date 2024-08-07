using d4160.Runtime.OpenAI.API;
using UnityEngine;
using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace d4160.Runtime.OpenAI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AddThreadMessageRequest", menuName = "d4160/OpenAI/Requests/AddThreadMessage")]
    public class AddThreadMessageRequestSO : ScriptableObject
    {
        [SerializeField] private OpenAISettingsSO _settingsSO;

        [Space]

        [SerializeField] private string _threadId;
        [SerializeField] private string _role;
        [TextArea]
        [SerializeField] private string _content;

        public string ThreadId { get => _threadId; set => _threadId = value; }

        public AddThreadMessageRequest GetRequest() => new(_role, _content);

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SendRequest()
        {
            AssistantsAPI.AddThreadMessage(_threadId, _settingsSO.ApiKey, GetRequest(), null, null);
        }

        public void SendRequest(string threadId, Action<string> onResponse, Action<string> onError = null)
        {
            SendRequest(threadId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, string role, string content, Action<string> onResponse, Action<string> onError = null)
        {
            SendRequest(threadId, _settingsSO.ApiKey, role, content, onResponse, onError);
        }

        public void SendRequestWithContent(string content, Action<string> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _settingsSO.ApiKey, _role, content, onResponse, onError);
        }

        public void SendRequestWithContent(string role, string content, Action<string> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _settingsSO.ApiKey, role, content, onResponse, onError);
        }

        public void SendRequest(Action<string> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, string apiKey, Action<string> onResponse, Action<string> onError = null)
        {
            AssistantsAPI.AddThreadMessage(threadId, apiKey, GetRequest(), onError, onResponse);
        }

        public void SendRequest(string threadId, string apiKey, string role, string content, Action<string> onResponse, Action<string> onError = null)
        {
            AssistantsAPI.AddThreadMessage(threadId, apiKey, new AddThreadMessageRequest(role, content), onError, onResponse);
        }
    }
}