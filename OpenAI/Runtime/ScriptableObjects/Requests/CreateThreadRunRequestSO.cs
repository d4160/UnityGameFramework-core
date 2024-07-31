using d4160.Runtime.OpenAI.API;
using UnityEngine;
using System;
using System.Collections;
using d4160.Coroutines;



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

        public string ThreadId { get => _threadId; set => _threadId = value; }

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
                //WebRequests.StartCoroutine
                PollAndList(res.id);
            }, (err) => { }, true);
        }

        public void PollAndList(string runId, Action<MessageListObject> onResponse = null, Action<string> onError = null)
        {
            if (_waitForSeconds == null)
            {
                _waitForSeconds = new WaitForSeconds(_settingsSO.ChecksDelay);
            }

            WebRequests.StartCoroutine(RetrieveThreadRunRequest(runId, onResponse, onError));
        }

        private IEnumerator RetrieveThreadRunRequest(string runId, Action<MessageListObject> onResponse = null, Action<string> onError = null)
        {
            yield return _waitForSeconds;

            _retrieveRunRequest.SendRequest(runId, (response) =>
            {
                Debug.Log(response.status);

                if (response.IsDone)
                {
                    if (response.IsCompleted)
                    {
                        _listMessagesRequest.QueryParams.run_id = runId;
                        _listMessagesRequest.SendRequest(onResponse, onError);
                    }
                }
                else
                {
                    WebRequests.StartCoroutine(RetrieveThreadRunRequest(runId, onResponse, onError));
                }
            });
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
