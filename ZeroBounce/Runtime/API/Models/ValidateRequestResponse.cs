using System;
using UnityEngine;

namespace d4160.Runtime.ZeroBounce.API
{
    [System.Serializable]
    public class ValidateRequest
    {
        public string email;
        public string ip_address;
        public string api_key;
        /// <summary>
        /// The duration (3 - 60 seconds) allowed for the validation. If met, the API will return unknown / greylisted. (optional parameter)
        /// </summary>
        [Tooltip("The duration (3 - 60 seconds) allowed for the validation. If met, the API will return unknown / greylisted. (optional parameter)")]
        public int? timeout;

        public ValidateRequest(string email, string ipAddress, string apiKey)
        {
            this.email = email;
            this.ip_address = ipAddress;
            this.api_key = apiKey;
        }
    }

    [System.Serializable]
    public class ValidateResponse
    {
        public const string Valid = "valid";
        public const string Temp = "do_not_mail";
        public const string Invalid = "invalid";

        public string address;
        public string status; // valid
        public string sub_status; // alternate
        public bool free_email;
        public string did_you_mean;
        public string account;
        public string domain;
        public int domain_age_days;
        public string smtp_provider;
        public bool mx_found;
        public string mx_record;
        public string firstname;
        public string lastname;
        public string gender;
        public string country;
        public string region;
        public string city;
        public string zipcode;
        public DateTime processed_at;
    }

    [System.Serializable]
    public class ValidateError
    {
        public string error;
    }
}