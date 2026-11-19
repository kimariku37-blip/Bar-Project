using UnityEngine;

// お酒1種類分のデータ。
// Project上で 右クリック→Create→BarGame→DrinkData で作成できる。
// 24種類分、この形式でアセットを作っていく想定。
[CreateAssetMenu(fileName = "NewDrink", menuName = "BarGame/DrinkData")]
public class DrinkData : ScriptableObject
{
    [Header("基本情報")]
    public string drinkName;          // 例: モヒート
    public DrinkType type;            // 属するタイプ
    public Sprite drinkImage;         // 提供シーンで表示する画像

    [Header("説明文")]
    [TextArea(2, 4)]
    public string description;        // 味の説明（例: ラム・ミント・ライムの爽快な一杯）

    [TextArea(2, 4)]
    public string trivia;             // 豆知識（度数や由来など、ここで初めて出す）

    [Header("マスターの一言")]
    [TextArea(2, 3)]
    public string masterComment;      // このお酒を選んだ理由をそれっぽく語るセリフ
}
