using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Movement")]
    public float moveSpeed = 6f;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Crouching")]
    public float crouchScale = 0.75f;
    public float crouchSpeed = 1f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Sprinting Effects")]
    [SerializeField] Camera cam;
    [SerializeField] float sprintFOV = 100f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftControl;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftShift;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;
    Vector3 movement;

    [HideInInspector] public bool isCrouching;
    [HideInInspector] public bool isSprinting;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlSpeed();

        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }

        if (Input.GetKeyDown(crouchKey) && !isCrouching)
        {
            Crouch();
        }
        else if (Input.GetKeyUp(crouchKey) && isCrouching)
        {
            UnCrouch();
        }

        if (isSprinting)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFOV, 8f * Time.deltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 90f, 8f * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    void MyInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = orientation.forward * moveVertical + orientation.right * moveHorizontal;
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        Vector3 newPosition = rb.position + movement * Time.deltaTime;
        rb.MovePosition(newPosition);
    }

    void Crouch()
    {
        Vector3 _crouchScale = new Vector3(transform.localScale.x, crouchScale, transform.localScale.z);
        transform.localScale = _crouchScale;

        isCrouching = true;
    }

    void UnCrouch()
    {
        Vector3 normalScale = new Vector3(transform.localScale.x, 0.9f, transform.localScale.z);
        transform.localScale = normalScale;

        isCrouching = false;
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey))
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            isSprinting = true;
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            isSprinting = false;
        }

        if (Input.GetKey(crouchKey))
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }
}
