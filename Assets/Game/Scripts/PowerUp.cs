using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float multiplier = 1.6f;
    [SerializeField] float duration = 5f;

    void Update() => transform.Rotate(0f, 120f * Time.deltaTime, 0f, Space.World);

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<PlayerController>().BoostSpeed(multiplier, duration);
        GameManager.Instance.UpdateUI();
        Destroy(gameObject);
    }
}
