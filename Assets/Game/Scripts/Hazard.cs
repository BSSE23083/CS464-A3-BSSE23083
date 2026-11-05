using System.Collections;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float knockback = 6f, knockTime = 0.25f;
    [SerializeField] float restTime = 0f, warnTime = 0.7f, strikeTime = 0.4f;
    [SerializeField] float startDelay = 0f;
    [SerializeField] GameObject warning;
    [SerializeField] GameObject strikeVisual;

    IEnumerator Start()
    {
        if (restTime <= 0f) yield break;
        Collider zone = GetComponent<Collider>();
        zone.enabled = false;
        warning.SetActive(false);
        if (strikeVisual != null) strikeVisual.SetActive(false);
        if (startDelay > 0f) yield return new WaitForSeconds(startDelay);
        while (true)
        {
            zone.enabled = false;
            if (strikeVisual != null) strikeVisual.SetActive(false);
            yield return new WaitForSeconds(restTime);
            warning.SetActive(true);
            yield return new WaitForSeconds(warnTime);
            warning.SetActive(false);
            zone.enabled = true;
            if (strikeVisual != null) strikeVisual.SetActive(true);
            yield return new WaitForSeconds(strikeTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out Health health) || !health.TakeDamage(damage)) return;
        Rigidbody body = other.attachedRigidbody;
        if (body == null) return;
        Vector3 away = body.position - transform.position;
        away.y = 0f;
        Vector3 push = away.normalized * knockback;
        if (other.TryGetComponent(out PlayerController p)) p.Knockback(push, knockTime);
    }
}
