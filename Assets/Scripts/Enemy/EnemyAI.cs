using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の状態機械。Patrol（巡回）→ Chase（追跡）→ Patrol の遷移を管理する。
/// NavMeshAgent と EnemyVision が同一 GameObject に必要。
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyVision))]
public class EnemyAI : MonoBehaviour
{
    // 状態
    enum State { Patrol, Chase }

    [Header("巡回設定")]
    [SerializeField] Transform[] patrolPoints;    // Inspector で巡回ポイントを登録
    [SerializeField] float waypointStopDistance = 0.5f;

    [Header("追跡設定")]
    [SerializeField] float chaseSpeed  = 4f;
    [SerializeField] float patrolSpeed = 2f;

    NavMeshAgent agent;
    EnemyVision  vision;

    State  currentState = State.Patrol;
    int    patrolIndex  = 0;

    void Awake()
    {
        agent  = GetComponent<NavMeshAgent>();
        vision = GetComponent<EnemyVision>();
    }

    void Start()
    {
        GoToNextWaypoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol: UpdatePatrol(); break;
            case State.Chase:  UpdateChase();  break;
        }
    }

    // ───── 巡回 ─────

    void UpdatePatrol()
    {
        agent.speed = patrolSpeed;

        if (vision.CanSeePlayer())
        {
            currentState = State.Chase;
            return;
        }

        // 目的地に近づいたら次のポイントへ
        if (!agent.pathPending && agent.remainingDistance < waypointStopDistance)
            GoToNextWaypoint();
    }

    void GoToNextWaypoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[patrolIndex].position;
        patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
    }

    // ───── 追跡 ─────

    void UpdateChase()
    {
        agent.speed = chaseSpeed;

        if (vision.PlayerTransform != null)
            agent.destination = vision.PlayerTransform.position;

        // 視野から外れたら巡回に戻る
        if (!vision.CanSeePlayer())
        {
            currentState = State.Patrol;
            GoToNextWaypoint();
        }
    }
}
