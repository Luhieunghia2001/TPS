using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private float _jumpForce;

    private float _velocityY;

    private Vector2 _currentSmoothedMoveInput;

    public bool IsMoving => _currentSmoothedMoveInput.magnitude > 0.01f;

    [SerializeField] private float _layerWeightSmoothSpeed = 5f;

    private float _currentShootRunLayerWeight = 0f;
    private float _targetShootRunLayerWeight = 0f;

    private float _currentReloadRunLayerWeight = 0f;
    private float _targetReloadRunLayerWeight = 0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var rawInput = _moveAction.action.ReadValue<Vector2>();

        _currentSmoothedMoveInput.x = Mathf.Lerp(_currentSmoothedMoveInput.x, rawInput.x, Time.deltaTime * 10f);
        _currentSmoothedMoveInput.y = Mathf.Lerp(_currentSmoothedMoveInput.y, rawInput.y, Time.deltaTime * 10f);

        var direction = transform.forward * rawInput.y + transform.right * rawInput.x;
        var newVelocity = direction * _moveSpeed;

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