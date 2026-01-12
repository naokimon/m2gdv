using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string GameScene = "Game";

    public void PlayGameButton()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
