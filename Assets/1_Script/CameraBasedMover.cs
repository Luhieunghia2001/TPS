using UnityEngine;

public class CameraBasedMover : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Vector3 initialOffset; // khoảng cách ban đầu giữa object và camera
    private float lastYaw;

    private void Start()
    {
        // Lưu khoảng cách ban đầu giữa object và camera
        initialOffset = transform.position - cameraTransform.position;
        lastYaw = cameraTransform.eulerAngles.y;
    }

    private void Update()
    {
        float currentYaw = cameraTransform.eulerAngles.y;
        float yawDelta = Mathf.DeltaAngle(lastYaw, currentYaw);

        if (Mathf.Abs(yawDelta) > 0.01f) // tránh jitter
        {
            // Xoay offset quanh trục Y theo góc yawDelta
            initialOffset = Quaternion.AngleAxis(yawDelta, Vector3.up) * initialOffset;

            // Cập nhật lại vị trí theo camera
            transform.position = cameraTransform.position + initialOffset;
        }

        lastYaw = currentYaw;
    }
}
