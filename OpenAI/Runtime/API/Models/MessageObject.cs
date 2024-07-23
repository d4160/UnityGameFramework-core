using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class MessageObject
    {
        public string id;
        public int created_at;
        public string assistant_id;
        public string thread_id;
        public string run_id;
        public string role;
        public MessageContent[] content;
    }

    [System.Serializable]
    public class MessageContent
    {
        public string type;
        public MessageContentText text;
    }

    [System.Serializable]
    public class MessageContentText
    {
        public string value;
    }
}
