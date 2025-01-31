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
    public int PlayerCount => _playerCount;
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
        if(indexPlayerJoined >= 0 && indexPlayerJoined < players.Length) playerSlots[indexPlayerJoined].OnPlayerJoined(players[indexPlayerJoined]);
        var playerController = playerObject.GetComponent<PlayerController>();
        playerController.indexPlayer = indexPlayerJoined;
        playerController.OnDeviceRemoved += OnDeviceLost;
        playerController.onSpellChanged += caster => {
            if(_playerCount >= 4) return;
            int currentIndex = 0;
            for (int i = 0; i < spellManager.spells.Length; i++) {
                if (spellManager.spells[i] == caster.spell) {
                    currentIndex = i;
                    break;
                }
            }
            for (int i = currentIndex; i < spellManager.spells.Length; i++) {
                if (spellManager.spells[i].taken) {
                    if (i == spellManager.spells.Length - 1) {
                        i = -1;
                    }
                    continue;
                }
                spellManager.Select(currentIndex, false);
                spellManager.Select(i, true);
                caster.spell.taken = false;
                caster.spell = spellManager.spells[i];
                spellManager.spells[i].taken = true;
                var controller = caster.GetComponent<PlayerController>();
                playerSlots[controller.indexPlayer].OnPlayerJoined(players[controller.indexPlayer]);
                return;
            }
        };
        cameraGroup.UpdateListCameraPlayer();
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
            for (int j = 0; j < spellManager.spells.Length; j++) {
                var t = spellManager.spells[j];
                if (t.taken) continue;
                spellCaster.spell = t;
                t.taken = true;
                spellManager.spellSelector[j].Select(true);
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
                for (int j = 0; j < spellManager.spells.Length; j++) {
                    if (players[i].spell == spellManager.spells[j]) {
                        spellManager.spellSelector[j].Select(false);
                    }
                }
                players[i] = null;
                return i;
            }
        }
        return -1;
    }
}