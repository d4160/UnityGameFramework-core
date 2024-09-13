using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public static class ClipboardUtils
{
    public static void CopyToClipboard(string text)
    {
#if UNITY_STANDALONE
        GUIUtility.systemCopyBuffer = text;
#endif

#if UNITY_WEBGL
        //En WebGL, no puedes acceder directamente al portapapeles debido a restricciones de seguridad. Sin embargo, puedes mostrar un mensaje al usuario con el texto que desees que copien y pedirles que lo hagan manualmente.
#endif

#if UNITY_ANDROID
        // Asegúrate de tener los permisos adecuados en el archivo AndroidManifest.xml para acceder al portapapeles.
        //if (Permission.HasUserAuthorizedPermission(Permission.ClipboardWrite))
        //{
        GUIUtility.systemCopyBuffer = text;
        //}
        //else
        //{
        //Permission.RequestUserPermission(Permission.ClipboardWrite);
        //}
#endif
    }
}
