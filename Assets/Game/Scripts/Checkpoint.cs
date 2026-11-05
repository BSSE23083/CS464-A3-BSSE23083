using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject reachedVisual;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.SetCheckpoint(transform);
        reachedVisual.SetActive(true);
    }
}
