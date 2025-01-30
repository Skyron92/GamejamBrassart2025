using System.Collections;
using UnityEngine;

public class SicknessObject : SpellObject
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField, Range(1, 100)] private float castSpeed = 10;

    [HideInInspector] public Vector3 target;
    [HideInInspector] public Transform origin;
    public override void Interact(GameObject enemy) {
    }

    public void Init() {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, target);
        //StartCoroutine(CastLine(target));
        Debug.Log($"Ronce spawn à {origin.position} et doit aller à {target}");
    }
  
    private IEnumerator CastLine(Vector3 target) {
        while (Vector3.Distance(lineRenderer.GetPosition(1), target) > .1f) {
            var dir = target - lineRenderer.GetPosition(0);
            var pos = lineRenderer.GetPosition(1);
            pos += dir * castSpeed * Time.fixedDeltaTime;
            pos.z = 0;
            lineRenderer.SetPosition(1, pos);
            transform.position = origin.position;
            yield return null;
        }
    }
}