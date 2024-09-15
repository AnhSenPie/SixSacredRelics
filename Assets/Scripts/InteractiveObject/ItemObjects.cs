using AnhSenPai;
using AnhSenPai.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjects : MonoBehaviour
{
    public ItemData item;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();
        if (controller != null)
        {
            PlayerController.instance.AddItemByUID(item.UID, 1);
            Destroy(gameObject, 0.3f);
        }
        
    }
}
