using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTriggerHelper : MonoBehaviour, IPointerExitHandler
{
    public TooltipManager TooltipManager { get; internal set; }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (TooltipManager && TooltipManager.LastToolTipTrigger)
        {
            TooltipManager.LastToolTipTrigger.HideTooltip();
            TooltipManager.LastToolTipTrigger = null;
        }
    }
}
