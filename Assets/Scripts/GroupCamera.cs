using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class TargetGroupUpdater : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public List<Transform> targets = new List<Transform>();
    public float defaultWeight = 1f;
    public float defaultRadius = 1f;

    void Start()
    {
        UpdateTargetGroup();
    }

    public void UpdateTargetGroup()
    {
        if (targetGroup == null) return;

        // Création d'un tableau de cibles pour Cinemachine
        CinemachineTargetGroup.Target[] groupTargets = new CinemachineTargetGroup.Target[targets.Count];

        for (int i = 0; i < targets.Count; i++)
        {
            groupTargets[i].target = targets[i];
            groupTargets[i].weight = defaultWeight;
            groupTargets[i].radius = defaultRadius;
        }

        // Mise à jour des cibles dans le CinemachineTargetGroup
        targetGroup.m_Targets = groupTargets;
    }

    public void AddTarget(Transform newTarget)
    {
        if (!targets.Contains(newTarget))
        {
            targets.Add(newTarget);
            UpdateTargetGroup();
        }
    }

    public void RemoveTarget(Transform targetToRemove)
    {
        if (targets.Contains(targetToRemove))
        {
            targets.Remove(targetToRemove);
            UpdateTargetGroup();
        }
    }
}