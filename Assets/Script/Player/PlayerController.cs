using UnityEngine;

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
        private Vector3 _velocity;
        private bool _isDashing;
        private float _dashTimer;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleMovement();
            HandleGravity();
        }

        private void HandleMovement()
        {
            // --- Input แบบเก่า (Legacy Input) ---
            float moveX = Input.GetAxis("Horizontal"); // กด A, D หรือ ลูกศร ซ้าย, ขวา
            float moveY = Input.GetAxis("Vertical");   // กด W, S หรือ ลูกศร บน, ล่าง
            
            // เช็คการวิ่ง (Sprint) โดยการกด Left Shift ค้างไว้
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);

            // เช็คการ Dash โดยการกดปุ่ม E หรือ Space (เปลี่ยนได้ตามใจชอบ)
            if (Input.GetKeyDown(KeyCode.LeftControl) && !_isDashing)
            {
                StartDash();
            }

            // คำนวณความเร็ว
            float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
            Vector3 move = transform.right * moveX + transform.forward * moveY;

            if (_isDashing)
            {
                // ขณะ Dash จะใช้ความเร็วคงที่ dashForce
                _characterController.Move(move * dashForce * Time.deltaTime);
                _dashTimer -= Time.deltaTime;
                
                if (_dashTimer <= 0)
                {
                    _isDashing = false;
                }
            }
            else
            {
                // การเดิน/วิ่งปกติ
                _characterController.Move(move * currentSpeed * Time.deltaTime);
            }
        }

        private void StartDash()
        {
            _isDashing = true;
            _dashTimer = dashDuration;
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