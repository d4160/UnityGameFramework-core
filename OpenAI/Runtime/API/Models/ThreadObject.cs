using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class ThreadObject
    {
        public string id;

        public static ThreadObject FromJson(string json)
        {
            return JsonUtility.FromJson<ThreadObject>(json);
        }
    }
}