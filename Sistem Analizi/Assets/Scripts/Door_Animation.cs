using UnityEngine;
using TMPro;

[RequireComponent(typeof(Door_Status))]
public class Door_Animation : Animation_Manager_Class, ISaveable
{
    [Header("Eğer bu ikili bir kapıysa bu alana LEFT HOLDER'ı atayın..")]
    [Tooltip("First you need to add a Door_Animation Component to the HOLDER.")]
    [SerializeField] Door_Animation otherDoor = null;
    private Animator animController;
    float rnd_AnimSpeed;
    public float Rnd_AnimSpeed { get => rnd_AnimSpeed; set => rnd_AnimSpeed = value; }
    bool triggered;
    [SerializeField] bool IsThisLeftSide = true;
    bool _doorIsOpen = false;
    public bool DoorIsOpen { get => _doorIsOpen; set => _doorIsOpen = value; }

    private void Start()
    {
        animController = GetComponentInChildren<Animator>();
        if (animController == null)
            Debug.LogError("Couldnt find Door animator. Door will not open or close!");

        TryGetComponent<Door_Is_Locked>(out Door_Is_Locked _door);
        door = _door;
        TryGetComponent<DoorKeycard_Management>(out DoorKeycard_Management _keycard);
        Keycard = _keycard;
    }

    bool _doorLocked, _keycardsAreRemoved;
    public bool DoorLocked { get => _doorLocked; set => _doorLocked = value; }
    public bool KeycardsAreRemoved { get => _keycardsAreRemoved; set => _keycardsAreRemoved = value; }
    Door_Is_Locked door;
    DoorKeycard_Management Keycard;
    private void Update()
    {
        if (triggered)
        {
            if (door == null) _doorLocked = false;
            else _doorLocked = door.DoorLocked;
            if (Keycard == null) _keycardsAreRemoved = true;
            else _keycardsAreRemoved = Keycard.KeycardsAreRemoved;

            if (!_doorLocked && _keycardsAreRemoved && Input.GetKeyDown(KeyCode.F))
            {
                if (IsThisLeftSide)
                {
                    LeftSideMovement(_doorIsOpen);
                    if(otherDoor != null) otherDoor.RightSideMovement(_doorIsOpen);
                }
                else
                {
                    RightSideMovement(_doorIsOpen);
                    if(otherDoor != null) otherDoor.LeftSideMovement(_doorIsOpen);
                }
                _doorIsOpen = !_doorIsOpen;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) triggered = false;
    }

    private void RightSideMovement(bool _doorIsOpen)
    {
        if (!_doorIsOpen)
        {
            AnimationToStop(animController, "RightSide_Close");
            AnimationToPlay(animController, "RightSide_Open");
        }
        else
        {
            AnimationToStop(animController, "RightSide_Open");
            AnimationToPlay(animController, "RightSide_Close");
        }
    }

    private void LeftSideMovement(bool _doorIsOpen)
    {
        if (!_doorIsOpen)
        {
            AnimationToStop(animController, "LeftSide_Close");
            AnimationToPlay(animController, "LeftSide_Open");
        }
        else
        {
            AnimationToStop(animController, "LeftSide_Open");
            AnimationToPlay(animController, "LeftSide_Close");
        }
    }

    protected override void AnimationToPlay(Animator _animController, string animationName)
    {
        rnd_AnimSpeed = UnityEngine.Random.Range(0.5f, 1.5f);
        _animController.SetFloat("rndAnimSpeed", rnd_AnimSpeed);
        _animController.SetBool(animationName, true);

    }

    protected override void AnimationToStop(Animator animController, string animationName)
    {
        animController.SetBool(animationName, false);
    }

    public object CaptureState()
    {
        return new SaveData
        {
            _keycardsAreRemoved = KeycardsAreRemoved,
            _doorLocked = DoorLocked,
            _doorIsOpen = DoorIsOpen,
            _isThisLeftSide = IsThisLeftSide
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        KeycardsAreRemoved = saveData._keycardsAreRemoved;
        DoorLocked = saveData._doorLocked;
        DoorIsOpen = saveData._doorIsOpen;
        IsThisLeftSide = saveData._isThisLeftSide;

        if (!DoorLocked && KeycardsAreRemoved)
        {
            if (IsThisLeftSide)
            {
                LeftSideMovement(!_doorIsOpen);
                if (otherDoor != null) otherDoor.RightSideMovement(!_doorIsOpen);
            }
            else
            {
                RightSideMovement(!_doorIsOpen);
                if (otherDoor != null) otherDoor.LeftSideMovement(!_doorIsOpen);
            }
        }

        //if (IsThisLeftSide && DoorIsOpen)
        //{
        //    LeftSideMovement(false);
        //    if (otherDoor != null) otherDoor.RightSideMovement(false);
        //    _doorIsOpen = !_doorIsOpen;
        //}
        //else if (!IsThisLeftSide && DoorIsOpen)
        //{
        //    RightSideMovement(false);
        //    if (otherDoor != null) otherDoor.LeftSideMovement(false);
        //    _doorIsOpen = !_doorIsOpen;
        //}
    }

    [System.Serializable]
    struct SaveData
    {
        public bool _keycardsAreRemoved;
        public bool _doorLocked;
        public bool _doorIsOpen;
        public bool _isThisLeftSide;
    }
}
