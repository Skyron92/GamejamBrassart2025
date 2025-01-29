using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class CameraGroup : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public PlayersManager playersManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    //Update Liste plyer for caera
    public void UpdateListCameraPlayer()
    {
        CinemachineTargetGroup.Target[] groupTargets = new CinemachineTargetGroup.Target[playersManager.players.Count];

        for (int i = 0; i < playersManager.players.Count; i++)
        {
            targetGroup.Targets[i].Object = playersManager.players[i].transform;
            Debug.Log("Liste des cibles mise à jour dans CinemachineTargetGroup");
        }
    }
}
