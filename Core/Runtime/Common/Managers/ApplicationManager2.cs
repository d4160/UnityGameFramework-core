using System;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: Preserve]
namespace d4160.Runtime.Common
{
    [AddComponentMenu("")]
    public class ApplicationManager2 : Singleton2<ApplicationManager2>
    {
        // STATIC PROPERTIES: ---------------------------------------------------------------------

        public static bool IsExiting { get; private set; }
        
        // STATIC EVENTS: -------------------------------------------------------------------------

        public static event Action EventExit;

        // INITIALIZE METHODS: --------------------------------------------------------------------

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        public static void OnSubsystemsInit()
        {
            IsExiting = false;
            Instance.WakeUp();
        }
        
        private void OnApplicationQuit()
        {
            IsExiting = true;
            EventExit?.Invoke();
        }
    }
}