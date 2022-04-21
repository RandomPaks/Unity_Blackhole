using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnButtonPlay() => SceneManager.LoadScene("GameScene");

    public void OnButtonExit() => Application.Quit();
}
