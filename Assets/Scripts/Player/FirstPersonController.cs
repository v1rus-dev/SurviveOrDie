using UnityEngine;

namespace Player
{
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private Transform playerCamera;
        [SerializeField] private Transform checkGroundPlace;

        [SerializeField] [Range(0f, 10f)] private float mouseSensitivity = 3f;
        [SerializeField] [Range(0f, 10f)] private float movementSpeed = 5f;
        [SerializeField] private float jumpSpeed = 5f;
        [SerializeField] private float mass = 1f;

        private CharacterController _characterController;

        #region InputValues

        private float _horizontal;
        private float _vertical;

        private float _mouseX;
        private float _mouseY;

        private bool _isGrounded = true;

        private Vector2 _look;
        private Vector3 _velocity;

        private float _previousLocalCameraHeight;
        private bool _isCrouching;
        private float _originalMoveSpeed;
        private float _originalMouseSensitivity;

        #endregion

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _originalMoveSpeed = movementSpeed;
            _originalMouseSensitivity = mouseSensitivity;
        }

        // Update is called once per frame
        private void Update()
        {
            HandleMovement();
            CheckIsGround();
            UpdateGravity();
            UpdateRotation();
            UpdateMovement();
        }
        
        public void ChangeMouseSensitivity(float value)
        {
            mouseSensitivity = value;
        }
        
        public void ResetMouseSensitivity()
        {
            mouseSensitivity = _originalMouseSensitivity;
        }

        #region Utils

        private void CheckIsGround()
        {
            RaycastHit[] hits = Physics.RaycastAll(checkGroundPlace.transform.position, Vector3.down, 0.1f);
            bool isGround = false;
            foreach (var raycastHit in hits)
            {
                if (raycastHit.transform.CompareTag("Player"))
                {
                    continue;
                }

                isGround = true;
            }
        
            _isGrounded = isGround;
        
        }

        #endregion

        #region Update Movements

        private void UpdateGravity()
        {
            var gravity = Physics.gravity * (mass * Time.deltaTime);
            _velocity.y = _characterController.isGrounded ? -1f : _velocity.y + gravity.y;
        }

        private void UpdateRotation()
        {
            transform.localRotation = Quaternion.Euler(0f, _look.x, 0f);
            _look.y = Mathf.Clamp(_look.y, -89f, 89f);
            playerCamera.localRotation = Quaternion.Euler(-_look.y, 0f, 0f);
        }

        private void UpdateMovement()
        {
            var input = new Vector3();
            var transform1 = transform;
            input += transform1.forward * _vertical;
            input += transform1.right * _horizontal;
            input = Vector3.ClampMagnitude(input, 2f);

            if (Input.GetButtonDown("Jump") && _characterController.isGrounded)
            {
                _velocity.y += jumpSpeed;
            }

            _characterController.Move((input * movementSpeed + _velocity) * Time.deltaTime);
        }

        #endregion

        #region Handle Movement

        private void HandleMovement()
        {
            HandleKeyboardInput();
            HandleMouseInput();
            HandleSpeed();
        }

        private void HandleMouseInput()
        {
            _mouseX = Input.GetAxis("Mouse X");
            _mouseY = Input.GetAxis("Mouse Y");

            _look.x += _mouseX * mouseSensitivity;
            _look.y += _mouseY * mouseSensitivity;
        }

        private void HandleKeyboardInput()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
        }

        private void HandleSpeed()
        {
            if (Input.GetKey(KeyCode.LeftShift) && _isGrounded)
            {
                movementSpeed = 10f;
            }
            else
            {
                movementSpeed = _originalMoveSpeed;
            }
        }

        #endregion
    }
}