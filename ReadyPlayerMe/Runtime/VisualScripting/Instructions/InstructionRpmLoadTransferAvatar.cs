using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using ReadyPlayerMe.Core;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace d4160.Runtime.ReadyPlayerMe
{
    [Title("Load Transfer Avatar")]
    [Description("Load the avatar with url and then transfer mesh data to placeholder")]

    [Category("Ready Player Me/Load Transfer Avatar")]

    [Keywords("Authentication", "Multiplayer", "Networking", "Password", "Sign In", "UGS", "Username")]

    [Image(typeof(IconCharacter), ColorTheme.Type.Blue)]

    [Parameter("Url", "The RPM avatar URL or ID")]
    [Parameter("AvatarPlaceHolder", "The GameObject the loaded avatar will transfer to")]
    [Parameter("AvatarConfig", "The config asset for loading avatar")]
    [Parameter("ConfigEyePosition", "If we need to configure the eye's positions of avatar placeholder after transfer mesh")]

    [Serializable]
    public class InstructionRpmLoadTransferAvatar : Instruction
    {
        [SerializeField] private PropertyGetString m_Url = new PropertyGetString();
        [SerializeField] private PropertyGetGameObject m_AvatarPlaceHolder = new PropertyGetGameObject();
        [SerializeField] private AvatarConfig m_AvatarConfig;
        [SerializeField] private bool m_ConfigEyePosition = true;

        public override string Title => $"Load Transfer Avatar {m_AvatarPlaceHolder} from {m_Url}";

        protected override async Task Run(Args args)
        {
            GameObject avatarPlaceholder = m_AvatarPlaceHolder.Get(args);

            if (avatarPlaceholder == null) return;
            if (m_AvatarConfig == null) return;

            await AvatarLoadingHelper.LoadAndTransferAvatarAsync(m_Url.Get(args), avatarPlaceholder, m_AvatarConfig, m_ConfigEyePosition);
        }
    }
}