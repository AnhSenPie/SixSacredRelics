using AnhSenPai.Inventory;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public static TreeController Instance { get; private set; }
    
    public GameObject itemPrefab;

    public int dropQuanity = 3;
    int currentDrop = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void DropItem()
    {
        Vector2 dropPos = transform.position;
        Instantiate(itemPrefab, dropPos, transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if( currentDrop < dropQuanity)
            {
                currentDrop++;
                DropItem();
            }
                 
        }
      
    }
}
