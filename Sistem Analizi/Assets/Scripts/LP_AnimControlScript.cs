using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class LP_AnimControlScript : MonoBehaviour
{
    Animator anim;
    private instance_LittlePeopleController _LittlePeopleController;

    AudioSource audioSource;
    [SerializeField]
    AudioClip[] stepSound = null;
    float audioSourcePitch;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSourcePitch = audioSource.pitch;
        anim = GetComponent<Animator>();
        _LittlePeopleController = FindObjectOfType<instance_LittlePeopleController>();
    }

    void PlayAudio()
    {
        AudioClip clip = RandomClip();
        
        audioSource.PlayOneShot(clip);
    }

    AudioClip RandomClip()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            audioSource.pitch = 1.5f;
        else
            audioSource.pitch = audioSourcePitch;
        return stepSound[Random.Range(0, stepSound.Length)];
    }
    bool Walk;
    bool Run;

    void ReadInput()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.01f || Input.GetAxisRaw("Horizontal") < -0.01f ||
            Input.GetAxisRaw("Vertical") > 0.01f || Input.GetAxisRaw("Vertical") < -0.01f &&
            _LittlePeopleController.Allow_Input)
        {
            Walk = true;
        }
        else Walk = false;

        if (Walk && Input.GetKey(KeyCode.LeftShift))
        {
            Run = true;
        }
        else
        {
            Run = false;
        }

    }

    void PlayTheAnim()
    {
        anim.SetBool("Walk", Walk);
        anim.SetBool("Run", Run);
        if (Input.GetKeyDown(KeyCode.Space))
            anim.SetBool("Jump", true);
        else
            anim.SetBool("Jump", false);
        if (!_LittlePeopleController.Allow_Input)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Run", false);
            anim.SetBool("Jump", false);
        }
    }

    public IEnumerator AddJumpForce()
    {
        _LittlePeopleController.SpaceDown = true;
        yield return new WaitForFixedUpdate();
        _LittlePeopleController.SpaceDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
        PlayTheAnim();
    }
}
