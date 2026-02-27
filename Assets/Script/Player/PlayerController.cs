using UnityEngine;
using UnityEngine.InputSystem;

namespace PanicLab.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float sprintSpeed = 8f;
        [SerializeField] private float dashForce = 15f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float gravity = -9.81f;

        private CharacterController _characterController;
        private Vector2 _moveInput;
        private Vector3 _velocity;
        private bool _isSprinting;
        private bool _isDashing;
        private float _dashTimer;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnSprint(InputValue value)
        {
            _isSprinting = value.isPressed;
        }

        public void OnDash(InputValue value)
        {
            if (value.isPressed && !_isDashing)
            {
                StartDash();
            }
        }

        private void StartDash()
        {
            _isDashing = true;
            _dashTimer = dashDuration;
        }

        private void Update()
        {
            HandleMovement();
            HandleGravity();
        }

        private void HandleMovement()
        {
            float currentSpeed = _isSprinting ? sprintSpeed : walkSpeed;
            
            Vector3 move = transform.right * _moveInput.x + transform.forward * _moveInput.y;

            if (_isDashing)
            {
                _characterController.Move(move * dashForce * Time.deltaTime);
                _dashTimer -= Time.deltaTime;
                if (_dashTimer <= 0)
                {
                    _isDashing = false;
                }
            }
            else
            {
                _characterController.Move(move * currentSpeed * Time.deltaTime);
            }
        }

        private void HandleGravity()
        {
            if (_characterController.isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }

            _velocity.y += gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }
    }
}
