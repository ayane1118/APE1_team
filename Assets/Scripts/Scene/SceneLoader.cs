using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移の窓口。シーン名は SceneNames に集約。
/// </summary>
public static class SceneLoader
{
    public static void LoadTitle()    => SceneManager.LoadScene(SceneNames.Title);
    public static void LoadGame()     => SceneManager.LoadScene(SceneNames.Game);
    public static void LoadGameOver() => SceneManager.LoadScene(SceneNames.GameOver);
    public static void LoadClear()    => SceneManager.LoadScene(SceneNames.Clear);
}
