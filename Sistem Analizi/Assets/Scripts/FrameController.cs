using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameController : MonoBehaviour
{
    public static FrameController instance;
    [SerializeField] int _frameRate = 30;
    public int FrameRate
    {
        get { return _frameRate; }
        set
        {
            if (value >= 60) _frameRate = 60;
            else if (value < 60) _frameRate = 30;
            else _frameRate = 30;
        }
    }
    [SerializeField] private bool VSync;
    public int VsyncCount
    {
        get
        {
            if (VSync) return 1;
            else return 0;
        }
        set
        {
            VSync = !VSync;
        }
    }

    void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        Application.targetFrameRate = _frameRate;
        if (VSync) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;
    }
}
