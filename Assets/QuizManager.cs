using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// クイズの進行・集計・お酒抽選を担当するメインクラス。
// UIとは切り離してあるので、UI側はこのクラスのpublicメソッドを呼ぶだけでいい。
public class QuizManager : MonoBehaviour
{
    [Header("データ参照")]
    public QuestionSetData questionSet;      // 7問分の質問データ
    public List<DrinkData> allDrinks;        // 24種類すべてのお酒データ
    public EndingCommentData endingComments; // ランダムな一言集

    private int currentQuestionIndex = 0;
    private Dictionary<DrinkType, int> tally = new Dictionary<DrinkType, int>();

    // 結果が決まった後にUI側が使えるように保持しておく
    public DrinkData ResultDrink { get; private set; }
    public string ResultEndingComment { get; private set; }

    void Awake()
    {
        ResetQuiz();
    }

    // 最初の質問からやり直したい時に呼ぶ
    public void ResetQuiz()
    {
        currentQuestionIndex = 0;
        tally.Clear();
        foreach (DrinkType type in System.Enum.GetValues(typeof(DrinkType)))
        {
            tally[type] = 0;
        }
        ResultDrink = null;
        ResultEndingComment = "";
    }

    public bool HasNextQuestion()
    {
        return currentQuestionIndex < questionSet.questions.Length;
    }

    public Question GetCurrentQuestion()
    {
        if (!HasNextQuestion()) return null;
        return questionSet.questions[currentQuestionIndex];
    }

    // UI側が選択肢のボタンを押した時に呼ぶ
    // choiceIndex は 0〜3（4択想定）
    public void SelectChoice(int choiceIndex)
    {
        Question q = GetCurrentQuestion();
        if (q == null) return;
        if (choiceIndex < 0 || choiceIndex >= q.choices.Length) return;

        DrinkType tag = q.choices[choiceIndex].tag;
        tally[tag]++;

        currentQuestionIndex++;

        // 全問終わったら結果を確定する
        if (!HasNextQuestion())
        {
            DecideResult();
        }
    }

    // 集計結果から最終的なお酒を1つ決める
    private void DecideResult()
    {
        // 一番票が多いタイプを探す（同数の場合は複数残る）
        int maxCount = tally.Values.Max();
        List<DrinkType> topTypes = tally
            .Where(pair => pair.Value == maxCount)
            .Select(pair => pair.Key)
            .ToList();

        // 同率タイプが複数あれば、その中からランダムで1タイプ選ぶ
        DrinkType winningType = topTypes[Random.Range(0, topTypes.Count)];

        // そのタイプに属するお酒（4種類想定）からランダムで1つ選ぶ
        List<DrinkData> candidates = allDrinks
            .Where(drink => drink.type == winningType)
            .ToList();

        if (candidates.Count > 0)
        {
            ResultDrink = candidates[Random.Range(0, candidates.Count)];
        }

        ResultEndingComment = endingComments.GetRandomComment();
    }
}
