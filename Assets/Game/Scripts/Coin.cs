using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;

    void Update() => transform.Rotate(0f, 180f * Time.deltaTime, 0f, Space.World);

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.AddScore(value);
        Destroy(gameObject);
    }
}
