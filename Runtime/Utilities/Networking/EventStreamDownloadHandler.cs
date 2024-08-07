using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace d4160.Runtime.Networking
{
    public class EventStreamDownloadHandler : DownloadHandlerScript
    {
        private Action<string> _onDataReceived;
        private StringBuilder _dataBuffer = new StringBuilder();

        public EventStreamDownloadHandler(Action<string> onDataReceived)
        {
            _onDataReceived = onDataReceived;
        }

        protected override bool ReceiveData(byte[] data, int dataLength)
        {
            if (data == null || data.Length < 1)
            {
                Debug.Log("No data received!");
                return false;
            }

            string receivedData = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
            _dataBuffer.Append(receivedData);

            // Process the buffered data line by line
            while (true)
            {
                string line;
                int newLineIndex = _dataBuffer.ToString().IndexOf('\n');
                if (newLineIndex >= 0)
                {
                    line = _dataBuffer.ToString().Substring(0, newLineIndex);
                    _dataBuffer.Remove(0, newLineIndex + 1);
                }
                else
                {
                    break;
                }

                if (!string.IsNullOrEmpty(line))
                {
                    _onDataReceived(line);
                }
            }

            return true;
        }
    }
}