using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (ScoreManager.Instance == null) return;

        scoreText.text =
            $"Score: {ScoreManager.Instance.score}\n" +
            $"Combo: x{ScoreManager.Instance.ComboMultiplier}";
    }
}
