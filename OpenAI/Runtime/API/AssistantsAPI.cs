using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace d4160.Runtime.OpenAI.API
{
    public static class AssistantsAPI
    {
        public static void CreateThread(string apiKey, Action<string> onError, Action<string> onSuccess)
        {
            string url = $"https://api.openai.com/v1/threads";

            WebRequests.Post(url,
            (UnityWebRequest unityWebRequest) =>
                {
                    unityWebRequest.SetRequestHeader("Authorization", $"Bearer {apiKey}");
                    unityWebRequest.SetRequestHeader("OpenAI-Beta", "assistants=v2");
                },
            (err) =>
            {
                Debug.Log(err);
                //ValidateError error = JsonUtility.FromJson<ValidateError>(err);
                onError?.Invoke(err);
            }, (res) =>
            {
                Debug.Log(res);
                //ValidateResponse response = JsonUtility.FromJson<ValidateResponse>(res);
                onSuccess?.Invoke(res);
            });
        }

        public static void AddThreadMessage(string threadId, string apiKey, AddThreadMessageRequest request, Action<string> onError, Action<string> onSuccess)
        {
            string url = $"https://api.openai.com/v1/threads/{threadId}/messages";

            //Debug.Log(url);

            WebRequests.PostJson(url, (webRequest) =>
            {
                webRequest.SetRequestHeader("Authorization", $"Bearer {apiKey}");
                webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v2");
            }, request.ToJson(),
            (err) =>
            {
                Debug.Log(err);
                //ValidateError error = JsonUtility.FromJson<ValidateError>(err);
                onError?.Invoke(err);
            }, (res) =>
            {
                Debug.Log(res);
                //ValidateResponse response = JsonUtility.FromJson<ValidateResponse>(res);
                onSuccess?.Invoke(res);
            });
        }

        public static void CreateThreadRun(string threadId, string apiKey, CreateThreadRunRequest request, Action<string> onError, Action<RunObject> onSuccess, bool test = false)
        {
            string url = $"https://api.openai.com/v1/threads/{threadId}/runs";

            WebRequests.PostJson(url, (webRequest) =>
            {
                webRequest.SetRequestHeader("Authorization", $"Bearer {apiKey}");
                webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v2");
            }, request.ToJson(), (err) =>
            {
                Debug.Log(err);
                //ValidateError error = JsonUtility.FromJson<ValidateError>(err);
                onError?.Invoke(err);
            }, (res) =>
            {
                Debug.Log(res);
                RunObject response = JsonUtility.FromJson<RunObject>(res);
                onSuccess?.Invoke(response);
            }, test);
        }

        public static void RetrieveThreadRun(string threadId, string runId, string apiKey, Action<RunObject> onSuccess, Action<string> onError)
        {
            string url = $"https://api.openai.com/v1/threads/{threadId}/runs/{runId}";

            WebRequests.Get(url, (webRequest) =>
            {
                webRequest.SetRequestHeader("Authorization", $"Bearer {apiKey}");
                webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v2");
            }, (err) =>
            {
                Debug.Log(err);
                //ValidateError error = JsonUtility.FromJson<ValidateError>(err);
                onError?.Invoke(err);
            }, (res) =>
            {
                Debug.Log(res);
                RunObject response = JsonUtility.FromJson<RunObject>(res);
                onSuccess?.Invoke(response);
            });
        }

        public static void ListThreadMessages(string threadId, string apiKey, string queryParams, Action<string> onError, Action<MessageListObject> onSuccess)
        {
            string url = $"https://api.openai.com/v1/threads/{threadId}/messages{queryParams}";

            WebRequests.Get(url, (webRequest) =>
            {
                webRequest.SetRequestHeader("Authorization", $"Bearer {apiKey}");
                webRequest.SetRequestHeader("OpenAI-Beta", "assistants=v2");
            }, (err) =>
            {
                Debug.Log(err);
                //ValidateError error = JsonUtility.FromJson<ValidateError>(err);
                onError?.Invoke(err);
            }, (res) =>
            {
                MessageListObject response = JsonUtility.FromJson<MessageListObject>(res);

                Debug.Log(JsonConvert.SerializeObject(response));

                onSuccess?.Invoke(response);
            });
        }
    }
}
