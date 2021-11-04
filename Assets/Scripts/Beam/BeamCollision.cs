using UnityEngine;

public class BeamCollision : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
