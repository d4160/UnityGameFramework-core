#if !DEDICATED_SERVER
using d4160.Events;
using d4160.Variables;
using UnityEngine.Serialization;
using UnityEngine;
using NaughtyAttributes;

public class OnAudioFilterReadForwarder : MonoBehaviour, IEventListener<bool>
{
    public enum MuteState
    {
        None,
        Before,
        After
    }
    [SerializeField, FormerlySerializedAs("_muteBehaviour")] private MuteState _muteState;
    [Range(0f, 7f)]
    [SerializeField, FormerlySerializedAs("amplitudeMultiplier")] private float _amplitudeMultiplier = 1f;
    [SerializeField] private AudioFilterReadEventSO _audioFilterReadEvent;

    [Header("Variables")]
    [SerializeField, Expandable] private BoolVariableSO _isMutedVar;
    [SerializeField, Expandable] private BoolVariableSO _isGlobalMutedVar;

    private BoolEventSO.EventListener _onGlobalIsMutedChanged;

    private Dissonance.Audio.Capture.BasicMicrophoneCapture _micCapture;
    private float _originalAmplitude;
    private bool _isMutedPrev = true;
    private bool _isGlobalMutedPrev = true;

    private void Awake()
    {
        _originalAmplitude = _amplitudeMultiplier;

        _micCapture = GetComponent<Dissonance.Audio.Capture.BasicMicrophoneCapture>();

        _onGlobalIsMutedChanged = new(SetIsGlobalMuted);
    }

    private void Start()
    {
        _isMutedPrev = _isMutedVar.Value;
        if (_isGlobalMutedVar) _isGlobalMutedPrev = _isGlobalMutedVar.Value;

        SetIsMuted(_isMutedVar.Value);

        if (_isGlobalMutedVar) SetIsGlobalMuted(_isGlobalMutedVar.Value);

        if (_micCapture)
        {
            _micCapture.OnStartCapture += (s) =>
            {
                //Debug.Log($"[onStartCapture] Microphone: {s}");
                if (!string.IsNullOrEmpty(s) && s.Contains("Stereo Mix"))
                {
                    _amplitudeMultiplier = 1f;
                }
                else
                {
                    _amplitudeMultiplier = _originalAmplitude;
                }
            };
        }
    }

    private void OnEnable()
    {
        if (_isMutedVar != null && _isMutedVar.OnValueChange)
        {
            _isMutedVar.OnValueChange.AddListener(this);
        }

        if (_isGlobalMutedVar) _isGlobalMutedVar.OnValueChange.AddListener(_onGlobalIsMutedChanged);
    }

    private void OnDisable()
    {
        if (_isMutedVar != null && _isMutedVar.OnValueChange)
        {
            _isMutedVar.OnValueChange.RemoveListener(this);
        }

        if (_isGlobalMutedVar) _isGlobalMutedVar.OnValueChange.RemoveListener(_onGlobalIsMutedChanged);
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        //Debug.Log($"[OnAudioFilterRead] DataLength: {data.Length}; Channels: {channels}");
        if (_muteState == MuteState.Before)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
        }
        else
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Mathf.Clamp(data[i] * _amplitudeMultiplier, -1f, 1f);
            }
        }
        if (_audioFilterReadEvent) _audioFilterReadEvent.Invoke(data, channels);
        if (_muteState == MuteState.After)
        {
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = 0;
            }
        }
    }

    public void OnInvoked(bool isMuted)
    {
        SetIsMuted(isMuted);
    }

    private void SetIsMuted(bool isMuted)
    {
        _isMutedPrev = isMuted;
        if (isMuted && !_isGlobalMutedPrev)
        {
            isMuted = false;
        }

        _muteState = isMuted ? MuteState.Before : MuteState.After;
    }

    private void SetIsGlobalMuted(bool isMuted)
    {
        _isGlobalMutedPrev = isMuted;

        if (isMuted && !_isMutedPrev)
        {
            isMuted = false;
        }

        _muteState = isMuted ? MuteState.Before : MuteState.After;
    }
}
#endif