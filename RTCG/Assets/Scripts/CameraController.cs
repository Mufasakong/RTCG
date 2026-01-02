using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float fastMultiplier = 3f;
    public float mouseSensitivity = 2f;

    float yaw;
    float pitch;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        if (!Input.GetMouseButton(1)) return;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * 100f * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * 100f * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void HandleMovement()
    {
        if (!Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            speed *= fastMultiplier;

        // Scroll wheel speed adjustment
        moveSpeed += Input.mouseScrollDelta.y * 2f;
        moveSpeed = Mathf.Clamp(moveSpeed, 1f, 100f);

        Vector3 direction = new Vector3(
            Input.GetAxis("Horizontal"),
            (Input.GetKey(KeyCode.E) ? 1 : 0) - (Input.GetKey(KeyCode.Q) ? 1 : 0),
            Input.GetAxis("Vertical")
        );

        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }
}
