﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using d4160.Runtime.Networking;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequests
{

    private class WebRequestsMonoBehaviour : MonoBehaviour
    {
        void Awake()
        {
#if UNITY_EDITOR
            gameObject.hideFlags = HideFlags.HideAndDontSave
                                | HideFlags.HideInInspector;
#endif
        }
    }

    private static WebRequestsMonoBehaviour webRequestsMonoBehaviour;

    private static void Init()
    {
        if (webRequestsMonoBehaviour == null)
        {
            GameObject gameObject = new GameObject("WebRequests");
            webRequestsMonoBehaviour = gameObject.AddComponent<WebRequestsMonoBehaviour>();
        }
    }

    public static void StartCoroutine(IEnumerator enumerator)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(enumerator);
    }

    public static void Get(string url, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutine(url, null, onError, onSuccess));
    }

    public static void Get(string url, Action<UnityWebRequest> setHeaderAction, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutine(url, setHeaderAction, onError, onSuccess));
    }

    private static IEnumerator GetCoroutine(string url, Action<UnityWebRequest> setHeaderAction, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(url))
        {
            if (setHeaderAction != null)
            {
                setHeaderAction(unityWebRequest);
            }
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

    public static void Post(string url, Dictionary<string, string> formFields, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePost(url, formFields, onError, onSuccess));
    }

    public static void Post(string url, string postData, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePost(url, postData, onError, onSuccess));
    }

    public static void Post(string url, Action<UnityWebRequest> setHeaderAction, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePost(url, setHeaderAction, onError, onSuccess));
    }

    public static void PostJson(string url, string jsonData, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePostJson(url, null, jsonData, onError, onSuccess));
    }

    public static void PostJson(string url, Action<UnityWebRequest> setHeaderAction, string jsonData, Action<string> onError, Action<string> onSuccess, bool test = false)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePostJson(url, setHeaderAction, jsonData, onError, onSuccess, test));
    }

    public static void PostJsonWithStream(string url, Action<UnityWebRequest> setHeaderAction, string jsonData, Action<string> onError, Action<string> onSuccess, Action<string> onStreamEvent)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePostJsonEventStream(url, setHeaderAction, jsonData, onError, onSuccess, onStreamEvent));
    }

    private static IEnumerator GetCoroutinePost(string url, Action<UnityWebRequest> setHeaderAction, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = new UnityWebRequest(url, "POST"))
        {
            unityWebRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            unityWebRequest.SetRequestHeader("Content-Type", "application/json");

            if (setHeaderAction != null)
            {
                setHeaderAction(unityWebRequest);
            }

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

    private static IEnumerator GetCoroutinePost(string url, Dictionary<string, string> formFields, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, formFields))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

    private static IEnumerator GetCoroutinePost(string url, string postData, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.PostWwwForm(url, postData))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

    private static UnityWebRequest CurrentRequest;

    private static IEnumerator GetCoroutinePostJson(string url, Action<UnityWebRequest> setHeaderAction, string jsonData, Action<string> onError, Action<string> onSuccess, bool test = false)
    {
        using (UnityWebRequest unityWebRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            unityWebRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            unityWebRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            unityWebRequest.SetRequestHeader("Content-Type", "application/json");

            if (setHeaderAction != null)
            {
                setHeaderAction(unityWebRequest);
            }

            yield return unityWebRequest.SendWebRequest();

            //Debug.Log($"Is done: {unityWebRequest.isDone}");

#if UNITY_EDITOR
            if (test)
            {
                CurrentRequest = unityWebRequest;
                UnityEditor.EditorApplication.update += CheckRequestProgress;
            }
#endif

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

    private static IEnumerator GetCoroutinePostJsonEventStream(string url, Action<UnityWebRequest> setHeaderAction, string jsonData, Action<string> onError, Action<string> onSuccess, Action<string> onStreamEvent)
    {
        using (UnityWebRequest unityWebRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            unityWebRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            //unityWebRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            EventStreamDownloadHandler downloadHandler = new EventStreamDownloadHandler((receivedData) =>
            {
                onStreamEvent?.Invoke(receivedData);
            });

            unityWebRequest.downloadHandler = downloadHandler;

            unityWebRequest.SetRequestHeader("Content-Type", "application/json");

            if (setHeaderAction != null)
            {
                setHeaderAction(unityWebRequest);
            }

            yield return unityWebRequest.SendWebRequest();

            Debug.Log($"Is done: {unityWebRequest.isDone}");

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                Debug.Log(unityWebRequest.downloadHandler.text);
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

    [System.Serializable]
    private class ResponseTest
    {
        public string id;
        public string status;
    }

#if UNITY_EDITOR
    private static void CheckRequestProgress()
    {
        if (CurrentRequest != null)
        {
            try
            {
                ResponseTest obj = JsonUtility.FromJson<ResponseTest>(CurrentRequest.downloadHandler.text);

                Debug.Log(obj.status);

                if (obj.status != "queued")
                {
                    UnityEditor.EditorApplication.update -= CheckRequestProgress;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error: {e.Message}");
                UnityEditor.EditorApplication.update -= CheckRequestProgress;
            }

        }
    }
#endif

    public static void Put(string url, string bodyData, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePut(url, bodyData, onError, onSuccess));
    }

    public static void PutJson(string url, Action<UnityWebRequest> setHeaderAction, string jsonData, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePutJson(url, setHeaderAction, jsonData, onError, onSuccess));
    }

    private static IEnumerator GetCoroutinePutJson(string url, Action<UnityWebRequest> setHeaderAction, string jsonData, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = new UnityWebRequest(url, "PUT"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            unityWebRequest.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            unityWebRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            unityWebRequest.SetRequestHeader("Content-Type", "application/json");

            if (setHeaderAction != null)
            {
                setHeaderAction(unityWebRequest);
            }

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
                onError(unityWebRequest.downloadHandler.text);
            }
            else
            {
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

    private static IEnumerator GetCoroutinePut(string url, string bodyData, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Put(url, bodyData))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                onSuccess(unityWebRequest.downloadHandler.text);
            }
        }
    }

    public static void Delete(string url, Action<UnityWebRequest> setHeaderAction, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutineDelete(url, setHeaderAction, onError, onSuccess));
    }

    private static IEnumerator GetCoroutineDelete(string url, Action<UnityWebRequest> setHeaderAction, Action<string> onError, Action<string> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = new UnityWebRequest(url, "DELETE"))
        {
            if (setHeaderAction != null)
            {
                setHeaderAction(unityWebRequest);
            }

            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                onSuccess(unityWebRequest.result.ToString());
            }
        }
    }

    public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator GetTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return unityWebRequest.SendWebRequest();

            if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                // Error
                onError(unityWebRequest.error);
            }
            else
            {
                DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
                onSuccess(downloadHandlerTexture.texture);
            }
        }
    }

}
