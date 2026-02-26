using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/**
* This class implements the movement of the Camera in the Zoo scene 
* @author Eric Wolf
* @version 19.02.2026
**/
public class CameraMovementZoo : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float verticalSpeed = 5f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private bool isFrozen = false;

    void Start()
    {
        LockCursor();

        Vector3 rot = transform.eulerAngles;
        rotationX = rot.x;
        rotationY = rot.y;
    }

    void Update()
    {
        HandleFreezeToggle();

        if (isFrozen)
            return;

        HandleMouseLook();
        HandleMovement();
        HandleMiddleClickPrefabPath();
    }

    // ==========================================
    // ESC → Freeze / Unfreeze Camera
    // ==========================================
    void HandleFreezeToggle()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isFrozen = !isFrozen;

            if (isFrozen)
                UnlockCursor();
            else
                LockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ==========================================
    // Mouse Look
    // ==========================================
    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        rotationY += mouseX;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    // ==========================================
    // Movement
    // ==========================================
    void HandleMovement()
    {
        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= 2f;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (Input.GetKey(KeyCode.LeftControl))
            move += Vector3.down * verticalSpeed;

        if (Input.GetKey(KeyCode.Space))
            move += Vector3.up * verticalSpeed;

        transform.position += move * speed * Time.deltaTime;
    }

    // ==========================================
    // Middle Mouse → Get Prefab Path
    // ==========================================
    void HandleMiddleClickPrefabPath()
    {
        if (Input.GetMouseButtonDown(2)) // Middle Mouse Button
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f))
            {
                GameObject hitObject = hit.collider.gameObject;

                // Oberstes Root-Objekt ermitteln
                GameObject rootObject = hitObject.transform.root.gameObject;

                // Hierarchiepfad generieren
                string hierarchyPath = GetHierarchyPath(hitObject.transform);

                Debug.Log("Hit GameObject: " + rootObject.name);
                Debug.Log("Hierarchy Path: " + hierarchyPath);

#if UNITY_EDITOR
                // Objekt im Hierarchy-Fenster markieren
                UnityEditor.Selection.activeGameObject = rootObject;
#endif
            }
            else
            {
                Debug.Log("No GameObject hit.");
            }
        }
    }
    string GetHierarchyPath(Transform current)
    {
        string path = current.name;

        while (current.parent != null)
        {
            current = current.parent;
            path = current.name + "/" + path;
        }

        return path;
    }
}