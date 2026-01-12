using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Conditions : MonoBehaviour
{
    [Header("Game Rules")]
    public string enemyTag = "Enemy";
    public string sceneToLoadWin = "WinScene";
    public string sceneToLoadLose = "LoseScene";

    [Header("References")]
    public Shoot shootScript;

    private bool _gameEnded = false;

    private void Awake()
    {
        if (shootScript == null)
        {
            shootScript = FindObjectOfType<Shoot>();
        }
    }

    private void Start()
    {
        if (shootScript != null)
        {
            shootScript.OnKnifeDestroyedEvent += OnKnifeDestroyed;
        }
    }

    private void OnDestroy()
    {
        if (shootScript != null)
        {
            shootScript.OnKnifeDestroyedEvent -= OnKnifeDestroyed;
        }
    }

    private void OnKnifeDestroyed()
    {
        // Wait ONE frame so Unity finishes destroying enemies
        StartCoroutine(CheckConditionsNextFrame());
    }

    private IEnumerator CheckConditionsNextFrame()
    {
        yield return null; // 👈 critical

        if (_gameEnded) yield break;

        int enemiesLeft = GameObject.FindGameObjectsWithTag(enemyTag).Length;

        // ✅ WIN ABSOLUTE PRIORITY
        if (enemiesLeft == 0)
        {
            _gameEnded = true;
            SceneManager.LoadScene(sceneToLoadWin);
            yield break;
        }

        // ❌ LOSS ONLY IF ENEMIES STILL EXIST
        if (!shootScript.HasKnivesLeft &&
            !shootScript.HasActiveKnife &&
            enemiesLeft > 0)
        {
            _gameEnded = true;
            SceneManager.LoadScene(sceneToLoadLose);
        }
    }
}
