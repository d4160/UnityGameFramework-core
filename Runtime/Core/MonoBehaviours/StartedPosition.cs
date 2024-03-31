using d4160.Core;
#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif
using UnityEngine;

public class StartedPosition : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    [SerializeField] private UnityLifetimeMethodType _setAt;

    private void Awake()
    {
        if (_setAt == UnityLifetimeMethodType.Awake)
            UpdatePosition();
    }

    private void Start()
    {
        if (_setAt == UnityLifetimeMethodType.Start)
            UpdatePosition();
    }

    private void OnEnable()
    {
        if (_setAt == UnityLifetimeMethodType.OnEnable)
            UpdatePosition();
    }

#if ENABLE_NAUGHTY_ATTRIBUTES
    [Button]
#endif
    public void UpdatePosition()
    {
        for (int i = 0; i < _transforms.Length; i++)
        {
            if (_transforms[i])
                _transforms[i].SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}
