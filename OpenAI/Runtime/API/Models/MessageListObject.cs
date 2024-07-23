using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class MessageListObject
    {
        public MessageObject[] data;
        public string first_id;
        public string last_id;
        public bool has_more;
    }
}