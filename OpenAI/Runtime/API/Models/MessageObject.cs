using Newtonsoft.Json;
using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    public enum MessageRole
    {
        none,
        assistant,
        user
    }

    [System.Serializable]
    public class MessageObject
    {
        public string id;
        public int created_at;
        public string assistant_id;
        public string thread_id;
        public string run_id;
        public string role;
        public MessageContent[] content; // Single object
    }

    [System.Serializable]
    public class MessageDeltaObject
    {
        public MessageContent[] content; // Single object
    }

    [System.Serializable]
    public class MessageStreamObject
    {
        public string id;
        //[JsonProperty("object")]
        //public string objectProp;
        public string status = "none";
        public MessageContent[] content = null;
        public MessageDeltaObject delta = null;

        public static MessageStreamObject FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MessageStreamObject>(json);
        }
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
