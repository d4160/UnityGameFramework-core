using UnityEngine;

namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class RunObject
    {
        public string id;
        public string status;

        public bool IsDone => status == "completed" || status == "expired" || status == "cancelled" || status == "failed" || status == "incomplete";

        public bool IsCompleted => status == "completed";
    }
}