using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnhSenPai.Inventory
{
    public class Item
    {
        public string UID;
        public string itemName;
        public string description;
        public ItemType itemType;
        public int quantity;
        public ItemRarelity Rarelity;
        
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
        public enum ItemRarelity
        {
            thien, //gold
            dia,   //purple
            huyen, //green
            hoang  //white
        }
    }
}