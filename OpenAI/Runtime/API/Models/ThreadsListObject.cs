using System.Collections.Generic;
using UnityEngine;
namespace d4160.Runtime.OpenAI.API
{
    [System.Serializable]
    public class ThreadsListObject
    {
        public List<ThreadsObject> threads = new();

        public string[] GetThreadIds(int categoryId = 1)
        {
            for (int i = 0; i < threads.Count; i++)
            {
                if (threads[i].categoryId == categoryId)
                {
                    return threads[i].GetThreadIds();
                }
            }

            return null;
        }

        public void AddThreadId(string threadId, int categoryId = 1)
        {
            bool found = false;
            for (int i = 0; i < threads.Count; i++)
            {
                if (threads[i].categoryId == categoryId)
                {
                    threads[i].AddThreadId(threadId);
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                threads.Add(new()
                {
                    categoryId = categoryId,
                    threads = threadId
                });
            }
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static ThreadsListObject FromJson(string json)
        {
            return JsonUtility.FromJson<ThreadsListObject>(json);
        }
    }

    [System.Serializable]
    public class ThreadsObject
    {
        public int categoryId;
        public string threads = string.Empty; // Separated by commas

        public string[] GetThreadIds()
        {
            return threads.Split(',');
        }

        public void AddThreadId(string threadId)
        {
            if (!threads.Contains(threadId))
            {
                threads += string.IsNullOrEmpty(threads) ? threadId : $",{threadId}";
            }
        }
    }
}
