using UnityEngine;
using TMPro; // Make sure you have TextMeshPro package imported

public class KnivesUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Shoot shootScript;
    [SerializeField] private TMP_Text knivesText; // TextMeshPro UI element

    private void Start()
    {
        if (shootScript == null)
        {
            Debug.LogError("Shoot script reference is missing in KnivesUI!");
            return;
        }

        if (knivesText == null)
        {
            Debug.LogError("TMP_Text reference is missing in KnivesUI!");
            return;
        }

        // Subscribe to knife destroyed event
        shootScript.OnKnifeDestroyedEvent += UpdateUI;

        // Initial display
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (shootScript != null)
            shootScript.OnKnifeDestroyedEvent -= UpdateUI;
    }

    public void UpdateUI()
    {
        int knivesLeft = shootScript.maxKnives - shootScript.knivesUsed;
        knivesText.text = $"Knives: {knivesLeft}";
    }
}
