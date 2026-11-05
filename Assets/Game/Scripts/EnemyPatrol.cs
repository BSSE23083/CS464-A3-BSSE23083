using UnityEngine;

public enum PatrolMode { Loop, PingPong }

public class EnemyPatrol : MonoBehaviour
{
    public enum State { Patrol, Chase, Return }

    [SerializeField] Transform[] points;
    [SerializeField] PatrolMode mode = PatrolMode.Loop;
    [SerializeField] float patrolSpeed = 2f, chaseSpeed = 3.5f, turnSpeed = 360f;
    [SerializeField] float arriveDistance = 0.1f;
    [SerializeField] float chaseRange = 4f, giveUpRange = 6f;

    Transform player;
    int index, step = 1;

    public State CurrentState { get; private set; } = State.Patrol;

    void Start() => player = FindAnyObjectByType<PlayerController>().transform;

    void Update()
    {
        Vector3 waypoint = points[index].position;
        switch (CurrentState)
        {
            case State.Patrol:
                MoveTo(waypoint, patrolSpeed);
                if (Arrived(waypoint)) index = NextIndex();
                if (InRange(chaseRange)) CurrentState = State.Chase;
                break;
            case State.Chase:
                MoveTo(new Vector3(player.position.x, transform.position.y, player.position.z), chaseSpeed);
                if (!InRange(giveUpRange)) CurrentState = State.Return;
                break;
            case State.Return:
                MoveTo(waypoint, patrolSpeed);
                if (InRange(chaseRange)) CurrentState = State.Chase;
                else if (Arrived(waypoint)) CurrentState = State.Patrol;
                break;
        }
    }

    // arrival by distance, not ==
    bool Arrived(Vector3 target) => Vector3.Distance(transform.position, target) < arriveDistance;

    // seen inside one range, lost outside a longer one
    bool InRange(float range) => Vector3.Distance(transform.position, player.position) < range;

    void MoveTo(Vector3 target, float speed)
    {
        Vector3 toTarget = target - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (toTarget != Vector3.zero) transform.rotation = Quaternion.RotateTowards(
            transform.rotation, Quaternion.LookRotation(toTarget), turnSpeed * Time.deltaTime);
    }

    int NextIndex()
    {
        if (mode == PatrolMode.Loop) return (index + 1) % points.Length;
        if (index + step < 0 || index + step >= points.Length) step = -step;
        return index + step;
    }

    void OnDrawGizmosSelected()
    {
        if (points == null) return;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == null) continue;
            Gizmos.DrawWireSphere(points[i].position, 0.3f);
            if (i == points.Length - 1 && mode != PatrolMode.Loop) continue;
            Transform next = points[(i + 1) % points.Length];
            if (next != null) Gizmos.DrawLine(points[i].position, next.position);
        }
    }
}
