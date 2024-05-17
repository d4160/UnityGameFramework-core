using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using MiTschMR.Runtime.EasyUGS.Authentication;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace d4160.Runtime.UGS.Authentication
{
    [Title("Sign in with Username and Password")]
    [Description("Signs into UGS with username and password")]

    [Category("UGS/Authentication/Sign In With Username and Password")]

    [Keywords("Authentication", "Multiplayer", "Networking", "Password", "Sign In", "UGS", "Username")]

    [Image(typeof(IconPersonCircleOutline), ColorTheme.Type.Green)]

    [Parameter("Username", "The variable to take the username from")]
    [Parameter("Password", "The variable to take the password from")]

    [Serializable]
    public class InstructionSignInUsernamePassword2 : Instruction
    {
        [SerializeField] private PropertyGetString username = new PropertyGetString();
        [SerializeField] private PropertyGetString password = new PropertyGetString();

        public override string Title => $"Sign into UGS with Username and Password";

        protected override async Task Run(Args args)
        {
            try
            {
                if (AuthenticationService.Instance.IsSignedIn && !AuthenticationService.Instance.IsExpired) return;

                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username.Get(args), password.Get(args));
            }
            catch (AuthenticationException exception)
            {
                AuthenticationManager.Instance.InvokeAuthenticationException(exception);
            }
            catch (RequestFailedException exception)
            {
                AuthenticationManager2.Instance.InvokeRequestException(exception);
            }
        }
    }
}