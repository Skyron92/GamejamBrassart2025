using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages all the players devices
/// </summary>
public class PlayersManager : MonoBehaviour {

    // Script of the group camera
    public CameraGroup cameraGroup;
    
    private int _playerCount;
    private readonly Dictionary<InputDevice, GameObject> _playersByDevice = new Dictionary<InputDevice, GameObject>();
    [HideInInspector] public List<GameObject> players = new List<GameObject>(); 

    public void OnPlayerJoined(PlayerInput playerInput) {
        _playerCount++;
        var playerObject = playerInput.gameObject;
        playerObject.name = "Player " + _playerCount;
        Debug.Log($"Player {_playerCount} joined using {playerInput.devices[0].displayName}");
        _playersByDevice.Add(playerInput.devices[0], playerObject);
        if(playerObject != null && !players.Contains(playerObject)) players.Add(playerObject);
        cameraGroup?.UpdateListCameraPlayer();
        playerObject.GetComponent<PlayerController>().OnDeviceRemoved += OnDeviceLost;
    }

    void OnDeviceLost(object sender, PlayerInput playerInput) {
        Debug.Log($"Player {_playerCount} left using {playerInput.devices[0].displayName}");
        _playerCount--;
        var device = playerInput.devices[0];
        var playerObject = _playersByDevice[device];
        _playersByDevice.Remove(device); 
        if(playerObject != null && players.Contains(playerObject)) players.Remove(playerObject);
        cameraGroup?.UpdateListCameraPlayer();
        Destroy(playerObject);
        

    }
}