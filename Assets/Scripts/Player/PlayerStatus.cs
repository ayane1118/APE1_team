using UnityEngine;

/// <summary>
/// プレイヤーのライフ管理。
/// ダメージは TakeDamage() を呼ぶ。ライフが0になったらゲームオーバーシーンへ遷移。
/// タイマーは後から追加予定（このクラスに追記する）。
/// </summary>
public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    [SerializeField] int maxLife = 5;
    public int CurrentLife { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        CurrentLife = maxLife;
    }

    public void TakeDamage(int amount = 1)
    {
        CurrentLife -= amount;
        CurrentLife = Mathf.Max(CurrentLife, 0);

        if (CurrentLife <= 0)
            SceneLoader.LoadGameOver();
    }
}
