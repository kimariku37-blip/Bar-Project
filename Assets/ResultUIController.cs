using UnityEngine;
using TMPro;

// 結果画面（ResultPanel）のUI制御
public class ResultUIController : MonoBehaviour
{
    [Header("結果表示用のテキスト")]
    public TextMeshProUGUI drinkNameText;     // お酒の名前
    public TextMeshProUGUI descriptionText;   // 説明
    public TextMeshProUGUI triviaText;        // 豆知識
    public TextMeshProUGUI masterCommentText; // マスターの一言

    // 結果データを画面に反映するメソッド
    public void DisplayResult(string drinkName, string desc, string trivia, string comment)
    {
        if (drinkNameText != null) drinkNameText.text = drinkName;
        if (descriptionText != null) descriptionText.text = desc;
        if (triviaText != null) triviaText.text = trivia;
        if (masterCommentText != null) masterCommentText.text = comment;
    }
}