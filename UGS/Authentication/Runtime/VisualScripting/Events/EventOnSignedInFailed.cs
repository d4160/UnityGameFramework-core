using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;

namespace d4160.Runtime.UGS.Authentication
{
    [Title("On Signed In Failed")]
    [Description("Executes when a user login into UGS fails")]

    [Category("UGS/Authentication/On Signed In Failed")]

    [Keywords("Authentication", "Failed", "Networking", "Signed In", "UGS")]

    [Image(typeof(IconPersonCircleOutline), ColorTheme.Type.Red)]

    [Serializable]
    public class EventOnSignedInFailed2 : Event
    {
        protected override void OnStart(Trigger trigger)
        {
            base.OnStart(trigger);

            AuthenticationManager2.Instance.OnSignedInFailed += OnSignedInFailed;
        }

        protected override void OnDestroy(Trigger trigger)
        {
            base.OnDestroy(trigger);

            AuthenticationManager2.Instance.OnSignedInFailed -= OnSignedInFailed;
        }

        protected virtual void OnSignedInFailed()
        {
            _ = this.m_Trigger.Execute(this.Self);
        }
    }
}