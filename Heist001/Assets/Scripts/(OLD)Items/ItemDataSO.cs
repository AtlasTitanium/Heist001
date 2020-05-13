using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemDataSO", order = 1)]
public class ItemDataSO : ScriptableObject {
    public string itemName;

    [Range(1,100)]
    public int itemValue = 1;

    public Sprite icon;
}

[CreateAssetMenu(fileName = "ArtpieceData", menuName = "ScriptableObjects/ArtpieceDataSO", order = 1)]
public class ArtpieceData : ItemDataSO {
    public string authorName;
    public string artpieceTitle;
    public string date;
}
