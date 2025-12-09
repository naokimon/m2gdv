using UnityEngine;

public class Peggle : MonoBehaviour
{
    public Sprite Sad_Guy;
    public GameObject HitEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreManager.Instance.AddScore(2);

        // Spawn the hit-effect animation
        if (HitEffectPrefab != null)
        {
            Instantiate(HitEffectPrefab, transform.position, Quaternion.identity);
        }

        // Change sprite
        GetComponent<SpriteRenderer>().sprite = Sad_Guy;

        StartCoroutine(WaitForObjectToBeDestroyed(collision.gameObject));
    }

    private System.Collections.IEnumerator WaitForObjectToBeDestroyed(GameObject obj)
    {
        while (obj != null)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
