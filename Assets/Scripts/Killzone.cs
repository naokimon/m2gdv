using UnityEngine;

public class Killzone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        KnifeLife knife = other.GetComponent<KnifeLife>();
        if (knife != null)
        {
            knife.Kill(); // notify Shoot that knife is gone
        }
    }

}
