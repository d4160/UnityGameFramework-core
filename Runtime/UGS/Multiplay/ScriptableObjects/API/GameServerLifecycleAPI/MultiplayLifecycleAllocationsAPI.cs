using System;
using d4160.Logging;
using d4160.UGS.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace d4160.UGS.Multiplay.LifecycleAPI
{
    [CreateAssetMenu(menuName = "d4160/UGS/Multiplay/API/Lifecycle/Allocations")]
    public class MultiplayLifecycleAllocationsAPI : ScriptableObject
    {
        [SerializeField] private ProjectDataSO _projectData;
        [SerializeField] private MultiplayDataSO _multiplayData;
        [SerializeField] private TokenAPI _tokenAPI;
        [SerializeField] private TokenExchangeRequestSO _tokenExchangeRequest;
        [SerializeField] private LoggerSO _logger;

        public void QueueAllocation(QueueAllocationRequest request, Action<string> onResult = null, Action<string> onError = null)
        {
            _tokenAPI.TokenExchange(_tokenExchangeRequest.GetRequest(), (result) =>
            {
                QueueAllocationInternal(result, request, onResult, onError);
            });
        }

        private void QueueAllocationInternal(TokenExchangeResponse tokenExchangeResponse, QueueAllocationRequest request, Action<string> onResult, Action<string> onError = null)
        {
            string url = $"https://multiplay.services.api.unity.com/v1/allocations/projects/{_projectData.ProjectId}/environments/{_projectData.EnvironmentId}/fleets/{_multiplayData.FleetId}/allocations";

            if (_logger)
                _logger.LogInfo($"AllocationId: {request.allocationId}");

            WebRequests.PostJson(url,
            (UnityWebRequest unityWebRequest) =>
            {
                unityWebRequest.SetRequestHeader("Authorization", "Bearer " + tokenExchangeResponse.accessToken);
            },
            JsonUtility.ToJson(request),
            (string error) =>
            {
                if (_logger)
                    _logger.LogError("Error: " + error);

                onError?.Invoke(error);
            },
            (string json) =>
            {
                if (_logger)
                    _logger.LogInfo("Success: " + json);

                onResult?.Invoke(request.allocationId);
            });
        }

        public void RemoveAllocation(string allocationId, Action<string> onResult = null, Action<string> onError = null)
        {
            _tokenAPI.TokenExchange(_tokenExchangeRequest.GetRequest(), (result) =>
            {
                RemoveAllocationInternal(result, allocationId, onResult, onError);
            });
        }

        private void RemoveAllocationInternal(TokenExchangeResponse tokenExchangeResponse, string allocationId, Action<string> onResult, Action<string> onError = null)
        {
            string url = $"https://multiplay.services.api.unity.com/v1/allocations/projects/{_projectData.ProjectId}/environments/{_projectData.EnvironmentId}/fleets/{_multiplayData.FleetId}/allocations/{allocationId}";

            WebRequests.Delete(url,
            (UnityWebRequest unityWebRequest) =>
            {
                unityWebRequest.SetRequestHeader("Authorization", "Bearer " + tokenExchangeResponse.accessToken);
            },
            (string error) =>
            {
                if (_logger)
                    _logger.LogError("Error: " + error);

                onError?.Invoke(error);
            },
            (string json) =>
            {
                if (_logger)
                    _logger.LogInfo("Success: " + json);

                onResult?.Invoke(json);
            });
        }

        public void ListFleetAllocations(ListFleetAllocationsRequest request, Action<AllocationList> onResult = null, Action<string> onError = null)
        {
            _tokenAPI.TokenExchange(_tokenExchangeRequest.GetRequest(), (result) =>
            {
                ListFleetAllocationsInternal(result, request, onResult, onError);
            });
        }

        private void ListFleetAllocationsInternal(TokenExchangeResponse tokenExchangeResponse, ListFleetAllocationsRequest request, Action<AllocationList> onResult, Action<string> onError = null)
        {
            string url = $"https://multiplay.services.api.unity.com/v1/allocations/projects/{_projectData.ProjectId}/environments/{_projectData.EnvironmentId}/fleets/{_multiplayData.FleetId}/allocations{request.GetQueryParameters()}";

            // if (_multiplay)//
            //     _multiplay.LogInfo($"AllocationId: {request.allocationId}");

            WebRequests.Get(url,
            (UnityWebRequest unityWebRequest) =>
            {
                unityWebRequest.SetRequestHeader("Authorization", "Bearer " + tokenExchangeResponse.accessToken);
            },
            (string error) =>
            {
                if (_logger)
                    _logger.LogError("Error: " + error);

                onError?.Invoke(error);
            },
            (string json) =>
            {
                if (_logger)
                    _logger.LogInfo("Success: " + json);

                AllocationList allocationsList = JsonUtility.FromJson<AllocationList>(json);

                onResult?.Invoke(allocationsList);
            });
        }

        // // Learboards API
        // // TODO: Move to actual folder
        // public void GetScores(string leaderboardId, string jsonData, Action<string> onResult = null, Action<string> onError = null)
        // {
        //     _tokenAPI.TokenExchange(_tokenExchangeRequest.GetRequest(), (result) =>
        //     {
        //         GetScoresInternal(result, leaderboardId, jsonData, onResult, onError);
        //     });
        // }

        // private void GetScoresInternal(TokenExchangeResponse tokenExchangeResponse, string leaderboardId, string queryParams, Action<string> onResult, Action<string> onError = null)
        // {
        //     string url = $"leaderboards.services.api.unity.com/v1/projects/{_projectData.ProjectId}/environments/{_projectData.EnvironmentId}/leaderboards/{leaderboardId}/scores";

        //     Debug.Log(url);
        //     Debug.Log(queryParams);

        //     WebRequests.Get(url,
        //     (UnityWebRequest unityWebRequest) =>
        //     {
        //         unityWebRequest.SetRequestHeader("Authorization", "Bearer " + tokenExchangeResponse.accessToken);
        //     },
        //     (string error) =>
        //     {
        // if (_multiplay)//         
        //     _multiplay.LogError("Error: " + error);

        //         onError?.Invoke(error);
        //     },
        //     (string json) =>
        //     {
        // if (_multiplay)//         
        //     _multiplay.LogInfo("Success: " + json);

        //         onResult?.Invoke(json);
        //     });
        // }
    }
}
