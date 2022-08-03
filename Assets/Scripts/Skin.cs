using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skin")]
public class Skin : ScriptableObject
{
    public string skinName;
    public Sprite sprite;
    public int price;
    public bool isBought;
    public bool isSelected;

    //public bool isBoughtDefault;
    //public bool isSelectedDefault;

    public readonly string boughtSave = "isBought";
    public readonly string selectSave = "isSelected";
    private void Awake()
    {
        isBought = Utils.GetBool(boughtSave + skinName, isBought);
        isSelected = Utils.GetBool(selectSave + skinName, isSelected);
    }
}
