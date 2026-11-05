using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    [SerializeField] Renderer body;
    [SerializeField] Material normalMaterial, downMaterial;
    Health health;

    void Awake() => health = GetComponent<Health>();

    void Update()
    {
        body.sharedMaterial = health.IsDown ? downMaterial : normalMaterial;
        body.enabled = health.IsDown || !health.IsInvulnerable || Mathf.Repeat(Time.time, 0.2f) < 0.1f;
    }
}
