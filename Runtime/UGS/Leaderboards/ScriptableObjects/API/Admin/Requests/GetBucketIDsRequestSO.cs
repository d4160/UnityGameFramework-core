using System;
#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif
using UnityEngine;
using Newtonsoft.Json;

namespace d4160.UGS.Leaderboards.AdminAPI
{
    [CreateAssetMenu(menuName = "d4160/UGS/Leaderboards/API/Requests/GetBucketIds")]
    public class GetBucketIdsRequestSO : ScriptableObject
    {
        [SerializeField] private string _leaderboardId;
        [SerializeField] private int _offset = 0;
        [SerializeField] private int _limit = 10;

        [Space]

        [SerializeField] private LeaderboardsAdminAPI _api;

        private GetBucketIdsResponse _getBucketIdsResponse;

        public string LeaderboardId
        {
            get => _leaderboardId;
            set => _leaderboardId = value;
        }

        public GetBucketIdsResponse GetResponse() => _getBucketIdsResponse;

        public GetLeaderboardsAdminGenericRequest GetRequest()
        {
            return new GetLeaderboardsAdminGenericRequest(_leaderboardId, _offset, _limit);
        }

#if ENABLE_NAUGHTY_ATTRIBUTES
        [Button]
#endif
        public void SendRequest()
        {
            SendRequest(null);
        }

        public void SendRequest(Action<GetBucketIdsResponse> onResult, Action<string> onError = null)
        {
            _api.GetBucketIDs(GetRequest(), (response) =>
            {
                _getBucketIdsResponse = response;
                onResult?.Invoke(response);
            }, onError);
        }

#if ENABLE_NAUGHTY_ATTRIBUTES
        [Button]
#endif
        public void LogResponse()
        {
            var json = JsonConvert.SerializeObject(_getBucketIdsResponse);

            Debug.Log(json);
        }
    }

    [System.Serializable]
    public class GetBucketIdsResponse
    {
        public int offset;
        public int limit;
        public int total;
        public string[] results;
    }
}
