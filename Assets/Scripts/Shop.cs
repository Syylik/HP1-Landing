using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private Image currentSkinImage;
    [SerializeField] private Skin[] allSkins;
    private Skin curSkin;
    private int currentSkinNum;

    [SerializeField] private Button recentSkinButt, nextSkinButt;
    [SerializeField] private GameObject buyButt, selectButt, selectedText;
    [SerializeField] private TMP_Text skinPrice;
    private void Start()
    {
        currentSkinNum = PlayerPrefs.GetInt(Utils.selectedSkinSave);
        currentSkinImage.sprite = allSkins[currentSkinNum].sprite;
        ButtonsActive();
        CheckSkinState();
    }
    public void RecentSkin()
    {
        currentSkinNum--;
        ButtonsActive();
        CheckSkinState();
    }
    public void NextSkin()
    {
        currentSkinNum++;
        ButtonsActive();
        CheckSkinState();
    }
    public void Buy()
    {
        if(PlayerPrefs.GetInt(Utils.coinsSave) >= curSkin.price)
        {
            var coinsAfterBuy = PlayerPrefs.GetInt(Utils.coinsSave) - curSkin.price;
            PlayerPrefs.SetInt(Utils.coinsSave, coinsAfterBuy);
            curSkin.isBought = true;
        }
        CheckSkinState();
    }
    public void Select()
    {
        if(curSkin.isBought)
        {
            curSkin.isSelected = true;
            currentSkinImage.sprite = curSkin.sprite;
        }
        CheckSkinState();
    }
    private void CheckSkinState() //Проверяет куплен и выбран ли скин 
    {
        curSkin = allSkins[currentSkinNum];
        skinPrice.text = curSkin.price.ToString();
        if(!curSkin.isBought)
        {
            buyButt.SetActive(true);
            selectButt.SetActive(false);
            selectedText.SetActive(false);
        }
        else if(curSkin.isBought && !curSkin.isSelected)
        {
            buyButt.SetActive(false);
            selectButt.SetActive(true);
            selectedText.SetActive(false);
        }
        else if(curSkin.isBought && curSkin.isSelected)
        {
            buyButt.SetActive(false);
            selectButt.SetActive(false);
            selectedText.SetActive(true);
        }
    }    
    private void ButtonsActive() //Скрывает кнопки, когда дошёл до начала/конца
    {
        PlayerPrefs.SetInt(Utils.selectedSkinSave, currentSkinNum);
        if(currentSkinNum <= 0)
        {
            currentSkinNum = 0;
            recentSkinButt.interactable = false;
        }
        else recentSkinButt.interactable = true;

        if(currentSkinNum >= allSkins.Length)
        {
            currentSkinNum = allSkins.Length;
            nextSkinButt.interactable = false;
        }
        else nextSkinButt.interactable = true;
    }
}
