using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using System;

namespace d4160.Runtime.UGS.Authentication
{
    [Title("On Username Already Exists")]
    [Description("Executes when an attempt was made to sign up with an username already taken")]

    [Category("UGS/Authentication/On Username Already Exists")]

    [Keywords("Authentication", "Networking", "Username", "Already", "Exists", "UGS")]

    [Image(typeof(IconPersonCircleOutline), ColorTheme.Type.Red)]

    [Serializable]
    public class EventOnUsernameAlreadyExists : Event
    {
        protected override void OnStart(Trigger trigger)
        {
            base.OnStart(trigger);

            AuthenticationManager2.Instance.OnUsernameAlreadyExists += OnUsernameAlreadyExists;
        }

        protected override void OnDestroy(Trigger trigger)
        {
            base.OnDestroy(trigger);

            AuthenticationManager2.Instance.OnUsernameAlreadyExists -= OnUsernameAlreadyExists;
        }

        protected virtual void OnUsernameAlreadyExists()
        {
            _ = this.m_Trigger.Execute(this.Self);
        }
    }
}