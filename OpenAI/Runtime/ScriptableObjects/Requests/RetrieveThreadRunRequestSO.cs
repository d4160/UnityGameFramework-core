using UnityEngine;
using System;
using d4160.Runtime.OpenAI.API;


#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace d4160.Runtime.OpenAI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RetrieveThreadRunRequest", menuName = "d4160/OpenAI/Requests/RetrieveThreadRun")]
    public class RetrieveThreadRunRequestSO : ScriptableObject
    {
        [SerializeField] private OpenAISettingsSO _settingsSO;

        [Space]

        [SerializeField] private string _threadId;
        [SerializeField] private string _runId;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SendRequest()
        {
            SendRequest(null, null);
        }

        public void SendRequest(Action<RunObject> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _runId, _settingsSO.ApiKey, null);
        }

        public void SendRequest(string runId, Action<RunObject> onResponse = null, Action<string> onError = null)
        {
            SendRequest(_threadId, runId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, string runId, Action<RunObject> onResponse = null, Action<string> onError = null)
        {
            SendRequest(threadId, runId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, string runId, string apiKey, Action<RunObject> onResponse = null, Action<string> onError = null)
        {
            AssistantsAPI.RetrieveThreadRun(threadId, runId, apiKey, onResponse, onError);
        }
    }
}
