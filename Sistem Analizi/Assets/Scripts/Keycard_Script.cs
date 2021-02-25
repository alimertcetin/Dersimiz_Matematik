using System;
using UnityEngine;

public enum Door_and_Keycard_Level
{
    None,
    Yesil,
    Sari,
    Kirmizi
}
[RequireComponent(typeof(SaveableEntity))]
public class Keycard_Script : MonoBehaviour, ISaveable
{
    instance_Player_Inventory inventory;
    
    [SerializeField] Door_and_Keycard_Level _keycard = Door_and_Keycard_Level.None;
    public Door_and_Keycard_Level Keycard { get => _keycard; }

    [Tooltip("Toplandığında oynatılacak particle efektini bu alana ekleyin.")]
    [SerializeField]
    ParticleSystem CollectedParticle = null;
    bool triggerEntered, Collected;
    Collider col;
    MeshRenderer renderer;

    public EventHandler KeycardCollected;

    private void Awake()
    {
        col = GetComponent<Collider>();
        renderer = GetComponent<MeshRenderer>();
        inventory = FindObjectOfType<instance_Player_Inventory>();
    }

    private void Update()
    {
        if (Collected && col.enabled)
        {
            col.enabled = false;
            renderer.enabled = false;
            KeycardCollected?.Invoke(null,EventArgs.Empty);
            TryGetComponent<ObjectBasedEvents>(out ObjectBasedEvents events);
            if (events != null) events.enabled = false;
            return;
        }
        else if (triggerEntered && Input.GetKeyDown(KeyCode.F) && !Collected)
        {
            SpawnParicle();

            if (_keycard == Door_and_Keycard_Level.Yesil) inventory.KeycardEkle_Success("green");
            else if (_keycard == Door_and_Keycard_Level.Sari) inventory.KeycardEkle_Success("Yellow");
            else if (_keycard == Door_and_Keycard_Level.Kirmizi) inventory.KeycardEkle_Success("red");

            Collected = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) triggerEntered = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) triggerEntered = false;
    }

    private void SpawnParicle()
    {
        GameObject go = Instantiate(CollectedParticle.gameObject); //Particle yarat.
        go.transform.position = this.transform.position; //Particle konumunu bu Gameobject olarak belirle
        Destroy(go, 5.0f); //Particle'ı 5 saniye sonra yok et.
    }

    public object CaptureState()
    {
        return new SaveData
        {
            _isCollected = Collected
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;
        Collected = saveData._isCollected;
        if (!Collected)
        {
            col.enabled = true;
            renderer.enabled = true;
        }
    }

    [System.Serializable]
    struct SaveData
    {
        public bool _isCollected;
    }

}
