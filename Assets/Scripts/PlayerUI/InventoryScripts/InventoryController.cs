using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using AnhSenPai;
using System.Linq;


namespace AnhSenPai.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        public List<Slot> InventoryItems = new List<Slot>();

        public List<ItemData> InventoryData;//All item in Inventory

        public ItemData Budget;
        public static InventoryController instance { get; private set; }

        private VisualElement m_Root;
        private VisualElement m_SlotContainer;
        private VisualElement m_ItemInfo;
        private VisualElement m_ItemImage;
        private Label m_Rarelity;
        private Label m_ItemText;
        Label budgetText;       private Label m_itemName;
        private int currentslot = 200;
        private int itemCount;
        private Slot selectedSlot;

        private Button deleteBtn;
        private Button usingBtn;

        
        private void OnEnable()
        {
            instance = this;
            //Store the root from the UI Document component
            m_Root = GetComponent<UIDocument>().rootVisualElement;

            //Search the root for the SlotContainer Visual Element
            m_SlotContainer = m_Root.Q<VisualElement>("slotsContainer");

            m_ItemInfo = m_Root.Q<VisualElement>("itemInfo");
            m_ItemText = m_Root.Q<Label>("description");
            m_ItemImage = m_Root.Q<VisualElement>("itemImage");
            m_Rarelity = m_Root.Q<Label>("rarelity");
            deleteBtn = m_Root.Q<Button>("dropBtn");
            usingBtn = m_Root.Q<Button>("useBtn");
            m_itemName = m_Root.Q<Label>("itemName");
            budgetText = m_Root.Q<Label>("quantityText");

            //Create InventorySlots and add them as children to the SlotContainer
            InventoryRedisplay();
            for (int i = 0; i < currentslot; i++)
            {
                Slot item = new Slot(OnSlotClicked);

                InventoryItems.Add(item);

                m_SlotContainer.Add(item);
                
            }

            for(int i = 0; i < InventoryData.Count; i++)
            {
                if (InventoryData[i].quantity <= 0)
                {
                    InventoryData.RemoveAt(i);
                }
            }
            itemCount = InventoryData.Count;

            for(int i = 0; i  < itemCount; i++)
            {
                AddItemToSlot( i, InventoryData[i]);                   
            }
            //Update Inventory if quantity of item <= 0
            for (int i = 0; i < itemCount; i++)
            {
                if (InventoryData[i].quantity <= 0)
                {
                    selectedSlot = InventoryItems[i];
                    int Index = InventoryItems[i].IndexOf(selectedSlot);
                    ShiftItems(Index);
                    OnDeleteBtnClicked();
                }
            }

            consumeGem(0);

            deleteBtn.clicked += OnDeleteBtnClicked;
            usingBtn.clicked += OnUsingBtnClicked;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                AddItemByUID("diamond", 10);
            }
        }

        IEnumerator decreasingTime(float time, float baseIndex, float buffIndex)
        {
            yield return new WaitForSeconds(time);
            
            baseIndex -= buffIndex;

            Debug.Log(baseIndex);
        }
        public void AddItemToSlot( int slotIndex, ItemData itemData)
        {
            if ( itemData != null)
            {
                InventoryItems[slotIndex].AddItem(itemData, 1);
               // m_Quantity.visible = itemData.stackable;
                //m_Quantity.text = itemData.maxStack.ToString();
            }
        }

        bool foundItem;
        public void AddItemByUID(string UID, int count)
        {
            
            for(int i = 0; i < InventoryData.Count; i++)
            {
               if(InventoryData[i].UID == UID && InventoryData[i].stackable == false)
                {
                    for(int j = 0; j < InventoryItems.Count; j++)
                    {
                        
                        if (InventoryItems[j].ItemData == null)
                        { 
                            foundItem = true;
                            InventoryItems[j].AddItem( InventoryData[i], count);
                            break;
                        }                        
                    }
                }
               else if (InventoryData[i].UID == UID && InventoryData[i].stackable && InventoryData[i].quantity > 0)
                {   
                    InventoryItems[i].ItemData.quantity += count;
                    InventoryItems[i].AddItem(InventoryData[i], count);
                    if (InventoryData[i].quantity <= 0)
                    {
                        OnDeleteBtnClicked();
                        
                    }
                    foundItem = true;
                    break;
                }
               else if (InventoryData[i].UID == UID && InventoryData[i].quantity <= 0)
                {
                    foundItem = false;
                    break;
                }

            }
            if(!foundItem)
            {
                Debug.Log("Not found Item");
            }
            
        }

        public void consumeGem(int count)
        {
            Budget.quantity -= count;
            budgetText.text = Budget.quantity.ToString();
        }
        private void OnSlotClicked(Slot slot)
        {
            if (slot.ItemData != null)
            {
                m_ItemInfo.visible = true;

                m_ItemImage.style.backgroundImage = new StyleBackground(slot.ItemData.itemIcon);
                m_ItemText.text = slot.ItemData.description;
                m_itemName.text = slot.ItemData.itemName;
                selectedSlot = slot;
                switch (slot.ItemData.Rarelity)
                {
                    case Item.ItemRarelity.thien:
                        m_Rarelity.text = "Thien Cap";
                        break;
                    case Item.ItemRarelity.dia:
                        m_Rarelity.text = "Dia Cap";
                        break;
                    case Item.ItemRarelity.huyen:
                        m_Rarelity.text = "Huyen Cap";
                        break;
                    case Item.ItemRarelity.hoang:
                        m_Rarelity.text = "Hoang Cap";
                        break;
                }
            }
        }
        private void OnDeleteBtnClicked()
        {
            if(selectedSlot != null && selectedSlot.ItemData != null)
            {
                int INdex = InventoryItems.IndexOf(selectedSlot);
                selectedSlot.ClearSlot();
                selectedSlot = null;
                InventoryData.RemoveAt(INdex);
                ShiftItems(INdex);
            }
            InventoryRedisplay();
            m_ItemInfo.visible = false;

        }
        
        public void ShiftItems(int Index)
        {
            for (int i = Index; i < InventoryItems.Count - 1; i++)
            {
                Slot currentSlot = InventoryItems[i];
                Slot nextSlot = InventoryItems[i + 1];

                
                if (nextSlot.ItemData != null)
                {
                    currentSlot.ItemData = nextSlot.ItemData;
                    currentSlot.itemCount = nextSlot.itemCount;
                    currentSlot.ItemGuid = nextSlot.ItemGuid;
                    nextSlot.ClearSlot(); 
                    currentSlot.UpdateSlot();
                }
            }
        }
        public void OnUsingBtnClicked()
        {
            if(selectedSlot != null && selectedSlot.ItemData != null)
            {
                selectedSlot.ItemData.quantity -= 1;
                //player + n effects
                switch (selectedSlot.ItemData.itemType)
                {
                    case Item.ItemType.Healing:
                        PlayerController.instance.ChangeHealth(selectedSlot.ItemData.benefit);
                        Debug.Log("Healing Item");
                        break;
                    case Item.ItemType.ExpIncreasing:
                        PlayerController.instance.ChangeExp(selectedSlot.ItemData.benefit);
                        Debug.Log("Exp Increasing Item");
                        break;
                    case Item.ItemType.ManaBonus:
                        PlayerController.instance.ChangeMana(selectedSlot.ItemData.benefit);
                        Debug.Log("Mana Item");
                        break;
                    case Item.ItemType.Weapon:
                        Debug.Log("Weapon");
                        break;
                    case Item.ItemType.Atkbuff:
                        PlayerController.instance.baseAtk += selectedSlot.ItemData.benefit;
                        StartCoroutine(decreasingTime(300f, PlayerController.instance.baseAtk, selectedSlot.ItemData.benefit));
                        break;
                    case Item.ItemType.CritBuff:
                      
                        PlayerController.instance.baseCrit += selectedSlot.ItemData.benefit;
                        StartCoroutine(decreasingTime(300f, PlayerController.instance.baseCrit, selectedSlot.ItemData.benefit));
                        break;
                }

                if(selectedSlot.ItemData.quantity <= 0)
                {   
                    Debug.Log("Run out of " + selectedSlot.ItemData.name);
                    OnDeleteBtnClicked();     
                };
            }
            InventoryRedisplay() ;
        }
        public void InventoryRedisplay()
        {
            foreach(Slot slot in InventoryItems)
            {
                slot.UpdateSlot();
            }
            InventoryData = InventoryData.OrderBy(ItemData => ItemData.Rarelity).ToList();
        }
    }
}