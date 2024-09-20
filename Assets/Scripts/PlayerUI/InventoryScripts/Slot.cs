using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
namespace AnhSenPai.Inventory
{
    public class Slot : VisualElement
    {
        public Image Icon;
        public Label quantity;
        public string ItemGuid = "";
        public ItemData ItemData;
        public int itemCount;

        public Slot(System.Action<Slot> onClickCallback)
        {
            //Create a new Image element and add it to the root
            Icon = new Image();
            Add(Icon);
            quantity = new Label();
            Add(quantity);
            quantity.AddToClassList("quantity");          
            Icon.AddToClassList("slotIcon");
            AddToClassList("slot");

            this.RegisterCallback<ClickEvent>(evt => onClickCallback(this));
        }
        public bool AddItem(ItemData data, int count)
        {
            if (ItemData == null)
            {
                ItemData = data;
                itemCount = Mathf.Min(count, data.maxStack);
                UpdateSlot();
                return true;
            }   
            if (ItemData == data && ItemData.stackable)
            {
                int newCount = itemCount + count;
                if (newCount <= ItemData.maxStack)
                {
                    itemCount = newCount;
                    UpdateSlot();
                    return true;
                }
            }        
            return false;
        }
        public void ClearSlot()
        {          
            ItemGuid = "";
            ItemData = null;
            Icon.sprite = null;
            quantity.text = null;       
            
            UpdateSlot() ;
        }
        public void UpdateSlot()
        {
            if(ItemData != null)
            {
                Icon.sprite = ItemData.itemIcon;
                Icon.scaleMode = ScaleMode.StretchToFill;
                //quantity.visible = ItemData.stackable;
                quantity.text = ItemData.quantity.ToString();
                if(ItemData.stackable == false)
                {
                    quantity.visible = false;
                }
                RarelityStyle();
            }
            else
            {
                Icon.sprite = null;
                quantity.text= null;           
            }
        }
        public void DisableQuantityText()
        {
            quantity.text = null;
        }
        public void RarelityStyle()
        {
            switch (ItemData.Rarelity)
            {
                case Item.ItemRarelity.thien:
                    AddToClassList("thien");
                    break;
                case Item.ItemRarelity.dia:
                    AddToClassList("dia");
                    break;
                case Item.ItemRarelity.huyen:
                    AddToClassList("huyen");
                    break;
                case Item.ItemRarelity.hoang:
                    AddToClassList("hoang");
                    break;
            }
        }
    }

}

