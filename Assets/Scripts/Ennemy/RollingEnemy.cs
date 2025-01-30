using System.Collections;
using UnityEngine;

public class RollingEnemy : EnemyTest {
    
    private float RotationSpeed => moveSpeed * 10 ;
    private float Acceleration => RotationSpeed * .1f;
    [SerializeField, Range(0, 100)] private float moveSpeed = 15f;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Transform armor;

    private void Start() {
        Move();
    }

    IEnumerator StartRotation() {
       float increment = Acceleration;
        while (Mathf.Abs(rigidbody.linearVelocity.magnitude) > .1f) {
            armor.Rotate(new Vector3(increment, 0f, 0f), Space.Self);
            if(increment < Acceleration) increment += Acceleration;
            yield return null;
        }
    }

    public void Move() {
        rigidbody.AddForce(-transform.right * moveSpeed, ForceMode.Impulse);
        StartCoroutine(StartRotation());
    }
}
