using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // ゲーム本編へ
    public void GoToMainGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // リザルト（ゲームオーバー）画面へ
    public void GoToResult()
    {
        SceneManager.LoadScene("ResultScene");
    }

    // タイトル画面へ戻る
    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}