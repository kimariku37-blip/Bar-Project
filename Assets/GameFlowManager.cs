using UnityEngine;

// バーシーン内での進行管理
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance; // 他のスクリプトから呼び出せるようにする

    [Header("表示切り替え用パネル")]
    public GameObject greetingPanel; // マスターの挨拶パネル
    public GameObject questionPanel; // 質問パネル
    public GameObject resultPanel;   // 結果パネル

    private GameState currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 最初は挨拶画面からスタート
        SetState(GameState.Greeting);
    }

    public void SetState(GameState newState)
    {
        currentState = newState;

        // すべてのパネルを一旦非表示にしてから、必要なものだけ表示
        if (greetingPanel != null) greetingPanel.SetActive(false);
        if (questionPanel != null) questionPanel.SetActive(false);
        if (resultPanel != null) resultPanel.SetActive(false);

        switch (currentState)
        {
            case GameState.Greeting:
                if (greetingPanel != null) greetingPanel.SetActive(true);
                break;
            case GameState.Playing:
                if (questionPanel != null) questionPanel.SetActive(true);
                break;
            case GameState.Result:
                if (resultPanel != null) resultPanel.SetActive(true);
                break;
        }
    }

    // 挨拶パネルのボタンから呼ばれるメソッド
    public void OnClickGreetingNext()
    {
        SetState(GameState.Playing);
    }

    // 結果を表示するメソッド（引数なしでもエラーにならないように設定）
    public void ShowResult(string drinkName = "", string desc = "", string trivia = "", string comment = "")
    {
        SetState(GameState.Result);

        if (resultPanel != null)
        {
            ResultUIController resultUI = resultPanel.GetComponent<ResultUIController>();
            if (resultUI != null)
            {
                resultUI.DisplayResult(drinkName, desc, trivia, comment);
            }
        }
    }
}