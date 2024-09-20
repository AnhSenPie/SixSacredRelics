using AnhSenPai;
using AnhSenPai.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PotionShopController : MonoBehaviour
{
    public List<Slot> ShopItems = new List<Slot>();

    public List<ItemData> ItemsList;//All item in Inventory

    private Slot selectedSlot;

    private VisualElement Root;
    private Button buyBtn, quitBtn;
    private Label itemName,  itemRarelity, description, price;
    private VisualElement itemImage, slotsContainer, itemInfo;


    private void OnEnable()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        buyBtn = Root.Q<Button>("buyBtn");
        quitBtn = Root.Q<Button>("quit");
        itemName = Root.Q<Label>("itemName");
        itemImage = Root.Q<VisualElement>("itemImage");
        itemRarelity = Root.Q<Label>("rarelity");
        description = Root.Q<Label>("description");
        price = Root.Q<Label>("quantityText");
        itemInfo    = Root.Q<VisualElement>("itemInfo");
        slotsContainer = Root.Q<VisualElement>("slotsContainer");
        InventoryRedisplay();
        for (int i = 0; i < 20; i++)
        {
            Slot slot = new Slot(OnSlotClicked);

            ShopItems.Add(slot);

            slotsContainer.Add(slot);

        }

        for(int i = 0; i < ItemsList.Count; i++)
        {
            AddItemToSlot(i, ItemsList[i]);
        }
        quitBtn.clicked += QuitShop;
        buyBtn.clicked +=  BuyItem;
       
    }
    private void OnSlotClicked(Slot slot)
    {
        if (slot.ItemData != null)
        {
            itemInfo.visible = true;

            itemImage.style.backgroundImage = new StyleBackground(slot.ItemData.itemIcon);
            description.text = slot.ItemData.description;
            itemName.text = slot.ItemData.itemName;
            selectedSlot = slot;
            price.text = slot.ItemData.Price.ToString();
            switch (slot.ItemData.Rarelity)
            {
                case Item.ItemRarelity.thien:
                    itemRarelity.text = "Thien Cap";
                    break;
                case Item.ItemRarelity.dia:
                    itemRarelity.text = "Dia Cap";
                    break;
                case Item.ItemRarelity.huyen:
                    itemRarelity.text = "Huyen Cap";
                    break;
                case Item.ItemRarelity.hoang:
                    itemRarelity.text = "Hoang Cap";
                    break;
            }
        }
    }
    public void AddItemToSlot(int slotIndex, ItemData itemData)
    {
        if (itemData != null)
        {
            ShopItems[slotIndex].AddItem(itemData, 1);
            ShopItems[slotIndex].DisableQuantityText();
        }
    }
    void QuitShop()
    {
        UIDocumentManager.instance.DisableUI(5);
    }
    void BuyItem()
    {
        bool buySucessful = false;
        if (selectedSlot != null && selectedSlot.ItemData != null)
        {
            if(InventoryController.instance.Budget.quantity >= selectedSlot.ItemData.Price)
            {
                InventoryController.instance.consumeGem(selectedSlot.ItemData.Price);
                InventoryController.instance.AddItemByUID(selectedSlot.ItemData.UID, 1);
                buySucessful = true;
                selectedSlot.ItemData.sellQuantity -= 1;
            }
        }
        if(buySucessful)
        {
            InventoryController.instance.InventoryRedisplay();
        }
    }
    public void InventoryRedisplay()
    {
        foreach (Slot slot in ShopItems)
        {
            slot.RarelityStyle();
            slot.DisableQuantityText();
        }
        ItemsList = ItemsList.OrderBy(ItemData => ItemData.Rarelity).ToList();
    }
}
