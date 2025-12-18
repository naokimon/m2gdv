using System;
using UnityEngine;

public class RandomItems : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Press Space to print one random item.");
        Debug.Log("Press Backspace to print all items.");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))PrintRandomItem();
        if(Input.GetKeyDown(KeyCode.Backspace))PrintAllItems();
    }

    [SerializeField] private string[] randomItems = new string[9];

    private void PrintRandomItem()
    {
        int index = UnityEngine.Random.Range(0, randomItems.Length);
        Debug.Log(randomItems[index]);
    }

    private void PrintAllItems()
    {
        for (int i = 0; i < randomItems.Length; i++)
        {
            Debug.Log(randomItems[i]);
        }
    }
}
