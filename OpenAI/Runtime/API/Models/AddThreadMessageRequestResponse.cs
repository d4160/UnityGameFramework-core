using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class AddThreadMessageRequest
    {
        public string role;
        public string content;

        public AddThreadMessageRequest(string role, string content)
        {
            this.role = role;
            this.content = content;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }

    [System.Serializable]
    public class AddThreadMessageResponse
    {
    }
}