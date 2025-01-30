using UnityEngine;
using Unity.Cinemachine;

public class CameraGroup : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public PlayersManager playersManager;

    //Update Liste plyer for caera
    public void UpdateListCameraPlayer() {
        CinemachineTargetGroup.Target[] groupTargets = new CinemachineTargetGroup.Target[playersManager.players.Length];
        for (int i = 0; i < playersManager.PlayerCount; i++) {
            targetGroup.Targets[i].Object = playersManager.players[i].transform;
        }
    }
}
