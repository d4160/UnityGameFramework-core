using System;
using NaughtyAttributes;
using UnityEngine;

namespace d4160.Instancers
{
    public abstract class ComponentProviderSO : ScriptableObject
    {
        [SerializeField] protected Component _prefab;
        // [Tooltip("If is not null, set as parent in each instantiation.")]
        // [SerializeField] protected Transform _parent;
        [Tooltip("If true, the parent-relative position, scale and rotation are modified such that the object keeps the same world space position, rotation and scale as before.")]
        [SerializeField] protected bool _worldPositionStays = true;

        public event Action<Component> OnInstanced;
        public event Action<Component> OnDestroy;

        public abstract IProvider<Component> Provider { get; }
        public Transform Parent { get; set; }
        public Component Prefab { get => _prefab; set => _prefab = value; }

        protected void CallOnInstancedEvent(Component comp) => OnInstanced?.Invoke(comp);
        protected void CallOnDestroyEvent(Component comp) => OnDestroy?.Invoke(comp);

        public void Setup() {
            Provider.Prefab = _prefab;
        }

        public void RegisterEvents() {
            Provider.OnInstanced += CallOnInstancedEvent;
            Provider.OnDestroy += CallOnDestroyEvent;
        }

        public void UnregisterEvents() {
            Provider.OnInstanced -= CallOnInstancedEvent;
            Provider.OnDestroy -= CallOnDestroyEvent;
        }

        [Button]
        public Component Instantiate()
        {
            Component instance = Provider.Instantiate();
            if (Parent) instance.transform.SetParent(Parent, _worldPositionStays);
            return instance;
        }

        public T InstantiateAs<T>() where T : Component
        {
            return Instantiate() as T;
        }

        public void Destroy(Component instance)
        {
            Provider.Destroy(instance);
        }

        public void Destroy<T>(T instance) where T : Component
        {
            Provider.Destroy(instance);
        }
    }
}