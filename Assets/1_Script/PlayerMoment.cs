using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _cameraTransform;

    private float _currentMoveSpeed;

    private float _velocityY;
    private Vector2 _currentSmoothedMoveInput;

    public bool IsMoving => _currentSmoothedMoveInput.magnitude > 0.1f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        _currentMoveSpeed = PlayerStats.Instance.PlayerData.Speed;


        var rawInput = _moveAction.action.ReadValue<Vector2>();

        // Smooth input
        _currentSmoothedMoveInput.x = Mathf.Lerp(_currentSmoothedMoveInput.x, rawInput.x, Time.deltaTime * 10f);
        _currentSmoothedMoveInput.y = Mathf.Lerp(_currentSmoothedMoveInput.y, rawInput.y, Time.deltaTime * 10f);

        // Gửi vào Animator
        animator.SetFloat("Pox X", _currentSmoothedMoveInput.x);
        animator.SetFloat("Pox Y", _currentSmoothedMoveInput.y);
        animator.SetBool("IsMoving", IsMoving);

        // Tính hướng di chuyển theo camera
        Vector3 camForward = _cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = _cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        var direction = camForward * rawInput.y + camRight * rawInput.x;
        var newVelocity = direction * _currentMoveSpeed;

        // Nhảy
        if (_jumpAction.action.triggered && _controller.isGrounded)
        {
            _velocityY = _jumpForce;
        }
        else
        {
            UpdateFalling();
        }

        newVelocity.y = _velocityY;
        _controller.Move(newVelocity * Time.deltaTime);
    }

    private void UpdateFalling()
    {
        if (_controller.isGrounded)
        {
            _velocityY = -1;
        }
        else
        {
            _velocityY += Physics.gravity.y * Time.deltaTime;
        }
    }
}
