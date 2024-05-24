using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using System;

namespace d4160.Runtime.UGS.Authentication
{
    [Title("On Password Does Not Match Requirements")]
    [Description("Executes when an attempt was made to sign up with an invalid password")]

    [Category("UGS/Authentication/On Password does not Match Requirements")]

    [Keywords("Authentication", "Networking", "No", "Match", "Password", "Requirements", "UGS")]

    [Image(typeof(IconPersonCircleOutline), ColorTheme.Type.Red)]

    [Serializable]
    public class EventOnPasswordDoesNotMatchRequirements : Event
    {
        protected override void OnStart(Trigger trigger)
        {
            base.OnStart(trigger);

            AuthenticationManager2.Instance.OnPasswordDoesNotMatchRequirements += OnPasswordDoesNotMatchRequirements;
        }

        protected override void OnDestroy(Trigger trigger)
        {
            base.OnDestroy(trigger);

            AuthenticationManager2.Instance.OnPasswordDoesNotMatchRequirements -= OnPasswordDoesNotMatchRequirements;
        }

        protected virtual void OnPasswordDoesNotMatchRequirements()
        {
            _ = this.m_Trigger.Execute(this.Self);
        }
    }
}