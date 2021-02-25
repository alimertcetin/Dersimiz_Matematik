using UnityEngine;
using TMPro;

public class BlackBoard_Management : MonoBehaviour
{
    instance_LittlePeopleController littlePeopleController;

    [SerializeField] GameObject BlackBoard_UI_Canvas = null, NotificationCanvas = null;
    TMP_Text txt_Notification;
    bool _triggered = false;

    private void Awake()
    {
        if (NotificationCanvas != null)
            txt_Notification = NotificationCanvas.GetComponentInChildren<TMP_Text>();
        else
            Debug.LogWarning("Canvas atanmamış. " + this.name + " bildirim veremeyecek.");

        littlePeopleController = FindObjectOfType<instance_LittlePeopleController>();
    }

    private void Update()
    {
        if(_triggered)
        {
            NotificationCanvas.SetActive(!BlackBoard_UI_Canvas.activeSelf);
            if (Input.GetKeyDown(KeyCode.F))
            {
                BlackBoard_UI_Canvas.SetActive(!BlackBoard_UI_Canvas.activeSelf);
                littlePeopleController.Allow_Input = BlackBoard_UI_Canvas.activeSelf ? false : true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _triggered = true;
            txt_Notification.text = "Press F to interact with BlackBoard";
            if (NotificationCanvas != null)
                NotificationCanvas.SetActive(true);
            else Debug.LogWarning("Canvas bulunamadı.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _triggered = false;
            if (NotificationCanvas != null)
                NotificationCanvas.SetActive(false);
            else Debug.LogWarning("Canvas bulunamadı.");
        }
    }
}
