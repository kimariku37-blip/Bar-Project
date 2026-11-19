using UnityEngine;

// 結果画面末尾に表示する、マスターのランダムな一言集
// Project上で 右クリック→Create→BarGame→EndingCommentSet で作成
[CreateAssetMenu(fileName = "EndingComments", menuName = "BarGame/EndingCommentSet")]
public class EndingCommentData : ScriptableObject
{
    [TextArea(1, 3)]
    public string[] comments;

    public string GetRandomComment()
    {
        if (comments == null || comments.Length == 0) return "";
        return comments[Random.Range(0, comments.Length)];
    }
}
