using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用（Package ManagerでTMP Essentialsのインポートが必要）

// QuizPanel配下のUIを制御する。
// 「質問＋選択肢」と「マスターの反応＋次へボタン」の2グループを切り替えながら進行する。
public class QuizUIController : MonoBehaviour
{
    public QuizManager quizManager;

    [Header("質問グループ（質問文＋選択肢ボタン）")]
    public GameObject questionGroup;
    public TMP_Text questionText;
    public Button[] choiceButtons;        // 4つ
    public TMP_Text[] choiceButtonLabels; // 各ボタンの子にあるテキスト、4つ

    [Header("マスターの反応グループ（一言＋次へボタン）")]
    public GameObject reactionGroup;
    public TMP_Text reactionText;
    public Button nextButton;

    void Start()
    {
        nextButton.onClick.AddListener(OnClickNext);
    }

    // 現在の質問をUIに反映する（質問グループを表示、反応グループを隠す）
    public void ShowCurrentQuestion()
    {
        Question q = quizManager.GetCurrentQuestion();
        if (q == null) return;

        reactionGroup.SetActive(false);
        questionGroup.SetActive(true);

        questionText.text = q.questionText;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < q.choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtonLabels[i].text = q.choices[i].choiceText;

                int capturedIndex = i; // クロージャ対策
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(capturedIndex));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    // 選択肢が押された直後：まだ次の質問には進まず、マスターの一言を表示する
    private void OnChoiceSelected(int index)
    {
        Question q = quizManager.GetCurrentQuestion();
        string reaction = q.choices[index].masterReaction;

        quizManager.SelectChoice(index); // 内部の集計はここで進めておく

        questionGroup.SetActive(false);
        reactionGroup.SetActive(true);
        reactionText.text = reaction;
    }

    // 「次へ」ボタンが押された時：次の質問へ、または結果画面へ
    private void OnClickNext()
    {
        if (quizManager.HasNextQuestion())
        {
            ShowCurrentQuestion();
        }
        else
        {
            GameFlowManager.Instance.ShowResult();
        }
    }
}
