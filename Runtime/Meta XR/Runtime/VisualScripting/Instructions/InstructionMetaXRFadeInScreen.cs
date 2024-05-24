using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(0, 1, 0)]

[Title("Fade In")]
[Description("Fade In Screen using OVRScreenFade")]

[Category("MetaXR/Fade In")]

[Parameter("Wait to Complete", "Whether to wait until the fade in is finished or not")]

[Keywords("Screen", "Fade", "Fade In")]
[Image(typeof(IconCamera), ColorTheme.Type.Blue)]

[Serializable]
public class InstructionMetaXRFadeInScreen : Instruction
{
    [SerializeField] private bool m_WaitToComplete = true;

    public override string Title => string.Format(
            "OVR Fade In {0}",
            this.m_WaitToComplete ? "and wait" : string.Empty
        );

    protected override async Task Run(Args args)
    {
        OVRScreenFade screenFade = OVRScreenFade.instance;
        if (screenFade == null) return;

        screenFade.FadeIn();

        if (this.m_WaitToComplete)
            await this.Time(screenFade.fadeTime);
    }
}
