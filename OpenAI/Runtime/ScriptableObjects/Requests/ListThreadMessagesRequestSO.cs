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

        [Space]

        [SerializeField] private ListMessagesQueryParams _queryParams;

        public ListMessagesQueryParams QueryParams => _queryParams;

        public string ThreadId { get => _threadId; set => _threadId = value; }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SendRequest()
        {
            SendRequest(_threadId, _settingsSO.ApiKey);
        }

        public void SendRequest(Action<MessageListObject> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, Action<MessageListObject> onResponse = null, Action<string> onError = null)
        {
            SendRequest(threadId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, string apiKey, Action<MessageListObject> onResponse = null, Action<string> onError = null)
        {
            AssistantsAPI.ListThreadMessages(threadId, apiKey, _queryParams.GetString(), onError, onResponse);
        }
    }

    [System.Serializable]
    public class ListMessagesQueryParams
    {
        [Range(0, 100)]
        public int limit = 20;
#if ODIN_INSPECTOR
        [ValueDropdown("_OrderOptions")]
#endif
        public string order;
        public string after;
        public string before;
        public string run_id;

        private string[] _OrderOptions = new string[] { "", "asc", "desc" };

        public string GetString(bool appendStart = true)
        {
            return $"{(appendStart ? "?" : "")}{(limit <= 0 ? "" : $"limit={limit}")}{(string.IsNullOrEmpty(order) ? "" : $"&order={order}")}{(string.IsNullOrEmpty(after) ? "" : $"&after={after}")}{(string.IsNullOrEmpty(before) ? "" : $"&before={before}")}{(string.IsNullOrEmpty(run_id) ? "" : $"&run_id={run_id}")}";
        }
    }
}
