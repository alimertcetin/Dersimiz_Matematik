using UnityEngine;

public class LittlePeopleController : MonoBehaviour
{
    /*---------------------- Variables ----------------------*/
    //---Start
    CharacterController charController;

    //---ReadInput
    float Vertical , Horizontal;
    [SerializeField]
    bool KeyPressed_Vertical, KeyPressed_Horizontal, KeyPressed_LeftShift, KeyPressed_Space, Allow_Input = true;

    //---MoveTheChar
    [SerializeField]
    float speed = 2, runspeed = 4, gravity = 1f, GravityMultiplier = 1, NeededTimeForStartJump = 0, NeededTimeFor_EndJump = 0;
    public Transform cam;
    float targetAngle, Angle, timer = 0, TurnSmoothVelocity, TurnSmoothTime = 0.1f;
    Vector3 direction, GravityControl, GravityControl2; //GravityControl is in the Update function right now.

    //---JumpMethod
    [SerializeField] //To see whats goin on
    bool startcountdown = false, Jump = false;

    private void Start()
    {
        charController = GetComponent<CharacterController>();
        TempDelay = Delay;
    }

    
    void ReadInput()
    {
        //Yatay eksendeki girdiler
        Horizontal = Input.GetAxisRaw("Horizontal");
        //Dikey eksendeki girdiler.
        Vertical = Input.GetAxisRaw("Vertical");

        //Sol Shift girdisi.
        KeyPressed_LeftShift = Input.GetKey(KeyCode.LeftShift);

        //CharacterController isGrounded true ise yani karakter yerdeyse space girdisini oku.
        if (charController.isGrounded)
        {
            KeyPressed_Space = Input.GetKey(KeyCode.Space);
        }
        else
            KeyPressed_Space = false;

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
            MoveTheChar(Horizontal, Vertical);

    }

    //ReadInput tarafından çağırılan ve okunan girdileri alan fonksiyon.
    //The function that called by ReadInput and also takes the variables from ReadInput function
    void MoveTheChar(float _horizontal, float _vertical)
    {
        //Karakterin bakması gereken yeri belirle.
        targetAngle = Mathf.Atan2(_horizontal, _vertical) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //Karakteri yumuşak bir şekilde y ekseninde döndür
        Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
        direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;


        JumpMethod();


        if (KeyPressed_Horizontal || KeyPressed_Vertical)
        {
            transform.rotation = Quaternion.Euler(0f, Angle, 0f);
            if(KeyPressed_LeftShift)
            {
                charController.Move(direction.normalized * runspeed * Time.deltaTime);
            }
            else
                charController.Move(direction.normalized * speed * Time.deltaTime);
        }

    }

    /* Girişlerden bağımsız çalışacak fonksiyon. ---DÜZENLENECEK---
    void MoveTheChar_Independent(float _horizontal, float _vertical)
    {
        //Karakterin bakması gereken yeri belirle.
        targetAngle = Mathf.Atan2(_horizontal, _vertical) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //Karakteri yumuşak bir şekilde y ekseninde döndür
        Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
        direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        //Eğer Space basıldıysa yerçekimi tersine çevir
        //böylece karakter zıplıyormuş gibi görünecek.
        if (KeyPressed_Space)
        {
            startcountdown = true;
        }
        if (startcountdown)
        {
            timer += Time.deltaTime;
            if (timer >= NeededTimeForStartJump)
            {
                Jump = true; //Sıralamanın Önemi.
                timer = 0;
                startcountdown = false;
            }
        }
        if (Jump)
        {
            float tempGrav = 0; tempGrav -= gravity * Time.deltaTime;
            GravityControl = new Vector3(0, direction.y - tempGrav * GravityMultiplier * Time.deltaTime, 0);
            charController.Move(GravityControl);
            timer += Time.deltaTime;
            if (timer >= NeededTimeFor_EndJump)
            {
                timer = 0;
                Jump = false;
            }
        }

        //Karaktere yerçekimi ekle
        GravityControl = new Vector3(0, direction.y - gravity * Time.deltaTime, 0);
        charController.Move(GravityControl);


        if (KeyPressed_Horizontal || KeyPressed_Vertical)
        {
            transform.rotation = Quaternion.Euler(0f, Angle, 0f);
            if (KeyPressed_LeftShift)
            {
                charController.Move(direction.normalized * runspeed * Time.deltaTime);
            }
            else
                charController.Move(direction.normalized * speed * Time.deltaTime);
        }

    }
    */

    [SerializeField]
    float Delay = 0, TempDelay;
    bool startDelay;
    /* MoveTheChar tarafından çağırılan fonksiyon. */
    void JumpMethod()
    {
        //Eğer Space basıldıysa yerçekimi tersine çevir
        //böylece karakter zıplıyormuş gibi görünecek.
        if(KeyPressed_Space)
        { startcountdown = true; }

        if (startcountdown)
        {
            timer += Time.deltaTime;
            if (timer >= NeededTimeForStartJump)
            {
                timer = 0;
                Jump = true; //Sıralamanın Önemi.
                startcountdown = false;
            }
        }
        if (Jump)
        {
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

    }
    
    // Update is called once per frame
    void Update()
    {
        if (startDelay)
        {
            TempDelay -= Time.deltaTime;
            if (TempDelay <= 0)
            {
                TempDelay = Delay;
                startDelay = false;
            }
        }
        else
        {
            //Karaktere yerçekimi ekle
            GravityControl = new Vector3(0, direction.y - gravity * Time.deltaTime, 0);
            charController.Move(GravityControl);
        }
        
        ReadInput();
    }

}
