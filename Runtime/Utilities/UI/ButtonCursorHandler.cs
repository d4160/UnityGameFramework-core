using UnityEngine;
using UnityEngine.EventSystems;

namespace d4160.Utilities.UIs
{
    public class ButtonCursorHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Texture2D handCursor; // Assign a hand cursor texture in the Inspector

        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.SetCursor(handCursor, Vector2.zero, CursorMode.Auto);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}