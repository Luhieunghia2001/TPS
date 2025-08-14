using UnityEngine;
using Unity.Cinemachine;

public class ZomCamera : MonoBehaviour
{
    // Tham chiếu đến Cinemachine Virtual Camera, đã đổi tên thành CinemachineCamera.
    [SerializeField] private CinemachineCamera virtualCamera;

    // Các giá trị FOV cho trạng thái bình thường và trạng thái đã zoom.
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float zoomFOV = 30f;

    // Tốc độ chuyển đổi giữa hai FOV.
    [SerializeField] private float zoomSpeed = 5f;

    // Biến để lưu trữ FOV mục tiêu.
    private float targetFOV;

    void Start()
    {
        // Khởi tạo FOV mục tiêu ban đầu là FOV bình thường.
        targetFOV = normalFOV;
    }

    void Update()
    {
        // Kiểm tra xem chuột phải có đang được nhấn không.
        if (Input.GetMouseButton(1))
        {
            // Nếu có, đặt FOV mục tiêu là FOV khi zoom.
            targetFOV = zoomFOV;
        }
        else
        {
            // Nếu không, đặt FOV mục tiêu là FOV bình thường.
            targetFOV = normalFOV;
        }

        // Chuyển đổi mượt mà FOV hiện tại của camera về FOV mục tiêu.
        if (virtualCamera != null)
        {
            virtualCamera.Lens.FieldOfView = Mathf.Lerp(
                virtualCamera.Lens.FieldOfView,
                targetFOV,
                Time.deltaTime * zoomSpeed
            );
        }
        else
        {
            Debug.LogError("Lỗi: Không tìm thấy Cinemachine Virtual Camera!");
        }
    }
}
