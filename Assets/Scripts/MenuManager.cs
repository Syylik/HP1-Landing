using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Play() => SceneFade.ChangeScene(SceneManager.GetActiveScene().buildIndex +1);
    public void Exit() => Application.Quit();
}
