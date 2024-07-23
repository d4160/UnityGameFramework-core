#if DEDICATED_SERVER
using System.Collections;
using d4160.Events;
using d4160.Loops;
using d4160.Singleton;
using d4160.UGS.Multiplay.LifecycleAPI;
#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif
using Unity.Netcode;
using Unity.Services.Core;
using Unity.Services.Multiplay;
using UnityEngine;

namespace d4160.UGS.Multiplay
{
    public class MultiplayServiceMono : Singleton<MultiplayServiceMono>, IUpdateObject
    {
        [SerializeField] private bool _startServerAtStart;
        [SerializeField] private bool _setReadyServerForPlayersOnServerStarted;
        [SerializeField, Tooltip("The time to wait until remove allocation when nobody is in the server")] private float _autoRemoveAllocationMaxTime = 31f;

        [Header("Data")]
#if ENABLE_NAUGHTY_ATTRIBUTES
        [Expandable]
#endif
        [SerializeField] private MultiplaySO _multiplay;
        [SerializeField] private RemoveAllocationRequestSO _removeAllocReq;

        private float _autoAllocateTimer = 9999999f;
        private float _autoRemoveAllocationTimer = 0;
        private bool _isSentRemoveAllocationRequest = false;
        private VoidEventSO.EventListener _onServerStartedLtn;

        protected override void Awake()
        {
            base.Awake();

            _onServerStartedLtn = new(async () =>
            {
                if (_setReadyServerForPlayersOnServerStarted)
                {
                    await MultiplayService.Instance.ReadyServerForPlayersAsync();

                    _multiplay.LogInfo($"ReadyServerForPlayers was called");
                }

                Camera.main.enabled = false;
            });
        }

        protected void OnEnable()
        {
            _autoRemoveAllocationTimer = _autoRemoveAllocationMaxTime;

            UpdateManager.AddListener(this);

            _multiplay.OnServerStarted.AddListener(_onServerStartedLtn);
        }

        protected void OnDisable()
        {
            UpdateManager.RemoveListener(this);

            _multiplay.OnServerStarted.RemoveListener(_onServerStartedLtn);
        }

        private IEnumerator Start()
        {
            while (UnityServices.State != ServicesInitializationState.Initialized)
            {
                yield return null;
            }

            if (_startServerAtStart)
            {
                _multiplay.SubscribeEvents();
                _ = _multiplay.StartServerAsync();
            }
        }

        void OnDestroy()
        {
            if (_startServerAtStart)
            {
                _multiplay.UnsubscribeEvents();
            }
        }

        void IUpdateObject.OnUpdate(float deltaTime)
        {
            _autoAllocateTimer -= deltaTime;
            if (_autoAllocateTimer <= 0)
            {
                _autoAllocateTimer = 999f;
                Debug.Log("AutoAllocateTimer time out!");
            }

            if (_multiplay.ServerQueryHandler != null)
            {
                if (NetworkManager.Singleton.IsServer)
                {
                    ushort playersCount = (ushort)NetworkManager.Singleton.ConnectedClientsIds.Count;

                    _multiplay.ServerQueryHandler.CurrentPlayers = playersCount;

                    if (playersCount == 0)
                    {
                        _autoRemoveAllocationTimer -= deltaTime;

                        if (_autoRemoveAllocationTimer <= 0)
                        {
                            if (!_isSentRemoveAllocationRequest)
                            {
                                _removeAllocReq.AllocationId = MultiplayService.Instance.ServerConfig.AllocationId;
                                _removeAllocReq.SendRequest();

                                _autoRemoveAllocationTimer = _autoRemoveAllocationMaxTime * 3.1f;
                                _isSentRemoveAllocationRequest = true;

                                Debug.Log("Allocation shutdown since no players!");
                            }
                        }
                    }
                    else
                    {
                        _autoRemoveAllocationTimer = _autoRemoveAllocationMaxTime;
                        _isSentRemoveAllocationRequest = false;
                    }
                }
                _multiplay.ServerQueryHandler.UpdateServerCheck();
            }
        }
    }
}
#endif