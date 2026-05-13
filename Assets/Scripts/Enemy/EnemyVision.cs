using UnityEngine;

/// <summary>
/// 扇形の視野でプレイヤーを検出する。
/// EnemyAI から毎フレーム CanSeePlayer() を呼んで使う。
/// </summary>
public class EnemyVision : MonoBehaviour
{
    [Header("視野設定")]
    [SerializeField] float viewRadius = 10f;      // 検出距離
    [SerializeField, Range(0f, 360f)] float viewAngle = 90f;  // 視野角（全体の角度）

    [Header("障害物レイヤー")]
    [SerializeField] LayerMask obstacleMask;

    Transform player;

    void Awake()
    {
        var playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    /// <summary>プレイヤーが視野内にいれば true を返す。</summary>
    public bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 toPlayer = player.position - transform.position;

        // 距離チェック
        if (toPlayer.magnitude > viewRadius) return false;

        // 角度チェック（視野角の半分と比較）
        if (Vector3.Angle(transform.forward, toPlayer) > viewAngle / 2f) return false;

        // 遮蔽チェック
        if (Physics.Raycast(transform.position, toPlayer.normalized, toPlayer.magnitude, obstacleMask))
            return false;

        return true;
    }

    /// <summary>プレイヤーの Transform を返す（追跡先として使う）。</summary>
    public Transform PlayerTransform => player;

    // エディタ上でギズモ表示（開発時デバッグ用）
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 leftBound  = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightBound = Quaternion.Euler(0,  viewAngle / 2f, 0) * transform.forward;
        Gizmos.DrawRay(transform.position, leftBound  * viewRadius);
        Gizmos.DrawRay(transform.position, rightBound * viewRadius);
    }
#endif
}
