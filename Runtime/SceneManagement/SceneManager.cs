using d4160.Core;
using d4160.Singleton;
#if ENABLE_NAUGHTY_ATTRIBUTES
using NaughtyAttributes;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace d4160.SceneManagement
{
    public class SceneManager : Singleton<SceneManager>
    {

#if ENABLE_NAUGHTY_ATTRIBUTES
        [Expandable]
#endif
        [SerializeField] protected SceneManagerSO _sceneManagerAsset;
        [SerializeField] protected UnityLifetimeMethodType _loadSceneAt;
#if ENABLE_NAUGHTY_ATTRIBUTES
        [DropdownIndex("SceneCollectionsNames")]
#endif
        [SerializeField] protected int _sceneCollection;

#if UNITY_EDITOR
        private string[] SceneCollectionsNames => _sceneManagerAsset?.GetSceneCollectionNames;
#endif

        public SceneManagerSO SceneManagerAsset => _sceneManagerAsset;

        [Header("EVENTS")]
        [SerializeField] private UnityEvent<int, string> _onCollectionLoaded;

        /* LOAD */
        public static void LoadSceneCollectionAsync(string label)
        {
            SceneManager.Instance._sceneManagerAsset?.LoadSceneCollectionAsync(label);
        }

        public static void LoadSceneCollectionAsync(int index)
        {
            SceneManager.Instance._sceneManagerAsset?.LoadSceneCollectionAsync(index);
        }

        public static void LoadSceneCollectionAsync(string label, AssetManagementType sceneAssetType)
        {
            SceneManager.Instance._sceneManagerAsset?.LoadSceneCollectionAsync(label, sceneAssetType);
        }

        public static void LoadSceneCollectionAsync(int index, AssetManagementType sceneAssetType)
        {
            SceneManager.Instance._sceneManagerAsset?.LoadSceneCollectionAsync(index, sceneAssetType);
        }

        /* CONTINUE */
        public static void ContinueCollectionLoadAsync(string label)
        {
            SceneManager.Instance._sceneManagerAsset?.ContinueCollectionLoadAsync(label);
        }

        public static void ContinueCollectionLoadAsync(int index)
        {
            SceneManager.Instance._sceneManagerAsset?.ContinueCollectionLoadAsync(index);
        }

        public static void ContinueCollectionLoadAsync(string label, AssetManagementType sceneAssetType)
        {
            SceneManager.Instance._sceneManagerAsset?.ContinueCollectionLoadAsync(label, sceneAssetType);
        }

        public static void ContinueCollectionLoadAsync(int index, AssetManagementType sceneAssetType)
        {
            SceneManager.Instance._sceneManagerAsset?.ContinueCollectionLoadAsync(index, sceneAssetType);
        }

        /* UNLOAD */
        public static void UnloadSceneCollectionAsync(string label)
        {
            SceneManager.Instance._sceneManagerAsset?.UnloadSceneCollectionAsync(label);
        }

        public static void UnloadSceneCollectionAsync(int index)
        {
            SceneManager.Instance._sceneManagerAsset?.UnloadSceneCollectionAsync(index);
        }

        public static void UnloadSceneCollectionAsync(string label, AssetManagementType sceneAssetType)
        {
            SceneManager.Instance._sceneManagerAsset?.UnloadSceneCollectionAsync(label, sceneAssetType);
        }

        public static void UnloadSceneCollectionAsync(int index, AssetManagementType sceneAssetType)
        {
            SceneManager.Instance._sceneManagerAsset?.UnloadSceneCollectionAsync(index, sceneAssetType);
        }

        protected override void Awake()
        {
            base.Awake();
            if (_loadSceneAt == UnityLifetimeMethodType.Awake)
            {
                LoadSceneCollectionAsync(_sceneCollection);
            }
        }

        protected virtual void Start()
        {
            if (_loadSceneAt == UnityLifetimeMethodType.Start)
            {
                LoadSceneCollectionAsync(_sceneCollection);
            }
        }

        protected virtual void OnEnable()
        {
            if (_loadSceneAt == UnityLifetimeMethodType.OnEnable)
            {
                LoadSceneCollectionAsync(_sceneCollection);
            }

            if (_sceneManagerAsset)
            {
                _sceneManagerAsset.RegisterEvents();
                _sceneManagerAsset.OnCollectionLoaded += OnCollectionLoadedCallback;
            }
        }

        protected virtual void OnDisable()
        {
            if (_sceneManagerAsset)
            {
                _sceneManagerAsset.UnregisterEvents();
                _sceneManagerAsset.OnCollectionLoaded -= OnCollectionLoadedCallback;
            }
        }

        private void OnCollectionLoadedCallback(int index, string label)
        {
            _onCollectionLoaded?.Invoke(index, label);
        }

#if ENABLE_NAUGHTY_ATTRIBUTES
        [Button]
#endif
        public void LoadSceneCollectionAsync()
        {
            LoadSceneCollectionAsync(_sceneCollection);
        }

#if ENABLE_NAUGHTY_ATTRIBUTES
        [Button]
#endif
        public void ContinueCollectionLoadAsync()
        {
            ContinueCollectionLoadAsync(_sceneCollection);
        }

#if ENABLE_NAUGHTY_ATTRIBUTES
        [Button]
#endif
        public void UnloadSceneCollectionAsync()
        {
            UnloadSceneCollectionAsync(_sceneCollection);
        }
    }
}