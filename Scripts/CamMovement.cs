using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mousesensitivityX;
    public float mousesensitivityY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

        // Mouse Input + Sensitivity adjustments
        float inputX = Input.GetAxis("Mouse X") * Time.deltaTime * (mousesensitivityX-300);
        float inputY = Input.GetAxis("Mouse Y") * Time.deltaTime * (mousesensitivityY-300);

        yRotation += inputX;
        xRotation -= inputY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Cam rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}