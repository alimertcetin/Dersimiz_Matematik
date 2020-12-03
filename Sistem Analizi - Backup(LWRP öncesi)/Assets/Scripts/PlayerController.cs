using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    Animator anim;

    public float speed = 0f;
    public float Runspeed = 0f;
    public float turnSmoothTime = 0.1f;
    public float gravity = 1f;
    float turnSmoothVelocity;
    float horizontal;
    float vertical;
    Vector3 direction;
    Vector3 moveDir;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        moveDir.y -= gravity * Time.deltaTime;

        if (direction.magnitude >= 0.1f)
        {
            anim.SetBool("isWalking", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            if (!Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("isRunning", false);
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isRunning", true);
                controller.Move(moveDir.normalized * Runspeed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }
    }
}
