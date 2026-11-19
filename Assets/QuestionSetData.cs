using UnityEngine;

// 選択肢1つ分。テキストと、選んだ時に加算されるタイプをセットで持つ
[System.Serializable]
public class Choice
{
    public string choiceText;   // 例: 元気いっぱい
    public DrinkType tag;       // 例: Refreshing

    [TextArea(1, 3)]
    public string masterReaction; // この選択肢を選んだ直後にマスターが返す一言
}

// 質問1問分。4択を持つ
[System.Serializable]
public class Question
{
    [TextArea(1, 2)]
    public string questionText; // 例: 今日の気分は？
    public Choice[] choices;    // 4つ想定（4以外でも動くが基本4択）
}

// 質問セット全体（7問）を持つScriptableObject
// Project上で 右クリック→Create→BarGame→QuestionSet で作成
[CreateAssetMenu(fileName = "QuestionSet", menuName = "BarGame/QuestionSet")]
public class QuestionSetData : ScriptableObject
{
    public Question[] questions;
}
