using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    public float sensitivityX = 10f;
    public float sensitivityY = 10f;

    public float minY = -20f;
    public float maxY = 80f;

    public float zoomSensitivity = 2f;
    public float minZoomDistance = 2f;
    public float maxZoomDistance = 10f;

    private float rotationY = 0f;
    private float currentZoom;
    private float currentRotationX;

    void Start()
    {
        currentZoom = offset.magnitude;
        currentRotationX = transform.eulerAngles.y;
        transform.position = target.position + offset.normalized * currentZoom;
        transform.LookAt(target);
    }

    void LateUpdate()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        AdjustZoom(scrollInput);

        if (Input.GetMouseButton(0))
        {
            float rotationX = Input.GetAxis("Mouse X") * sensitivityX;
            float rotationYInput = Input.GetAxis("Mouse Y") * sensitivityY;
            RotateCamera(rotationX, rotationYInput, false);
        }
        else if (Input.GetMouseButton(1))
        {
            float rotationX = Input.GetAxis("Mouse X") * sensitivityX;
            float rotationYInput = Input.GetAxis("Mouse Y") * sensitivityY;
            RotateCamera(rotationX, rotationYInput, true);
        }

        UpdateCameraPosition();
    }

    void RotateCamera(float rotationX, float rotationYInput, bool rotatePlayer)
    {
        currentRotationX += rotationX;
        rotationY -= rotationYInput;
        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        Quaternion rotation = Quaternion.Euler(rotationY, currentRotationX, 0);
        transform.position = target.position + rotation * offset.normalized * currentZoom;

        transform.LookAt(target);

        if (rotatePlayer)
        {
            Vector3 lookDirection = transform.position - target.position;
            lookDirection.y = 0;
            target.rotation = Quaternion.LookRotation(-lookDirection);
        }
    }

    void AdjustZoom(float scrollInput)
    {
        currentZoom -= scrollInput * zoomSensitivity;
        currentZoom = Mathf.Clamp(currentZoom, minZoomDistance, maxZoomDistance);
    }

    void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(rotationY, currentRotationX, 0);
        transform.position = target.position + rotation * offset.normalized * currentZoom;
    }
}
