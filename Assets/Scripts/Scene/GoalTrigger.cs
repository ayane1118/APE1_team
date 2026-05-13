using UnityEngine;

/// <summary>
/// 出口に配置する。Is Trigger ON のコライダーが必要。
/// プレイヤーが触れたらクリアシーンへ遷移する。
/// </summary>
public class GoalTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneLoader.LoadClear();
    }
}
