using UnityEngine;
using UnityEditor;
using System.IO;

public class spawnprefabs : MonoBehaviour
{

    public void Start()
    {
        BuildZoo();
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
            instance.transform.position = position;
            instance.transform.parent = zooRoot.transform;
            instance.name = prefab.name;
        }

        Debug.Log("Zoo created with " + guids.Length + " prefabs.");
    }
}
