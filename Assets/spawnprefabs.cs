using UnityEngine;
using UnityEditor;
using System.IO;
/*
 *In this class the idea is to spawn a zoo full of all existing gameobjects
 *To do:
 *  Correct spawn pacing 
 *  Correct null Pointer exceptions
 *  Disable player game object logic
 *  add correct flooring
 */

public class spawnprefabs : MonoBehaviour
{
    public void Start()
    {
#if UNITY_EDITOR
        BuildZoo();
#endif
    }

    // Menüeintrag in Unity
    [MenuItem("Tools/Build Zoo Scene")]
    public static void BuildZoo()
    {
        // Ordner mit Prefabs (anpassen falls nötig)
        string prefabFolder = "Assets";

        // Alle Prefabs im Ordner finden
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabFolder });

        if (guids.Length == 0)
        {
            Debug.LogWarning("No prefabs found!");
            return;
        }

        // Bestehenden Zoo löschen
        GameObject existingZoo = GameObject.Find("Zoo");
        if (existingZoo != null)
        {
            Object.DestroyImmediate(existingZoo);
        }

        // Zoo-Root erstellen
        GameObject zooRoot = new GameObject("Zoo");

        // Boden erstellen
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.parent = zooRoot.transform;
        ground.transform.position = Vector3.zero;

        // Grid-Einstellungen
        int itemsPerRow = 5;
        float spacing = 5f;

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab == null)
                continue;

            // Position im Grid berechnen
            int row = i / itemsPerRow;
            int col = i % itemsPerRow;

            Vector3 position = new Vector3(col * spacing, 0, row * spacing);

            // Prefab instanziieren
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            if (instance == null)
                continue;

            instance.transform.position = position;
            instance.transform.parent = zooRoot.transform;
            instance.name = prefab.name;

            // Komponenten bereinigen
            CleanComponents(instance);
        }

        Debug.Log("Zoo created with " + guids.Length + " prefabs.");
    }

    // Neue Funktion: Komponenten bereinigen + Rigidbody auf kinematic
    static void CleanComponents(GameObject root)
    {
        if (root == null)
            return;

        Component[] components = root.GetComponentsInChildren<Component>(true);

        foreach (Component comp in components)
        {
            if (comp == null)
                continue;

            // Transform nie anfassen
            if (comp is Transform)
                continue;

            // Renderer behalten
            if (comp is MeshRenderer)
                continue;

            // MeshFilter behalten
            if (comp is MeshFilter)
                continue;

            // Rigidbody auf kinematic setzen
            if (comp is Rigidbody rb)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                continue;
            }

            // Alle anderen Behaviour-Komponenten deaktivieren
            if (comp is Behaviour behaviour)
            {
                behaviour.enabled = false;
            }
        }
    }
}
