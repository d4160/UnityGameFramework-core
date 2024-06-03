using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

public class UIState : MonoBehaviour
{
    [SerializeField] protected bool _lockDisable = false;
    [SerializeField] protected UIStateMachine _stateMachine;

#if ODIN_INSPECTOR
    [ValueDropdown("_StatesList")]
#endif
    [SerializeField] protected UIState[] _prevStates;

#if ODIN_INSPECTOR
    [ValueDropdown("_StatesList")]
#endif
    [SerializeField] protected UIState[] _nextStates;

    [SerializeField] protected bool _forceDisable = false;

    private UIState[] _StatesList => _stateMachine.States;

    public bool LockDisable => _lockDisable;

    public UIStateMachine StateMachine { get => _stateMachine; internal set => _stateMachine = value; }

#if ODIN_INSPECTOR
    [Button]
#endif
    public void ShowState()
    {
        _stateMachine.SetActiveState(this, _forceDisable);
    }

    public void ShowState(bool forceDisable)
    {
        _stateMachine.SetActiveState(this, forceDisable);
    }


#if ODIN_INSPECTOR
    [Button]
#endif
    public void HideState()
    {
        _stateMachine.RemoveActiveState(this, _forceDisable);
    }

    public void HideState(bool forceDisable)
    {
        _stateMachine.RemoveActiveState(this, forceDisable);
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    public void GoNextState()
    {
        if (_nextStates == null || _nextStates.Length == 0) return;

        _stateMachine.SetActiveStates(_nextStates, _forceDisable);
    }

    public void GoNextState(bool forceDisable)
    {
        if (_nextStates == null || _nextStates.Length == 0) return;

        _stateMachine.SetActiveStates(_nextStates, forceDisable);
    }

#if ODIN_INSPECTOR
    [Button]
#endif
    public void GoPrevState()
    {
        if (_prevStates == null || _prevStates.Length == 0) return;

        _stateMachine.SetActiveStates(_prevStates, _forceDisable);
    }

    public void GoPrevState(bool forceDisable)
    {
        if (_prevStates == null || _prevStates.Length == 0) return;

        _stateMachine.SetActiveStates(_prevStates, forceDisable);
    }
}
