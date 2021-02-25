using UnityEngine;
using Cinemachine;
using System;
public class instance_LittlePeopleController : MonoBehaviour, ISaveable
{
    CharacterController charController;
    [HideInInspector]
    Transform cam;
    float targetAngle, Angle, TurnSmoothVelocity, TurnSmoothTime = 0.1f;
    CinemachineFreeLook cameraLook;
    float CameraSpeed_X,
             CameraSpeed_Y;
    public AudioSource mainMusic;

    [SerializeField]
    float moveSpeed = 2.1f, runSpeed = 5, jumpForce = 3.2f, gravityScale = .7f, MaxGravityForce = -3;
    private float storedVerticalAcceleration;
    float Vertical, Horizontal;
    bool _allow_Input = true;
    public bool Allow_Input { get => _allow_Input; set => _allow_Input = value; }
    bool isGrounded = false;

    Vector3 moveDir;
    [Range(0f, 1f)]
    [SerializeField] float TimeScale;

    bool LShiftDown, _spaceDown;
    public bool SpaceDown { get => _spaceDown; set => _spaceDown = value; }
    private void Awake()
    {
        cameraLook = FindObjectOfType<CinemachineFreeLook>();

        if (cameraLook == null)
            Debug.LogError("Sahnede Cinemachine FreeLook bulunamadı!!");
        else
        {
            CameraSpeed_Y = cameraLook.m_YAxis.m_MaxSpeed;
            CameraSpeed_X = cameraLook.m_XAxis.m_MaxSpeed;
        }

        cam = Camera.main.transform;
        if (cam == null)
        {
            Debug.LogError("instance_LittlePeopleController scriptine kamera atanmamış!");
            Time.timeScale = 0;
        }

        charController = GetComponent<CharacterController>();
        TimeScale = Time.timeScale;
    }

    void Update()
    {
        isGrounded = charController.isGrounded;
        Time.timeScale = TimeScale;
        if (Input.GetKeyDown(KeyCode.M)) mainMusic.enabled = mainMusic.enabled ? false : true;
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        LShiftDown = Input.GetKey(KeyCode.LeftShift);
    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (_allow_Input)
        {
            moveDir = new Vector3(Horizontal, 0f, Vertical);
            //Karakterin bakması gereken yeri belirle.
            targetAngle = Mathf.Atan2(Horizontal, Vertical) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //Karakteri yumuşak bir şekilde y ekseninde döndür
            Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
            if (moveDir.magnitude > 1f) moveDir.Normalize();
            if (Horizontal != 0 || Vertical != 0)
            {
                transform.rotation = Quaternion.Euler(0f, Angle, 0f);
                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                charController.Move(moveDir * Time.deltaTime);
            }
            if (LShiftDown)
                moveDir *= runSpeed;
            else
                moveDir *= moveSpeed;

            if (charController.isGrounded && _spaceDown)
                moveDir.y = jumpForce;
            else
            {
                moveDir.y = storedVerticalAcceleration;
                moveDir.y += Physics.gravity.y * gravityScale * Time.deltaTime;
            }
            if (moveDir.y < MaxGravityForce)
                moveDir.y = MaxGravityForce;

            cameraLook.m_YAxis.m_MaxSpeed = CameraSpeed_Y;
            cameraLook.m_XAxis.m_MaxSpeed = CameraSpeed_X;

            charController.Move(moveDir * Time.deltaTime);
            storedVerticalAcceleration = moveDir.y;
        }
        else
        {
            cameraLook.m_YAxis.m_MaxSpeed = 0;
            cameraLook.m_XAxis.m_MaxSpeed = 0;
        }
    }
    

    public object CaptureState()
    {
        var x = this.gameObject.transform.position.x;
        var y = this.gameObject.transform.position.y;
        var z = this.gameObject.transform.position.z;
        return new SaveData
        {
            positionX = x,
            positionY = y,
            positionZ = z,
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        Vector3 position;
        position.x = saveData.positionX;
        position.y = saveData.positionY;
        position.z = saveData.positionZ;
        this.gameObject.transform.position = position;
    }

    [System.Serializable]
    struct SaveData
    {
        public float positionX;
        public float positionY;
        public float positionZ;
    }
}
