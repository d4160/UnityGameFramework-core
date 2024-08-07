using TMPro;
using UnityEngine;

namespace d4160.Utilities.UIs
{
    public static class InputFieldExtensions
    {
        public static void FixRectTransformOffsets(this TMP_InputField instance)
        {
            instance.textComponent.rectTransform.SetRectTransformOffsets(0, 0, 0, 0);

            RectTransform caret = instance.transform.GetChild(0).Find("Caret") as RectTransform;
            caret.SetRectTransformOffsets(0, 0, 0, 0);
        }
    }
}