using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class CreateThreadRunRequest
    {
        public string assistant_id;
        [TextArea]
        public string instructions;
        public bool stream;

        public CreateThreadRunRequest(string assistant_id, string instructions, bool stream)
        {
            this.instructions = instructions;
            this.assistant_id = assistant_id;
            this.stream = stream;
        }

        public string ToJson() => JsonUtility.ToJson(this);
    }

    [System.Serializable]
    public class CreateThreadRunResponse
    {

    }
}