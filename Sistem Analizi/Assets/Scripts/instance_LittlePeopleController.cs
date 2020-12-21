using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class instance_LittlePeopleController : MonoBehaviour
{

    #region /*---------------------- Variables ----------------------*/


    //---Awake
    CharacterController charController;
    public GameObject GO_BlackBoard_UI;
    instance_Player_Inventory inventory;
    instance_btnManager btnManager;

    //---ReadInput
    [HideInInspector]
    public float Vertical, Horizontal;
    [SerializeField]
    bool KeyPressed_Vertical, KeyPressed_Horizontal, KeyPressed_Space;

    //instance_OpenTheDoor and instance_btnManager is controlling this variable.
    public bool Allow_Input = true;

    //---MoveTheChar
    [SerializeField]
    float speed = 2, runspeed = 4, gravity = 1f, GravityMultiplier = 1, NeededTimeForStartJump = 0, NeededTimeFor_EndJump = 0;
    Transform cam; // Main Camera buraya atanacak. Karakteri hareket ettirirken kamera yönünden bağımsız olması için.
    float targetAngle, Angle, timer = 0, TurnSmoothVelocity, TurnSmoothTime = 0.1f;
    Vector3 direction, GravityControl, GravityControl2; //GravityControl is in the Update function right now.

    //---JumpMethod
    bool startcountdown = false, Jump = false;

    //---Camera
    //Needs Cinemachine library.
    CinemachineFreeLook cameraLook;
    float CameraSpeed_X,
             CameraSpeed_Y;


    [SerializeField]
    float Delay = 0, TempDelay;
    bool startDelay;
    private bool BlackBoard_triggered;


    #endregion


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
        if(cam == null)
        {
            Debug.LogError("instance_LittlePeopleController scriptine kamera atanmamış!");
            Time.timeScale = 0;
        }

        inventory = FindObjectOfType<instance_Player_Inventory>();
        charController = GetComponent<CharacterController>();
        btnManager = FindObjectOfType<instance_btnManager>();
        TempDelay = Delay;
    }


    void ReadInput()
    {
        //Yatay eksendeki girdiler
        Horizontal = Input.GetAxisRaw("Horizontal");
        //Dikey eksendeki girdiler.
        Vertical = Input.GetAxisRaw("Vertical");
        

        //CharacterController isGrounded true ise yani karakter yerdeyse space girdisini oku.
        if (charController.isGrounded)
        {
            KeyPressed_Space = Input.GetKeyDown(KeyCode.Space);
        }

        //Works same as the other if else statement.
        //Eğer Horizontal 0'a eşit değilse KeyPressed_Horizontal'ı true yap. Eşitse false döndür.
        KeyPressed_Horizontal = Horizontal != 0 ? true : false;

        //Eğer Vertical 0'a eşit değilse KeyPressed_Vertical'ı true yap. Eşitse false döndür.
        if (Vertical != 0)
        {
            KeyPressed_Vertical = true;
        }
        else
            KeyPressed_Vertical = false;

        //Eğer girişe izin verilmezse karakter herhangi bir giriş verisi almayacak.
        //Fakat basılan tuşlar hala okunuyor olacak.
        if (Allow_Input)
        {
            MoveTheChar(Horizontal, Vertical);

            cameraLook.m_YAxis.m_MaxSpeed = CameraSpeed_Y;
            cameraLook.m_XAxis.m_MaxSpeed = CameraSpeed_X;
            //cameraLook.m_YAxis.m_InputAxisName = "Mouse Y";
            //cameraLook.m_XAxis.m_InputAxisName = "Mouse X";
        }
        else
        {
            cameraLook.m_YAxis.m_MaxSpeed = 0;
            cameraLook.m_XAxis.m_MaxSpeed = 0;
            //cameraLook.m_YAxis.m_InputAxisName = "";
            //cameraLook.m_XAxis.m_InputAxisName = "";
        }

    }
    
    public AudioSource mainMusic;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) { mainMusic.enabled = mainMusic.enabled ? false : true; }

        ReadInput();

        if (KeyPressed_Space)
            startcountdown = true;

        if (startcountdown)
        {
            timer += Time.deltaTime;
            if (timer >= NeededTimeForStartJump)
            {
                timer = 0;
                Jump = true;
                startcountdown = false;
            }
        }

        if (startDelay)
        {
            TempDelay -= Time.deltaTime;
            if (TempDelay <= 0)
            {
                TempDelay = Delay;
                startDelay = false;
            }
        }

        if (BlackBoard_triggered && Input.GetKeyDown(KeyCode.F))
        {
            GO_BlackBoard_UI.SetActive(!GO_BlackBoard_UI.activeSelf);

            if (GO_BlackBoard_UI.activeSelf)
                Allow_Input = false;
            else
                Allow_Input = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Jump)
        {
            JumpMethod();
        }
        if(!startDelay)
        {
            //Karaktere yerçekimi ekle
            GravityControl = new Vector3(0, direction.y - gravity * Time.deltaTime, 0);
            charController.Move(GravityControl);
        }

    }

    //The function that called by ReadInput and also takes the variables from ReadInput function
    void MoveTheChar(float _horizontal, float _vertical)
    {
        //Karakterin bakması gereken yeri belirle.
        targetAngle = Mathf.Atan2(_horizontal, _vertical) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //Karakteri yumuşak bir şekilde y ekseninde döndür
        Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
        direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


        if (KeyPressed_Horizontal || KeyPressed_Vertical)
        {
            transform.rotation = Quaternion.Euler(0f, Angle, 0f);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                charController.Move(direction.normalized * runspeed * Time.deltaTime);
            }
            else
                charController.Move(direction.normalized * speed * Time.deltaTime);
        }

    }
    /* MoveTheChar tarafından çağırılan fonksiyon. */
    void JumpMethod()
    {
        //Eğer Space basıldıysa yerçekimi tersine çevir
        //böylece karakter zıplıyormuş gibi görünecek.
        GravityControl2 = new Vector3(0, direction.y + (gravity * GravityMultiplier) * Time.deltaTime, 0);
        charController.Move(GravityControl2);
        timer += Time.deltaTime;
        if (timer >= NeededTimeFor_EndJump)
        {
            timer = 0;
            startDelay = true;
            Jump = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "BlackBoard")
            BlackBoard_triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        BlackBoard_triggered = false;
    }
}
