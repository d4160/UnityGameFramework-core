using System.Collections.Generic;
using UnityEngine;
using d4160.Collections;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace d4160.Runtime.UI
{
    public class UIStateMachine : MonoBehaviour
    {
        [SerializeField] protected bool _disableAllStatesOnEnable = true;

#if ODIN_INSPECTOR
        [ValueDropdown("_states")]
#endif
        [SerializeField] protected UIState[] _initialStates;

        [Header("States")]
        [SerializeField] protected UIState[] _states;

        [SerializeField] protected List<UIState> _activeStates = new();

        public UIState[] States => _states;
        public List<UIState> ActiveStates => _activeStates;

        protected virtual void OnEnable()
        {
            SetupStates();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        private void SetupStates()
        {
            _activeStates.Clear();

            for (int i = 0; i < _states.Length; i++)
            {
                _states[i].StateMachine = this;

                if (_disableAllStatesOnEnable)
                {
                    if (!_states[i].LockDisable)
                    {
                        _states[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        AddStateIfActive(_states[i]);
                    }
                }
                else
                {
                    AddStateIfActive(_states[i]);
                }
            }

            AddInitialStates();
        }

        protected virtual void AddInitialStates()
        {
            for (int i = 0; i < _initialStates.Length; i++)
            {
                AddActiveState(_initialStates[i]);
            }
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        protected virtual void ShowInitialStates()
        {
            SetActiveStates(_initialStates);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void GoPrevState()
        {
            if (_activeStates == null || _activeStates.Count == 0) return;

            _activeStates.Last().GoPrevState();
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void GoNextState()
        {
            if (_activeStates == null || _activeStates.Count == 0) return;

            _activeStates.Last().GoNextState();
        }

        private void AddStateIfActive(UIState state)
        {
            if (state.isActiveAndEnabled)
            {
                _activeStates.Add(state);
            }
        }

        public void DisableAllActiveStates(bool forceDisable = false)
        {
            for (int i = _activeStates.Count - 1; i >= 0; i--)
            {
                RemoveActiveState(_activeStates[i], forceDisable);
            }
        }

        public void SetActiveStates(UIState[] states, bool forceDisable = false)
        {
            DisableAllActiveStates(forceDisable);

            for (int i = 0; i < states.Length; i++)
            {
                AddActiveState(states[i]);
            }
        }

        public void SetActiveState(UIState state, bool forceDisable = false)
        {
            DisableAllActiveStates(forceDisable);

            AddActiveState(state);
        }

        public void AddActiveState(UIState state)
        {
            if (!_activeStates.Contains(state))
            {
                state.gameObject.SetActive(true);
                _activeStates.Add(state);
            }
        }

        public void RemoveActiveState(UIState state, bool forceDisable = false)
        {
            if (_activeStates.Contains(state) && (!state.LockDisable || forceDisable))
            {
                state.gameObject.SetActive(false);
                _activeStates.Remove(state);
            }
        }
    }
}