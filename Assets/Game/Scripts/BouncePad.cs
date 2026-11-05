using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] float bounceSpeed = 9f;

    // One kick, once per touch, so OnTriggerEnter. VelocityChange ignores mass.
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        Rigidbody body = other.attachedRigidbody;
        Vector3 v = body.linearVelocity;
        body.linearVelocity = new Vector3(v.x, 0f, v.z);   // every bounce is the same height
        body.AddForce(Vector3.up * bounceSpeed, ForceMode.VelocityChange);
    }
}
