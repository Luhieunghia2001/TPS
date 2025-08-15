using UnityEngine;

public class CameraBasedMover : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Vector3 initialOffset; 
    private float lastYaw;

    private void Start()
    {
        initialOffset = transform.position - cameraTransform.position;
        lastYaw = cameraTransform.eulerAngles.y;
    }

    private void Update()
    {
        float currentYaw = cameraTransform.eulerAngles.y;
        float yawDelta = Mathf.DeltaAngle(lastYaw, currentYaw);

        if (Mathf.Abs(yawDelta) > 0.01f) 
        {
            initialOffset = Quaternion.AngleAxis(yawDelta, Vector3.up) * initialOffset;

            transform.position = cameraTransform.position + initialOffset;
        }

        lastYaw = currentYaw;
    }
}
