using UnityEngine;

public class Peggle : MonoBehaviour
{
    public Sprite Sad_Guy;
    public GameObject HitEffectPrefab;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

 private void OnTriggerEnter2D(Collider2D collision)
{

    // Register combo hit
    ScoreManager.Instance.RegisterHit("Peggle", 2);

    // Spawn hit-effect animation
    if (HitEffectPrefab != null)
    {
        Instantiate(HitEffectPrefab, transform.position, Quaternion.identity);
    }

    // Switch to sad sprite
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    if (sr != null && Sad_Guy != null)
    {
        sr.sprite = Sad_Guy;
    }

    // Destroy this peg once the ball is gone
    StartCoroutine(WaitForObjectToBeDestroyed(collision.gameObject));

    audioManager.PlaySFX(audioManager.knife);
}


    // Wait until the colliding object (ball) is destroyed before destroying the peg
    private System.Collections.IEnumerator WaitForObjectToBeDestroyed(GameObject obj)
    {
        while (obj != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
