using UnityEngine;
using UnityEngine.SceneManagement;

// TitleSceneのTitlePanelにアタッチ。
// Startボタンの OnClick() から OnClickStart() を呼ぶ。
public class TitleScreenController : MonoBehaviour
{
    // バーのシーン名。シーンファイルの名前と完全に同じにすること
    public string barSceneName = "BarScene";

    public void OnClickStart()
    {
        SceneManager.LoadScene(barSceneName);
    }
}