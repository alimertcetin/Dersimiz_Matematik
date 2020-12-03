using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LP_AnimControlScript : MonoBehaviour
{
    Animator anim;
    private instance_LittlePeopleController _LittlePeopleController;


    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        _LittlePeopleController = FindObjectOfType<instance_LittlePeopleController>();
    }

    bool Walk;
    bool Run;
    bool KeyPressed_Space;
    float Horizontal;
    float Vertical;

    void ReadInput()
    {
        Horizontal = _LittlePeopleController.Horizontal;
        Vertical = _LittlePeopleController.Vertical;

        KeyPressed_Space = Input.GetKey(KeyCode.Space);

        if (Horizontal != 0 || Vertical != 0)
        {
            Walk = true;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Run = true;
            }
            else
            {
                Run = false;
            }
        }
        else
        {
            Walk = false;
            Run = false;
        }

    }

    void PlayTheAnim()
    {
        anim.SetBool("Walk", Walk);
        anim.SetBool("Run", Run);
        anim.SetBool("Jump", KeyPressed_Space);
    }

    // Update is called once per frame
    void Update()
    {
        if (_LittlePeopleController.Allow_Input == true)
        {
            ReadInput();
            PlayTheAnim();
        }
    }
}
