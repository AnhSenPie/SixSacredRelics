using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace AnhSenPie.Inventory
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        public string UID;
        public string itemName;
        public string description;
        public Item.ItemType itemType;
        public int benefit;
        public int maxStack;
        public Sprite itemIcon;
        public bool stackable;
        public int quantity;
    }
}