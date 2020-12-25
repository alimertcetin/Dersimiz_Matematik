using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_End_Script : MonoBehaviour
{
    instance_OpenTheDoor openTheDoor;
    bool TriggerEnter;

    void Start()
    {
        openTheDoor = GetComponent<instance_OpenTheDoor>();
    }

    void Update()
    {
        if(TriggerEnter && !openTheDoor.DoorIsLocked && openTheDoor.AllowToOpen && Input.GetKey(KeyCode.F))
        {
            Scene s = SceneManager.GetActiveScene();
            SceneManager.LoadScene(s.buildIndex + 1);
            if (s.buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadSceneAsync(s.buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) TriggerEnter = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) TriggerEnter = false;
    }
}
