using System.Collections;
using d4160.Coroutines;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace d4160.Utilities.UIs
{
    public static class ScrollViewUtils
    {
        public static void ForceScrollDown(ScrollRect rect, float duration = 0f, bool hardMode = true)
        {
            if (hardMode)
            {
                CoroutineStarter.Instance.StartCoroutine(ForceScrollDownHardCo(rect, duration));
            }
            else
            {
                CoroutineStarter.Instance.StartCoroutine(ForceScrollDownCo(rect));
            }
        }

        static IEnumerator ForceScrollDownCo(ScrollRect rect, float duration = 0f)
        {
            yield return new WaitForEndOfFrame();

            Canvas.ForceUpdateCanvases();

            rect.gameObject.SetActive(true);

            yield return ScrollDownTween(rect, duration);
        }

        static IEnumerator ForceScrollDownHardCo(ScrollRect rect, float duration = 0f)
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

            yield return ScrollDownTween(rect, duration);
        }

        private static IEnumerator ScrollDownTween(ScrollRect rect, float duration)
        {
            if (duration <= 0 || rect.verticalNormalizedPosition == 0)
            {
                rect.verticalNormalizedPosition = 0;
                rect.verticalScrollbar.value = 0;
            }
            else
            {
                float timePassed = 0f;
                float fromValue = rect.verticalNormalizedPosition;
                float toValue = 0;

                while (timePassed < duration)
                {
                    // Calculate the interpolation factor (0 to 1)
                    float factor = timePassed / duration;

                    // Linearly interpolate the value
                    float value = Mathf.Lerp(fromValue, toValue, factor);
                    rect.verticalNormalizedPosition = value;
                    rect.verticalScrollbar.value = value;

                    // Increase timePassed by the time since the last frame
                    timePassed += Mathf.Min(Time.deltaTime, duration - timePassed);

                    // "Pause" the coroutine here, render the frame, and continue from here in the next frame
                    yield return null;
                }

                rect.verticalNormalizedPosition = toValue;
                rect.verticalScrollbar.value = toValue;
            }
        }

        public static void ForceUpdateInputText(TMP_InputField field)
        {
            CoroutineStarter.Instance.StartCoroutine(ForceUpdateInputTextCo(field));
        }

        static IEnumerator ForceUpdateInputTextCo(TMP_InputField field)
        {
            var wait = new WaitForEndOfFrame();

            yield return wait;

            Canvas.ForceUpdateCanvases();

            yield return wait;

            var rectT = field.transform as RectTransform;
            var rectT2 = field.textComponent.rectTransform;

            LayoutRebuilder.MarkLayoutForRebuild(rectT);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectT);

            LayoutRebuilder.MarkLayoutForRebuild(rectT2);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectT2);

            yield return wait;

            Canvas.ForceUpdateCanvases();
        }
    }
}