using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MovingDayPlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab = null;

    private class PlayerInputData
    {
        public InputDevice inputDevice;
        public GameObject playerGO;
        public bool keyboard = false;
    }
    private List<PlayerInputData> players = new List<PlayerInputData>();

    void Update()
    {
        CheckForNewPlayers();
    }

    private void CheckForNewPlayers()
    {
        for (int d = 0; d < InputManager.Devices.Count; d++)
        {
            InputDevice device = InputManager.Devices[d];
            if (device.GetControl(InputControlType.Action1) && !CheckContainsDevice(device))
            {
                CreatePlayer(device);
            }
            else if (Input.GetKey(KeyCode.Return) && !CheckContainsKeyboard())
            {
                CreatePlayer(null);
            }
        }
    }

    private bool CheckContainsDevice(InputDevice inputDevice)
    {
        foreach (PlayerInputData playerInputData in players)
        {
            if (playerInputData.inputDevice == inputDevice)
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckContainsKeyboard()
    {
        foreach (PlayerInputData playerInputData in players)
        {
            if (playerInputData.keyboard)
            {
                return true;
            }
        }

        return false;
    }

    private void CreatePlayer(InputDevice inputDevice)
    {
        PlayerInputData newPlayerInputData = new PlayerInputData();
        newPlayerInputData.inputDevice = inputDevice;
        newPlayerInputData.keyboard = inputDevice == null;
        newPlayerInputData.playerGO = Instantiate(playerPrefab);
        newPlayerInputData.playerGO.GetComponent<BrianPlayerMovement>().SetInputDevice(inputDevice);
        players.Add(newPlayerInputData);
    }
}
