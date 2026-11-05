using UnityEngine;

public class GoalZone : MonoBehaviour
{
    [SerializeField] GameObject openVisual, lockedVisual;

    public void SetOpen(bool open)
    {
        openVisual.SetActive(open);
        lockedVisual.SetActive(!open);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) GameManager.Instance.ReachedGoal();
    }
}
