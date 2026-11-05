using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveForce = 10f;
    Rigidbody rb;
    Vector3 input;
    float speedMultiplier = 1f;
    float boostEnds;
    Coroutine boost;
    InputAction move;
    float noControlUntil;

    public float SpeedMultiplier => speedMultiplier;
    public float BoostRemaining => boost == null ? 0f : Mathf.Max(0f, boostEnds - Time.time);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0f; // OnTriggerStay stops firing if the rigidbody sleeps
        move = InputSystem.actions.FindAction("Player/Move", true);
        move.Enable();
    }

    void Update()
    {
        Vector2 m = move.ReadValue<Vector2>();
        input = Vector3.ClampMagnitude(new Vector3(m.x, 0f, m.y), 1f);
    }

    void FixedUpdate()
    {
        if (Time.time >= noControlUntil) rb.AddForce(input * moveForce * speedMultiplier);
    }

    public void Knockback(Vector3 push, float seconds)
    {
        Vector3 v = rb.linearVelocity;
        rb.linearVelocity = new Vector3(0f, v.y, 0f);
        rb.AddForce(push, ForceMode.Impulse);
        noControlUntil = Time.time + seconds;
    }

    public void BoostSpeed(float multiplier, float seconds)
    {
        if (boost != null) StopCoroutine(boost);
        boost = StartCoroutine(Boost(multiplier, seconds));
    }

    IEnumerator Boost(float multiplier, float seconds)
    {
        speedMultiplier = multiplier;
        boostEnds = Time.time + seconds;
        yield return new WaitForSeconds(seconds);
        speedMultiplier = 1f;
        boost = null;
    }
}
