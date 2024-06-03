using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

namespace d4160.Runtime.UI
{
    [Title("Go Prev UI State")]
    [Description("Go Prev UI State that belongs to a UIStateMachine")]

    [Category("UI/Go Prev UI State")]

    [Keywords("Go", "Prev", "UI", "State", "StateMachine")]

    [Image(typeof(IconUICanvasGroup), ColorTheme.Type.Purple)]

    [Parameter("UIState", "The UIState reference")]
    [Parameter("ForceDisable", "Whatever or not force disable UIStates which have LockDisable flag set to true")]

    [Serializable]
    public class InstructionUIStateGoPrev : Instruction
    {
        public override string Title => $"Go Prev UIState {m_UIState}";

        [SerializeField] private PropertyGetGameObject m_UIState = GetGameObjectInstance.Create();
        [SerializeField] private bool m_forceDisable = false;

        protected override Task Run(Args args)
        {
            UIState state = m_UIState.Get<UIState>(args);
            if (state == null) return DefaultResult;

            state.GoPrevState(m_forceDisable);

            return DefaultResult;
        }
    }
}