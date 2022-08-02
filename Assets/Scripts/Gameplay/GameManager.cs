using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int coins;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private GameObject[] allSkins;

    [SerializeField] private GameObject losePanel;
    public static GameManager instance { get; private set; }
    private void Awake() => instance = this;
    private void Start()
    {
        foreach(var go in allSkins) go.SetActive(false);
        allSkins[PlayerPrefs.GetInt(Utils.selectedSkinSave)].SetActive(true);
    }
    public void AddCoin()
    {
        coins++;
        coinsText.text = coins.ToString();
    }
    public void Lose()
    {
        int haveCoins = PlayerPrefs.GetInt(Utils.coinsSave);
        PlayerPrefs.SetInt(Utils.coinsSave, haveCoins + coins);
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneFade.ChangeScene(0);
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneFade.ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }
}
