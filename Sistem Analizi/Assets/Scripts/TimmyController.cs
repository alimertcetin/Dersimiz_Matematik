using UnityEngine;


public class TimmyController : MonoBehaviour
{

    Animator animator;
    CharacterController characterController;
    public float speed = 4;
    public float RunSpeed = 6;
    public float rot = 0;
    public float rotspeed = 80;
    [SerializeField]
    float gravity = 8;

    [SerializeField]
    Vector3 moveDir = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    bool Walk = false;
    bool Run = false;

    void ReadInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Walk = true;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Run = true;
            }
            else
                Run = false;
        }
        else
        {
            Walk = false;
            Run = false;
        }
        MoveTheChar(Walk, Run);
    }

    void ControlAnims()
    {
        animator.SetBool("isWalking", Walk);
        animator.SetBool("isRunning", Run);
    }

    void MoveTheChar(bool walk, bool run)
    {
        if (walk)
        {
            moveDir = new Vector3(0, 0, 1);
            moveDir *= speed;
            moveDir = transform.TransformDirection(moveDir);
        }
        if (!walk)
        {
            moveDir = Vector3.zero;
        }
        if (run)
        {
            moveDir = new Vector3(0, 0, 1);
            moveDir *= RunSpeed;
            moveDir = transform.TransformDirection(moveDir);
        }
        rot += Input.GetAxis("Horizontal") * rotspeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);

        moveDir.y -= gravity * Time.deltaTime;
        characterController.Move(moveDir * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        ReadInput();
        ControlAnims();
    }
}
