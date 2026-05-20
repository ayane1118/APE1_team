using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // ゲーム本編へ
    public void GoToMainGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // クリア画面へ
    public void ClearResult()
    {
        SceneManager.LoadScene("ClearScene");
    }

    // ゲームオーバー画面へ
    public void GameOverResult()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    // タイトル画面へ戻る
    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}