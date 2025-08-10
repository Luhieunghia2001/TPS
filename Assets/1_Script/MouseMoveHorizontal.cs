using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBasedMover : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // Gắn Main Camera hoặc Cinemachine Virtual Camera Follow target
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private InputActionReference lookAction; // Action Look (Mouse Delta hoặc Stick)

    private float yawInput;

    private void OnEnable()
    {
        lookAction.action.Enable();
    }

    private void OnDisable()
    {
        lookAction.action.Disable();
    }

    private void Update()
    {
        // Lấy delta chuột (hoặc tay cầm)
        Vector2 lookDelta = lookAction.action.ReadValue<Vector2>();

        // Chỉ lấy trục X để xác định lia sang trái/phải
        yawInput = lookDelta.x;

        // Nếu lia sang phải
        if (yawInput > 0.1f)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        // Nếu lia sang trái
        else if (yawInput < -0.1f)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
