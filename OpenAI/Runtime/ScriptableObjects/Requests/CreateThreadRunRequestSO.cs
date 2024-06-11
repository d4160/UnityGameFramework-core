using d4160.Runtime.OpenAI.API;
using UnityEngine;
using System;
using System.Collections;


#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace d4160.Runtime.OpenAI.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CreateThreadRunRequest", menuName = "d4160/OpenAI/Requests/CreateThreadRun")]
    public class CreateThreadRunRequestSO : ScriptableObject
    {
        [SerializeField] private OpenAISettingsSO _settingsSO;
        [SerializeField] private RetrieveThreadRunRequestSO _retrieveRunRequest;
        [SerializeField] private ListThreadMessagesRequestSO _listMessagesRequest;

        [Space]

        [SerializeField] private string _threadId;
        [SerializeField] private string _assistant_id;
        [TextArea]
        [SerializeField] private string _instructions;

        public CreateThreadRunRequest GetRequest() => new(_assistant_id, _instructions);

        private WaitForSeconds _waitForSeconds;
        private bool _checkingRun;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SendRequest()
        {
            SendRequest(_threadId, _settingsSO.ApiKey, (res) =>
            {
                _checkingRun = true;
                WebRequests.StartCoroutine(PollAndListCo(res.id));
            }, (err) => { }, true);
        }

        private IEnumerator PollAndListCo(string runId)
        {
            if (_waitForSeconds == null)
            {
                _waitForSeconds = new WaitForSeconds(_settingsSO.ChecksDelay);
            }

            while (_checkingRun)
            {
                yield return _waitForSeconds;

                _retrieveRunRequest.SendRequest(runId, (response) =>
                {
                    if (response.IsDone)
                    {
                        _checkingRun = false;

                        if (response.IsCompleted)
                        {
                            _listMessagesRequest.SendRequest();
                        }
                    }
                });
            }
        }

        public void SendRequest(string threadId, Action<RunObject> onResponse, Action<string> onError = null)
        {
            SendRequest(threadId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, string assistantId, string instructions, Action<RunObject> onResponse, Action<string> onError = null)
        {
            SendRequest(threadId, _settingsSO.ApiKey, assistantId, instructions, onResponse, onError);
        }

        public void SendRequestWithAssistant(string assistantId, string instructions, Action<RunObject> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _settingsSO.ApiKey, assistantId, instructions, onResponse, onError);
        }

        public void SendRequest(Action<RunObject> onResponse, Action<string> onError = null)
        {
            SendRequest(_threadId, _settingsSO.ApiKey, onResponse, onError);
        }

        public void SendRequest(string threadId, string apiKey, Action<RunObject> onResponse, Action<string> onError = null, bool test = false)
        {
            AssistantsAPI.CreateThreadRun(threadId, apiKey, GetRequest(), onError, onResponse, test);
        }

        public void SendRequest(string threadId, string apiKey, string assistantId, string instructions, Action<RunObject> onResponse, Action<string> onError = null)
        {
            AssistantsAPI.CreateThreadRun(threadId, apiKey, new CreateThreadRunRequest(assistantId, instructions), onError, onResponse);
        }
    }
}
