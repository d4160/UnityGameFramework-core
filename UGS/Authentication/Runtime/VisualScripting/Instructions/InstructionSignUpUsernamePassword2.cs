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
    [Title("Sign up with Username and Password")]
    [Description("Signs up for UGS with username and password")]

    [Category("UGS/Authentication/Sign Up With Username and Password")]

    [Keywords("Authentication", "Multiplayer", "Networking", "Password", "Sign Up", "UGS", "Username")]

    [Image(typeof(IconPersonCircleOutline), ColorTheme.Type.Teal)]

    [Parameter("Username", "The variable to take the username from")]
    [Parameter("Password", "The variable to take the password from")]

    [Serializable]
    public class InstructionSignUpUsernamePassword2 : Instruction
    {
        [SerializeField] private PropertyGetString username = new PropertyGetString();
        [SerializeField] private PropertyGetString password = new PropertyGetString();

        public override string Title => $"Sign up for UGS with Username and Password";

        protected override async Task Run(Args args)
        {
            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username.Get(args), password.Get(args));
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