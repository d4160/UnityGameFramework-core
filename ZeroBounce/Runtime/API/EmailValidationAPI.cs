using System;
using UnityEngine;

namespace d4160.Runtime.ZeroBounce.API
{
    public static class EmailValidationAPI
    {
        public static void Validate(ValidateRequest request, Action<ValidateError> onError, Action<ValidateResponse> onSuccess)
        {
            string url = $"https://api.zerobounce.net/v2/validate?api_key={request.api_key}&email={request.email}&ip_address={request.ip_address}";

            //Debug.Log(url);

            WebRequests.Get(url, (err) =>
            {
                //Debug.Log(err);
                ValidateError error = JsonUtility.FromJson<ValidateError>(err);
                onError?.Invoke(error);
            }, (res) =>
            {
                //Debug.Log(res);
                ValidateResponse response = JsonUtility.FromJson<ValidateResponse>(res);
                onSuccess?.Invoke(response);
            });
        }
    }
}