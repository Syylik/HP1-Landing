using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skin")]
public class Skin : ScriptableObject
{
    public Sprite sprite;
    public int price;
    public bool isBought;
    public bool isSelected;
}
