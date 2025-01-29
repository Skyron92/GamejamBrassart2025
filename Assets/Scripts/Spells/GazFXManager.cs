using UnityEngine;

public class GazFXManager : MonoBehaviour
{
    void OnParticleSystemStopped() {
        Destroy(transform.parent.gameObject);
    }
}
