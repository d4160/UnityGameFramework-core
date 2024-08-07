using System;
#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif
using UnityEngine;

namespace d4160.UGS.Multiplay.LifecycleAPI
{
    [CreateAssetMenu(menuName = "d4160/UGS/Multiplay/API/Lifecycle/RemoveAllocationRequest")]
    public class RemoveAllocationRequestSO : ScriptableObject
    {
        [SerializeField] private string _allocationId;

        [Space]

        [SerializeField] private MultiplayLifecycleAllocationsAPI _lifecycleAllocationAPI;

        public string AllocationId
        {
            get => _allocationId;
            set => _allocationId = value;
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
            _lifecycleAllocationAPI.RemoveAllocation(_allocationId, onResult, onError);
        }
    }
}
