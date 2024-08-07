using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using d4160.Singleton;

public enum ToastPosition
{
    TopLeft,
    TopCenter,
    TopRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}

public enum ToastTheme
{
    Info,
    Dark,
    White,
    Success,
    Warning,
    Error
}

public class ToastManager : Singleton<ToastManager>
{
    public GameObject toastPrefab;
    public Canvas canvas;

    private Dictionary<ToastTheme, Color> themeColors;

    protected override void Awake()
    {
        base.Awake();
        themeColors = new Dictionary<ToastTheme, Color>
        {
            { ToastTheme.Info, Color.cyan },
            { ToastTheme.Dark, Color.black },
            { ToastTheme.White, Color.white },
            { ToastTheme.Success, Color.green },
            { ToastTheme.Warning, Color.yellow },
            { ToastTheme.Error, Color.red }
        };
    }

    public void ShowToast(string message, ToastPosition position, ToastTheme theme, float duration = 2.0f, bool isPermanent = false)
    {
        GameObject toastInstance = Instantiate(toastPrefab, canvas.transform);
        Toast toastComponent = toastInstance.GetComponent<Toast>();

        toastComponent.SetMessage(message);
        toastComponent.SetTheme(themeColors[theme]);
        toastComponent.SetDuration(duration, isPermanent);

        toastInstance.SetActive(true);

        RectTransform toastRect = toastInstance.GetComponent<RectTransform>();
        toastRect.anchoredPosition = GetPosition(position);

        toastComponent.FadeIn();

        if (!isPermanent)
        {
            StartCoroutine(DestroyToastAfterDuration(toastInstance, duration));
        }
    }

    private Vector2 GetPosition(ToastPosition position)
    {
        switch (position)
        {
            case ToastPosition.TopLeft: return new Vector2(-canvas.GetComponent<RectTransform>().rect.width / 2 + 50, canvas.GetComponent<RectTransform>().rect.height / 2 - 50);
            case ToastPosition.TopCenter: return new Vector2(0, canvas.GetComponent<RectTransform>().rect.height / 2 - 50);
            case ToastPosition.TopRight: return new Vector2(canvas.GetComponent<RectTransform>().rect.width / 2 - 50, canvas.GetComponent<RectTransform>().rect.height / 2 - 50);
            case ToastPosition.MiddleLeft: return new Vector2(-canvas.GetComponent<RectTransform>().rect.width / 2 + 50, 0);
            case ToastPosition.MiddleCenter: return new Vector2(0, 0);
            case ToastPosition.MiddleRight: return new Vector2(canvas.GetComponent<RectTransform>().rect.width / 2 - 50, 0);
            case ToastPosition.BottomLeft: return new Vector2(-canvas.GetComponent<RectTransform>().rect.width / 2 + 50, -canvas.GetComponent<RectTransform>().rect.height / 2 + 50);
            case ToastPosition.BottomCenter: return new Vector2(0, -canvas.GetComponent<RectTransform>().rect.height / 2 + 50);
            case ToastPosition.BottomRight: return new Vector2(canvas.GetComponent<RectTransform>().rect.width / 2 - 50, -canvas.GetComponent<RectTransform>().rect.height / 2 + 50);
            default: return new Vector2(0, 0);
        }
    }

    private IEnumerator DestroyToastAfterDuration(GameObject toastInstance, float duration)
    {
        yield return new WaitForSeconds(duration);

        Toast toastComponent = toastInstance.GetComponent<Toast>();
        toastComponent.FadeOut();
        yield return new WaitForSeconds(toastComponent.fadeDuration);

        Destroy(toastInstance);
    }
}
