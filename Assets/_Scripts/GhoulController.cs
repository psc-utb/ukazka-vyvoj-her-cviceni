using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Animator))]
public class GhoulController : MonoBehaviour
{
    Animator _animator;

    [SerializeField]
    [Range(0.5f,10)]
    float speed = 1;

    [SerializeField]
    float verticalVelocity = 5;

    InputAction moveInputAction;
    InputAction jumpInputAction;

    Rigidbody rigidbody;

    // Awake is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();

        moveInputAction = InputSystem.actions.FindAction("Move");
        jumpInputAction = InputSystem.actions.FindAction("Jump");

        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float vx = Input.GetAxis("Horizontal");
        //float vz = Input.GetAxis("Vertical");

        Vector2 moveVector = moveInputAction.ReadValue<Vector2>();
        float vx = moveVector.x;
        float vz = moveVector.y;

        if (vx != 0 || vz != 0)
        {
            _animator.SetBool("IsMoving", true);

            Vector3 movementX = speed * vx * Vector3.right * Time.deltaTime;
            Vector3 movementZ = speed * vz * Vector3.forward * Time.deltaTime;
            transform.Translate(movementX + movementZ);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }

        if (isGrounded)
        {
            if (jumpInputAction.WasPressedThisFrame())
            {
                rigidbody.linearVelocity = new Vector3(0, verticalVelocity, 0);
            }
        }
    }

    [SerializeField]
    bool isGrounded;
    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
