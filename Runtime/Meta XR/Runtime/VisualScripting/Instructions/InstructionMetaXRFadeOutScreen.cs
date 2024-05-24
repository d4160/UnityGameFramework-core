using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using GameCreator.Runtime.VisualScripting;
using UnityEngine;

[Version(0, 1, 0)]

[Title("Fade Out")]
[Description("Fade Out Screen using OVRScreenFade")]

[Category("MetaXR/Fade Out")]

[Parameter("Wait to Complete", "Whether to wait until the fade out is finished or not")]

[Keywords("Screen", "Fade", "Fade Out")]
[Image(typeof(IconCamera), ColorTheme.Type.Blue)]

[Serializable]
public class InstructionMetaXRFadeOutScreen : Instruction
{
    [SerializeField] private bool m_WaitToComplete = true;

    public override string Title => string.Format(
            "OVR Fade Out {0}",
            this.m_WaitToComplete ? "and wait" : string.Empty
        );

    protected override async Task Run(Args args)
    {
        OVRScreenFade screenFade = OVRScreenFade.instance;
        if (screenFade == null) return;

        screenFade.FadeOut();

        if (this.m_WaitToComplete)
            await this.Time(screenFade.fadeTime);
    }
}
