using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int max = 3;
    [SerializeField] float iFrames = 1f;
    int current;
    float invulnerableUntil;

    public int Current => current;
    public int Max => max;
    public bool IsDown => current <= 0;
    public bool IsInvulnerable => Time.time < invulnerableUntil;

    void Awake() => current = max;

    public bool TakeDamage(int amount)
    {
        if (current <= 0 || Time.time < invulnerableUntil) return false;
        invulnerableUntil = Time.time + iFrames;
        current -= amount;
        GameManager.Instance.UpdateUI();
        if (current <= 0) GameManager.Instance.PlayerDied();   // <= 0, not == 0: the crusher hits for 2
        return true;
    }

    public void KnockOut() => current = 0;

    public void Restore(float protection)
    {
        current = max;
        invulnerableUntil = Time.time + protection;
        GameManager.Instance.UpdateUI();
    }
}
