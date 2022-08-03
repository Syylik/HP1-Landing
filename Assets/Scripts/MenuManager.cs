using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;
    public void PlayButtonSound()
    {
        buttonSound.pitch = Random.Range(0.9f, 1.1f);
        buttonSound.Play();
    }
    public void Play() => SceneFade.ChangeScene(SceneManager.GetActiveScene().buildIndex +1);
    public void Exit() => Application.Quit();
}
