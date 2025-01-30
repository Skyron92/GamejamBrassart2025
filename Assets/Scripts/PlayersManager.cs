using System;
using System.Collections.Generic;
using TMPro;
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
    public SpellCaster[] players =  new SpellCaster[4];
    [SerializeField] private PlayerSlot[] playerSlots = new PlayerSlot[4];
    [SerializeField] private SpellManager spellManager;
    
    public void OnPlayerJoined(PlayerInput playerInput) {
        _playerCount++;
        var playerObject = playerInput.gameObject;
        var playerSpellCaster = playerObject.GetComponent<SpellCaster>();
        Debug.Log($"Player {_playerCount} joined using {playerInput.devices[0].displayName}");
        _playersByDevice.Add(playerInput.devices[0], playerObject);
        int indexPlayerJoined = -2;
        if(playerSpellCaster != null) indexPlayerJoined = AssignPlayer(playerSpellCaster);
        if(indexPlayerJoined >= 0 && indexPlayerJoined < players.Length) playerSlots[indexPlayerJoined].OnPlayerJoined(players[indexPlayerJoined]);;
        cameraGroup?.UpdateListCameraPlayer();
        playerObject.GetComponent<PlayerController>().OnDeviceRemoved += OnDeviceLost;
    }

    void OnDeviceLost(object sender, PlayerInput playerInput) {
        Debug.Log($"Player {_playerCount} left using {playerInput.devices[0].displayName}");
        _playerCount--;
        var device = playerInput.devices[0];
        var playerObject = _playersByDevice[device];
        var playerSpellCaster = playerObject.GetComponent<SpellCaster>();
        _playersByDevice.Remove(device); 
        int indexPlayerJoined = -2;
        if(playerSpellCaster != null) indexPlayerJoined = RemovePlayer(playerSpellCaster);
        playerSlots[indexPlayerJoined].OnPlayerDisconnected();
        cameraGroup?.UpdateListCameraPlayer();
        Destroy(playerObject);
    }

    private int AssignPlayer(SpellCaster spellCaster) {
        for (int i = 0; i < 4; i++) {
            if (players[i] != null) continue;
            spellCaster.gameObject.name = "Player " + _playerCount;
            foreach (var t in spellManager.spells) {
                if (t.taken) continue;
                spellCaster.spell = t;
                t.taken = true;
                break;
            }
            players[i] = spellCaster;
            return i;
        }
        return -1;
    }
    private int RemovePlayer(SpellCaster spellCaster) {
        for (int i = 0; i < players.Length; i++) {
            if (players[i] == null) continue;
            if (players[i].gameObject.name == spellCaster.gameObject.name) {
                players[i] = null;
                return i;
            }
        }
        return -1;
    }
}