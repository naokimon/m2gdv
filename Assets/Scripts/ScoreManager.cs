using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Score")]
    public int score = 0;

    [Header("Knife Tracking (assign when spawned)")]
    public GameObject currentKnife;

    [Header("Combo Settings")]
    [SerializeField] private float comboResetTime = 5f;

    private int scoreMultiplier = 1;
    public int ComboMultiplier => scoreMultiplier;

    private float comboTimer = 0f;
    private string lastHitTag = "";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        // Reset combo if knife is destroyed
        if (currentKnife == null && scoreMultiplier > 1)
        {
            ResetCombo();
            return;
        }

        // Timer-based reset
        if (scoreMultiplier > 1)
        {
            comboTimer += Time.deltaTime;

            if (comboTimer >= comboResetTime)
            {
                ResetCombo();
            }
        }
    }

    public void RegisterHit(string hitTag, int baseValue)
    {
        comboTimer = 0f;

        // Same tag → build combo
        if (hitTag == lastHitTag)
        {
            scoreMultiplier++;
        }
        else
        {
            scoreMultiplier = 1;
            lastHitTag = hitTag;
        }

        AddScore(baseValue * scoreMultiplier);

        Debug.Log($"Score: {score} | Combo: x{scoreMultiplier}");
    }

    private void ResetCombo()
    {
        scoreMultiplier = 1;
        lastHitTag = "";
        comboTimer = 0f;

        Debug.Log("Combo reset");
    }

    public void AddScore(int amount)
    {
        score += amount;
    }
}
