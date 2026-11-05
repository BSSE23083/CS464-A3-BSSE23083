using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || GameManager.Instance.Respawning) return;
        other.GetComponent<Health>().KnockOut();
        GameManager.Instance.PlayerDied();
    }
}
