using d4160.Runtime.UI;
using UnityEngine;
using Unity.Services.Authentication;


#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace d4160.UGS.Authentication
{
    public class AuthUIStateMachine : UIStateMachine
    {
#if ODIN_INSPECTOR
        [ValueDropdown("_states")]
#endif
        [SerializeField] protected UIState[] _authStates;

        protected override void AddInitialStates()
        {
            if (Application.isPlaying && AuthenticationService.Instance.IsSignedIn)
            {
                for (int i = 0; i < _authStates.Length; i++)
                {
                    AddActiveState(_authStates[i]);
                }
            }
            else
            {
                base.AddInitialStates();
            }
        }

        protected override void ShowInitialStates()
        {
            if (Application.isPlaying && AuthenticationService.Instance.IsSignedIn)
            {
                SetActiveStates(_authStates);
            }
            else
            {
                base.ShowInitialStates();
            }
        }
    }
}