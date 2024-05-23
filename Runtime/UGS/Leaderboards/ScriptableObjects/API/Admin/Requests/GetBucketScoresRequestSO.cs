using System;
using NaughtyAttributes;
using UnityEngine;
using Newtonsoft.Json;

namespace d4160.UGS.Leaderboards.AdminAPI
{
    [CreateAssetMenu(menuName = "d4160/UGS/Leaderboards/API/Requests/GetBucketScores")]
    public class GetBucketScoresRequestSO : ScriptableObject
    {
        [SerializeField] private string _leaderboardId;
        [SerializeField] private string _bucketId;
        [SerializeField] private int _offset = 0;
        [SerializeField] private int _limit = 10;

        [Space]

        [SerializeField] private LeaderboardsAdminAPI _api;

        private GetBucketScoresResponse _getBucketScoresResponse;

        public string LeaderboardId
        {
            get => _leaderboardId;
            set => _leaderboardId = value;
        }

        public string BucketId
        {
            get => _bucketId;
            set => _bucketId = value;
        }

        public GetBucketScoresResponse GetResponse() => _getBucketScoresResponse;

        public GetBucketScoresRequest GetRequest()
        {
            return new GetBucketScoresRequest(_leaderboardId, _bucketId, _offset, _limit);
        }

        [Button]
        public void SendRequest()
        {
            SendRequest(null);
        }

        public void SendRequest(Action<GetBucketScoresResponse> onResult, Action<string> onError = null)
        {
            _api.GetBucketScores(GetRequest(), (response) =>
            {
                _getBucketScoresResponse = response;
                onResult?.Invoke(response);
            }, onError);
        }

        [Button]
        public void LogResponse()
        {
            var json = JsonConvert.SerializeObject(_getBucketScoresResponse);

            Debug.Log(json);
        }
    }

    [Serializable]
    public class GetBucketScoresRequest
    {
        public string leaderboardId;
        public string bucketId;
        public int offset;
        public int limit;

        public GetBucketScoresRequest(string leaderboardId, string bucketId, int offset, int limit)
        {
            this.leaderboardId = leaderboardId;
            this.bucketId = bucketId;
            this.offset = offset;
            this.limit = limit;
        }

        public string GetQueryParameters()
        {
            return $"?offset={offset}&limit={limit}";
        }
    }

    [Serializable]
    public class GetBucketScoresResponse
    {
        public int offset;
        public int limit;
        public int total;
        public BucketScore[] results;
    }

    [Serializable]
    public class BucketScore
    {
        public string playerId;
        public string playerName;
        public float score;
        public int rank;
        public string tier;
    }
}
