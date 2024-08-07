using System;
using d4160.Collections;
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

        public string GetMessageContentTextValue(int index, out MessageRole role)
        {
            role = default;
            if (data.IsValidIndex(index))
            {
                role = GetMessageRole(data[index].role);

                if (data[index].content.Length > 0)
                {
                    if (data[index].content[0].text != null)
                    {
                        return data[index].content[0].text.value;
                    }
                    else
                    {
                        Debug.LogError($"MessageContentText object is null at index {index}");

                        return string.Empty;
                    }
                }
                else
                {
                    Debug.LogError($"Not exists any MessageContent object to use at index {index}");

                    return string.Empty;
                }
            }
            else
            {
                Debug.LogError($"Not exists MessageObject at index {index}");

                return string.Empty;
            }
        }

        public static MessageRole GetMessageRole(string value)
        {
            MessageRole role;
            if (Enum.TryParse(value, out role))
            {
                return role;
            }
            else
            {
                Debug.LogError($"Failed to convert '{value}' to an MessageRole enum.");

                return default;
            }
        }
    }
}