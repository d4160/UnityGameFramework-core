using UnityEngine;

namespace d4160.Utilities.UIs
{
    public static class RectTransformExtensions
    {
        public static void SetRectTransformOffsets(this RectTransform instance, float left, float top, float right, float bottom)
        {
            instance.offsetMin = new Vector2(left, bottom); // Bottom-left corner
            instance.offsetMax = new Vector2(-right, -top); // Top-right corner
        }
    }
}