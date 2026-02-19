using UnityEngine;

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

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 rot = transform.eulerAngles;
        rotationX = rot.x;
        rotationY = rot.y;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        rotationY += mouseX;

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

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
}
