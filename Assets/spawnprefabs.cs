using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.WSA;
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
    // Ordner mit Prefabs (anpassen falls nötig)
    public string prefabFolder = "Assets/Prefabs";

    // Ordner die ausgeschlossen werden sollen
    public string[] excludedFolders;

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
        spawnprefabs instance = FindObjectOfType<spawnprefabs>();

        if (instance == null)
        {
            Debug.LogError("No spawnprefabs component found in scene.");
            return;
        }

        string prefabFolder = instance.prefabFolder;
        string[] excludedFolders = instance.excludedFolders;

        // Alle Prefabs im Ordner finden
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabFolder });

        if (guids == null || guids.Length == 0)
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

        // Grid-Einstellungen
        int itemsPerRow = 5;
        float spacing = 5f;

        int spawnedCount = 0;

        // Dictionary für Folder-Parent Objekte
        System.Collections.Generic.Dictionary<string, GameObject> folderParents =
            new System.Collections.Generic.Dictionary<string, GameObject>();

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);

            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("Invalid path for GUID: " + guids[i]);
                continue;
            }

            // Prüfen ob Prefab in ausgeschlossenem Ordner liegt
            bool isExcluded = false;

            if (excludedFolders != null)
            {
                foreach (string folder in excludedFolders)
                {
                    if (!string.IsNullOrEmpty(folder) && path.StartsWith(folder))
                    {
                        isExcluded = true;
                        break;
                    }
                }
            }

            if (isExcluded)
                continue;

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogWarning("Could not load prefab at path: " + path);
                continue;
            }

            // ===== REKURSIVE ORDNERSTRUKTUR =====

            string relativePath = path.Replace(prefabFolder + "/", "");

            // Windows-Fix: Backslashes normalisieren
            relativePath = relativePath.Replace("\\", "/");

            string directoryPath = Path.GetDirectoryName(relativePath);

            // Auch hier normalisieren
            if (!string.IsNullOrEmpty(directoryPath))
                directoryPath = directoryPath.Replace("\\", "/");

            GameObject currentParent = zooRoot;

            if (!string.IsNullOrEmpty(directoryPath))
            {
                string[] folders = directoryPath.Split('/');

                string cumulativePath = "";

                foreach (string folder in folders)
                {
                    if (string.IsNullOrEmpty(folder))
                        continue;

                    cumulativePath = string.IsNullOrEmpty(cumulativePath)
                        ? folder
                        : cumulativePath + "/" + folder;

                    if (!folderParents.ContainsKey(cumulativePath))
                    {
                        GameObject newParent = new GameObject(folder);
                        newParent.transform.parent = currentParent.transform;
                        folderParents.Add(cumulativePath, newParent);
                    }

                    currentParent = folderParents[cumulativePath];
                }
            }

            GameObject parentObject = currentParent;

            // Position im Grid berechnen
            int row = spawnedCount / itemsPerRow;
            int col = spawnedCount % itemsPerRow;

            Vector3 position = new Vector3(col * spacing, 0, row * spacing);

            // Prefab instanziieren
            GameObject instanceObj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

            if (instanceObj == null)
            {
                Debug.LogWarning("Could not instantiate prefab: " + prefab.name);
                continue;
            }

            instanceObj.transform.position = position;
            instanceObj.transform.parent = parentObject.transform;
            instanceObj.name = prefab.name;

            // Komponenten bereinigen
            CleanComponents(instanceObj);

            spawnedCount++;
        }
        Debug.Log("Zoo created with " + spawnedCount + " prefabs.");
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