using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] GameObject Enemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enemy.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
