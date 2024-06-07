using System;
using System.Collections;
using System.Collections.Generic;
using d4160.Logging;
using d4160.Runtime.ZeroBounce.API;
using Newtonsoft.Json;
using d4160.Variables;


#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif
using UnityEngine;

namespace d4160.Runtime.ZeroBounce.ScriptableObjects
{
    [CreateAssetMenu(menuName = "d4160/ZeroBounce/Requests/Validate")]
    public class ValidateRequestSO : ScriptableObject
    {
        [SerializeField] private StringReference _email;
        [SerializeField] private string _ipAddress;
        [SerializeField] private string _apiKey;

        [Header("Debug")]
        [SerializeField] private LoggerSO _logger;

        public string Email
        {
            get => _email;
            set => _email.Value = value;
        }

        private ValidateRequest GetRequest()
        {
            return new ValidateRequest(_email, _ipAddress, _apiKey);
        }

#if ENABLE_NAUGHTY_ATTRIBUTES
        [Button]
#endif
        public void SendRequest()
        {
            SendRequest(null);
        }

        public void SendRequest(Action<ValidateResponse> onResponse, Action<ValidateError> onError = null)
        {
            EmailValidationAPI.Validate(GetRequest(), (error) =>
            {
                if (_logger) _logger.LogError(error.error);
                onError?.Invoke(error);
            }, (response) =>
            {
                if (_logger) _logger.LogInfo(JsonConvert.SerializeObject(response));
                onResponse?.Invoke(response);
            });
        }
    }
}