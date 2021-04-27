using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class GarbageCollectionManager : MonoBehaviour
{
    public static GarbageCollectionManager Instance = null;
    [SerializeField] private float maxTimeBetweenGarbageCollections = 60f;
    private float _timeSinceLastGarbageCollection;

    private void Awake()
    {
        if (Instance != null)
        { Destroy(gameObject); }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        CollectGarbage();
    }
    private void Start()
    {
#if !UNITY_EDITOR
    GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
#endif
        // You might want to run this during loading times, screen fades and such.
        // Events.OnScreenFade += CollectGarbage;
    }
    private void Update()
    {
        _timeSinceLastGarbageCollection += Time.unscaledDeltaTime;
        if (_timeSinceLastGarbageCollection > maxTimeBetweenGarbageCollections)
        {
            CollectGarbage();
        }
    }
    private void CollectGarbage()
    {
        _timeSinceLastGarbageCollection = 0f;
        Debug.Log("Collecting garbage"); // talking about garbage... 
    #if !UNITY_EDITOR
        // Not supported on the editor
        GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
        System.GC.Collect();
        GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
    #endif
    }
}
