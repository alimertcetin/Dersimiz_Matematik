using System;
using System.Collections;
using UnityEngine;

public class GamePlayCanvasManager : MonoBehaviour
{
    [Header("Warning Settings")]
    [SerializeField] private float WarningTime = 2f;

    [Header("UI Elements")]
    [SerializeField] private GameObject PauseMenuUI = default;
    [SerializeField] private GameObject BlackBoardUI = default;
    [SerializeField] private LockedDoor_UI LockedDoorUI = default;
    [SerializeField] private GameObject HudUI = default;
    [SerializeField] private Notification Notification = default;
    [SerializeField] private WarningScreen WarningUI = default;

    public bool pauseMenu_acitveSelf { get => PauseMenuUI.activeSelf; }
    public bool BlackBoardUI_acitveSelf { get => BlackBoardUI.activeSelf; }
    public bool LockedDoorUI_acitveSelf { get => LockedDoorUI.gameObject.activeSelf; }
    public bool HudUI_acitveSelf { get => HudUI.activeSelf; }
    public bool Notification_acitveSelf { get => Notification.gameObject.activeSelf; }
    public bool WarningUI_acitveSelf { get => WarningUI.gameObject.activeSelf; }

    [Header("Listening To")]
    [SerializeField] private BoolEventChannelSO PauseMenuUIChannel = default;
    [SerializeField] private BoolEventChannelSO BlackBoardUIChannel = default;
    [SerializeField] private LockedDoorUIEventChannel LockedDoorUIChannel = default;
    [SerializeField] private BoolEventChannelSO HudUIChannel = default;
    [SerializeField] private StringEventChannelSO NotificationUIChannel = default;
    [SerializeField] private StringEventChannelSO WarningUIChannel = default;

    private void OnEnable()
    {
        PauseMenuUIChannel.OnEventRaised += onPauseMenuRequested;
        BlackBoardUIChannel.OnEventRaised += onBlackBoardRequested;
        LockedDoorUIChannel.OnInteractPressed += onlockedDoorRequested;
        LockedDoorUIChannel.ScriptTransfer += lockedDoorScriptTransfer;
        HudUIChannel.OnEventRaised += onHudRequested;
        NotificationUIChannel.OnEventRaised += onNotificationRequested;
        WarningUIChannel.OnEventRaised += onWarningRequested;
    }

    private void OnDisable()
    {
        PauseMenuUIChannel.OnEventRaised -= onPauseMenuRequested;
        BlackBoardUIChannel.OnEventRaised -= onBlackBoardRequested;
        LockedDoorUIChannel.OnInteractPressed -= onlockedDoorRequested;
        LockedDoorUIChannel.ScriptTransfer -= lockedDoorScriptTransfer;
        HudUIChannel.OnEventRaised -= onHudRequested;
        NotificationUIChannel.OnEventRaised -= onNotificationRequested;
        WarningUIChannel.OnEventRaised -= onWarningRequested;
    }

    private void onPauseMenuRequested(bool value)
    {
        PauseMenuUI.SetActive(value);
    }

    private void onBlackBoardRequested(bool value)
    {
        BlackBoardUI.SetActive(value);
    }

    private void onlockedDoorRequested()
    {
        if (LockedDoorUI.gameObject.activeSelf)
        {
            LockedDoorUI.gameObject.SetActive(false);
        }
        else
        {
            LockedDoorUI.gameObject.SetActive(true);
        }
    }

    private void lockedDoorScriptTransfer(Door_Is_Locked door)
    {
        LockedDoorUI.RecieveScriptFromDoor(door);
    }

    private void onHudRequested(bool value)
    {
        HudUI.SetActive(value);
    }

    private void onNotificationRequested(string str, bool value)
    {
        Notification.gameObject.SetActive(value);
        Notification.SetText(str);
    }

    private void onWarningRequested(string str, bool value)
    {
        if (value)
        {
            WarningUI.gameObject.SetActive(true);
            WarningUI.SetText(str);
            StartCoroutine(Warn(WarningTime));
        }
        else
        {
            WarningUI.gameObject.SetActive(false);
        }
    }

    private IEnumerator Warn(float time)
    {
        yield return new WaitForSeconds(time);
        WarningUI.gameObject.SetActive(false);
    }
}
