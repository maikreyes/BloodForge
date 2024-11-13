using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //Serialize variables

    [SerializeField]
    private float _playerSpeed = 15.0f;
    [SerializeField]
    private float _jumpHeight = 1.0f;
    [SerializeField]
    private float _gravityValue = -9.81f;
    [SerializeField]
    private Transform _cam;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private Animator _animator;

    //Private varibles without inspertor requeriments

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    //Variables necesaries for input

    private Vector2 _movement = Vector2.zero;
    private bool _jump = false;
    private bool _run = false;
    private PlayerInput playerInput;

    //Camera and movement varibles

    private float _turnSmoothVelocity;
    public float smoothTime = 0.1f;

    //Animation vairbeles
    private bool isRunning = false;

    //Dependencies

    [SerializeField]
    [Header("Dependencies")]
    private CharacterController _controller;


    //Fisics funtion

    void FixedUpdate()
    {

        //Movement funtion

        Movement();

        //Run funtion

        Run();

        //Jump funtion

        Jump();

    }

    //Input Actions Callback functions

    public void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        _jump = context.action.triggered;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _run = context.action.triggered;
    }

    private void Movement()
    {
        Vector3 move = new Vector3(_movement.x, 0, _movement.y).normalized;

        //Only move if the button or joystick is press

        if (move.magnitude >= 0.1f)
        {
            //Vision angle for character use the camera position

            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;

            //Smooth move for camera 

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, smoothTime);

            //Create a rotation for character

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Move controller

            Vector3 displement = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(displement.normalized * Time.deltaTime * _playerSpeed);

            isRunning = true;

            _animator.SetBool("Running", isRunning);
        }
        else
        {
            isRunning = false;
            _animator.SetBool("Running", isRunning);
        }
    }

    private void Run()
    {
        //If run button is press, change the velocity varible

        _playerSpeed = _run ? 25f : 15f;
    }

    private void Jump()
    {
        //Validation for player touch groud

        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..

        if (_jump && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            _animator.SetTrigger("Jump");
        }

        playerVelocity.y += _gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);

        
    }
}
