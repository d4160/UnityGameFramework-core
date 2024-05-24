using System;
using UnityEngine;

namespace d4160.Runtime.Common
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public abstract class Singleton2<T> : MonoBehaviour where T : MonoBehaviour
    {
        [field: NonSerialized] private static T _Instance { get; set; }

        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    if (ApplicationManager2.IsExiting) return null;

                    GameObject singleton = new GameObject();
                    _Instance = singleton.AddComponent<T>();

                    //string name = TextUtils.Humanize(typeof(T).Name);
                    singleton.name = $"{typeof(T).Name} (singleton)";

                    Singleton2<T> component = _Instance.GetComponent<Singleton2<T>>();
                    component.OnCreate();

                    if (component.SurviveSceneLoads) DontDestroyOnLoad(singleton);
                }

                return _Instance;
            }
        }

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected void WakeUp()
        { }

        // VIRTUAL METHODS: -----------------------------------------------------------------------

        protected virtual void OnCreate()
        { }

        protected virtual bool SurviveSceneLoads => true;

        // PRIVATE METHODS: -----------------------------------------------------------------------

        protected virtual void OnDestroy()
        {
            _Instance = null;
        }
    }
}