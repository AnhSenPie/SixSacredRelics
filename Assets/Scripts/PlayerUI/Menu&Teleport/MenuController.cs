using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuController : MonoBehaviour
{
    Button inventoryBtn;
    Button teleBtn;
    Button quitBtn;
    UIDocument uIDocument;
    VisualElement root;
   
  
    private void OnEnable()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        inventoryBtn = root.Q<Button>("inventoryBtn");
        teleBtn = root.Q<Button>("TeleportBtn");
        quitBtn = root.Q<Button>("QuitBtn");
    
        teleBtn.clicked += OnTeleportClicked;
    }
    public void OnTeleportClicked()
    {
        UIDocumentManager.instance.UsingUIDocument(1);
    }
}
