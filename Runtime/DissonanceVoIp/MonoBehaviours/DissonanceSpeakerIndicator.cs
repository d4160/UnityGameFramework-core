#if !DEDICATED_SERVER
using d4160.Loops;
using Dissonance;
using UnityEngine;
using System.Collections;

namespace d4160.Dissonance
{
    public class DissonanceSpeakerIndicator : UpdatableBehaviour
    {
        [SerializeField] private GameObject[] _isSpeakingReceivers;

        [Header("Debug")]
        [SerializeField] private bool _isSpeaking;
        [SerializeField] private float _amplitude;

        private IDissonancePlayer _player;
        private VoicePlayerState _state;
        private IIsSpeakingReceiver[] _receivers;

        private bool IsSpeaking
        {
            get { return _state != null && _state.IsSpeaking; } //_player.Type == NetworkPlayerType.Remote
        }

        private float Amplitude
        {
            get { return _state != null ? _state.Amplitude : 0f; } //_player.Type == NetworkPlayerType.Remote
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _player = GetComponent<IDissonancePlayer>();

            StartCoroutine(FindPlayerState());
        }

        private void Start()
        {
            _receivers = new IIsSpeakingReceiver[_isSpeakingReceivers.Length];
            for (int i = 0; i < _isSpeakingReceivers.Length; i++)
            {
                _receivers[i] = _isSpeakingReceivers[i].GetComponent<IIsSpeakingReceiver>();
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            _isSpeaking = IsSpeaking;
            _amplitude = Amplitude;
            if (_receivers == null) return;
            for (int i = 0; i < _receivers.Length; i++)
            {
                _receivers[i].IsSpeaking = IsSpeaking;
                _receivers[i].Amplitude = Amplitude;
            }
        }

        private IEnumerator FindPlayerState()
        {
            //Wait until player tracking has initialized
            while (!_player.IsTracking)
                yield return null;

            //Now ask Dissonance for the object which represents the state of this player
            //The loop is necessary in case Dissonance is still initializing this player into the network session
            while (_state == null)
            {
                _state = FindFirstObjectByType<DissonanceComms>().FindPlayer(_player.PlayerId);
                yield return null;
            }
        }
    }
}
#endif