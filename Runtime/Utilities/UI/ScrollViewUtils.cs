using System.Collections;
using d4160.Coroutines;
using UnityEngine;
using UnityEngine.UI;

namespace d4160.Utilities.UIs
{
    public static class ScrollViewUtils
    {
        public static void ForceScrollDown(ScrollRect rect)
        {
            CoroutineStarter.Instance.StartCoroutine(ForceScrollDownCo(rect));
        }

        static IEnumerator ForceScrollDownCo(ScrollRect rect)
        {
            Canvas.ForceUpdateCanvases();

            var wait = new WaitForEndOfFrame();

            yield return wait;

            Canvas.ForceUpdateCanvases();

            VerticalLayoutGroup layoutGroup = rect.content.GetComponent<VerticalLayoutGroup>();
            ContentSizeFitter sizeFitter = rect.content.GetComponent<ContentSizeFitter>();

            layoutGroup.CalculateLayoutInputVertical();
            sizeFitter.SetLayoutVertical();

            layoutGroup.enabled = false;
            sizeFitter.enabled = false;
            layoutGroup.enabled = true;
            sizeFitter.enabled = true;

            Canvas.ForceUpdateCanvases();

            yield return wait;

            var rectT = rect.content.GetComponent<RectTransform>();

            LayoutRebuilder.MarkLayoutForRebuild(rectT);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectT);

            yield return wait;

            Canvas.ForceUpdateCanvases();
            rect.verticalNormalizedPosition = 0;
            rect.verticalScrollbar.value = 0;
        }
    }
}