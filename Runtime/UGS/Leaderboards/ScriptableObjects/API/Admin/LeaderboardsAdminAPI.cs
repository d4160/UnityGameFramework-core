using System;
using d4160.Logging;
using d4160.UGS.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace d4160.UGS.Leaderboards.AdminAPI
{
    [CreateAssetMenu(fileName = "LeaderboardsAdminAPI", menuName = "d4160/UGS/Leaderboards/API/LeaderboardsAdmin")]
    public class LeaderboardsAdminAPI : ScriptableObject
    {
        [SerializeField] private ServiceAccountSO _serviceAccount;
        [SerializeField] private ProjectDataSO _projectData;
        [SerializeField] private LoggerSO _logger;

        public void GetScores(GetLeaderboardsAdminGenericRequest request, Action<string> onResult, Action<string> onError = null)
        {
            string url = $"https://services.api.unity.com/leaderboards/v1/projects/{_projectData.ProjectId}/environments/{_projectData.EnvironmentId}/leaderboards/{request.leaderboardId}/scores{request.GetQueryParameters()}";

            // Debug.Log(url);
            // Debug.Log(_serviceAccount.KeyBase64);

            WebRequests.Get(url,
            (UnityWebRequest unityWebRequest) =>
            {
                unityWebRequest.SetRequestHeader("Authorization", "Basic " + _serviceAccount.KeyBase64);
            },
            (string error) =>
            {
                _logger.LogError("Error: " + error);

                onError?.Invoke(error);
            },
            (string json) =>
            {
                _logger.LogInfo("Success: " + json);
                // ServerList serverList = JsonUtility.FromJson<ServerList>("{\"serverList\":" + json + "}");

                onResult?.Invoke(json);
            });
        }

        public void GetBucketIDs(GetLeaderboardsAdminGenericRequest request, Action<GetBucketIdsResponse> onResult, Action<string> onError = null)
        {
            string url = $"https://services.api.unity.com/leaderboards/v1/projects/{_projectData.ProjectId}/environments/{_projectData.EnvironmentId}/leaderboards/{request.leaderboardId}/buckets{request.GetQueryParameters()}";

            // Debug.Log(url);
            // Debug.Log(_serviceAccount.KeyBase64);

            WebRequests.Get(url,
            (UnityWebRequest unityWebRequest) =>
            {
                unityWebRequest.SetRequestHeader("Authorization", "Basic " + _serviceAccount.KeyBase64);
            },
            (string error) =>
            {
                _logger.LogError("Error: " + error);

                onError?.Invoke(error);
            },
            (string json) =>
            {
                _logger.LogInfo("Success: " + json);
                GetBucketIdsResponse response = JsonUtility.FromJson<GetBucketIdsResponse>(json);
                // {\"serverList\":" + json + "}

                onResult?.Invoke(response);
            });
        }

        public void GetBucketScores(GetBucketScoresRequest request, Action<GetBucketScoresResponse> onResult, Action<string> onError = null)
        {
            string url = $"https://services.api.unity.com/leaderboards/v1/projects/{_projectData.ProjectId}/environments/{_projectData.EnvironmentId}/leaderboards/{request.leaderboardId}/buckets/{request.bucketId}/scores{request.GetQueryParameters()}";

            // Debug.Log(url);
            // Debug.Log(_serviceAccount.KeyBase64);

            WebRequests.Get(url,
            (UnityWebRequest unityWebRequest) =>
            {
                unityWebRequest.SetRequestHeader("Authorization", "Basic " + _serviceAccount.KeyBase64);
            },
            (string error) =>
            {
                _logger.LogError("Error: " + error);

                onError?.Invoke(error);
            },
            (string json) =>
            {
                _logger.LogInfo("Success: " + json);
                GetBucketScoresResponse response = JsonUtility.FromJson<GetBucketScoresResponse>(json);
                // {\"serverList\":" + json + "}

                onResult?.Invoke(response);
            });
        }
    }
}