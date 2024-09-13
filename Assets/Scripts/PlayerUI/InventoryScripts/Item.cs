using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnhSenPie.Inventory
{
    public class Item
    {
        public string UID;
        public string itemName;
        public string description;
        public ItemType itemType;
        public int quantity;
        
        public enum ItemType
        {
            Healing,
            ManaBonus,
            ExpIncreasing,
            SkillIncreasing,
            Atkbuff,
            Defbuff,
            CritBuff,
            CritDmgBuff,
            Weapon
        }
    }
}