using System;
using UnityEngine;

public class PlayerInstancer : MonoBehaviour
{
    // Static instance (global access)
    public static PlayerInstancer Instance { get; private set; }

    [SerializeField] private GameObject modelPrefab;

    // Internal reference to the spawned model
    private GameObject modelInstance;

    void Awake()
    {
        // Singleton enforcement
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Creates the model if it doesn't exist yet
    /// </summary>
    private void InitializeModel()
    {
        if (modelInstance != null) return;

        modelInstance = Instantiate(modelPrefab);

        // Keep model across scenes too
        DontDestroyOnLoad(modelInstance);

        Debug.Log("Model initialized once.");
    }

    /// <summary>
    /// Public access to the model
    /// </summary>
    public GameObject GetModel()
    {
        // Lazy load safety
        if (modelInstance == null)
        {
            InitializeModel();
        }

        return modelInstance;
    }

    /// <summary>
    /// Optional: manually set position/rotation
    /// </summary>
    public void SetModelTransform(Vector3 position, Quaternion rotation)
    {
        if (modelInstance == null) return;

        modelInstance.transform.SetPositionAndRotation(position, rotation);
    }

    /// <summary>
    /// Optional: destroy model manually
    /// </summary>
    public void DestroyModel()
    {
        if (modelInstance != null)
        {
            Destroy(modelInstance);
            modelInstance = null;
        }
    }

    public void OnSceneLoadTrigger()
    {
        modelInstance.transform.position = modelPrefab.transform.position;
        modelInstance.transform.rotation = modelPrefab.transform.rotation;

        Debug.Log("Transform saved");
    }
}