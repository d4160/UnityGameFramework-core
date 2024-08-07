using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using System;

namespace d4160.Runtime.UGS.Authentication
{
    [Title("On Invalid Username or Password")]
    [Description("Executes when an attempt was made to sign in with an invalid username or password or account not exists")]

    [Category("UGS/Authentication/On Invalid Username or Password")]

    [Keywords("Authentication", "Networking", "Invalid", "Username", "Password", "UGS")]

    [Image(typeof(IconPersonCircleOutline), ColorTheme.Type.Red)]

    [Serializable]
    public class EventOnNoActiveSession : Event
    {
        protected override void OnStart(Trigger trigger)
        {
            base.OnStart(trigger);

            AuthenticationManager2.Instance.OnInvalidUsernameOrPassword += OnInvalidUsernameOrPassword;
        }

        protected override void OnDestroy(Trigger trigger)
        {
            base.OnDestroy(trigger);

            if (AuthenticationManager2.Instance)
            {
                AuthenticationManager2.Instance.OnInvalidUsernameOrPassword -= OnInvalidUsernameOrPassword;
            }
        }

        protected virtual void OnInvalidUsernameOrPassword()
        {
            _ = this.m_Trigger.Execute(this.Self);
        }
    }
}