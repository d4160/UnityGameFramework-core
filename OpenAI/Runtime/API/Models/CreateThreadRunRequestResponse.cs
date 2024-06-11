using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class CreateThreadRunRequest
    {
        public string assistant_id;
        [TextArea]
        public string instructions;

        public CreateThreadRunRequest(string assistant_id, string instructions)
        {
            this.instructions = instructions;
            this.assistant_id = assistant_id;
        }

        public string ToJson() => JsonUtility.ToJson(this);
    }

    [System.Serializable]
    public class CreateThreadRunResponse
    {

    }
}