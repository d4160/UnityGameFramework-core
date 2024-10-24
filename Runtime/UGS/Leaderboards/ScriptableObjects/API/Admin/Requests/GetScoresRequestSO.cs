using System;
#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif
using UnityEngine;

namespace d4160.UGS.Leaderboards.AdminAPI
{
    [CreateAssetMenu(menuName = "d4160/UGS/Leaderboards/API/Requests/GetScores")]
    public class GetScoresRequestSO : ScriptableObject
    {
        [SerializeField] private string _leaderboardId;
        [SerializeField] private int _offset = 0;
        [SerializeField] private int _limit = 10;

        [Space]

        [SerializeField] private LeaderboardsAdminAPI _api;

        public string LeaderboardId
        {
            get => _leaderboardId;
            set => _leaderboardId = value;
        }

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

        public void SendRequest(Action<string> onResult, Action<string> onError = null)
        {
            _api.GetScores(GetRequest(), onResult, onError);
        }
    }

    [Serializable]
    public class GetLeaderboardsAdminGenericRequest
    {
        public string leaderboardId;
        public int offset;
        public int limit;

        public GetLeaderboardsAdminGenericRequest(string leaderboardId, int offset, int limit)
        {
            this.leaderboardId = leaderboardId;
            this.offset = offset;
            this.limit = limit;
        }

        public string GetQueryParameters()
        {
            return $"?offset={offset}&limit={limit}";
        }
    }
}
