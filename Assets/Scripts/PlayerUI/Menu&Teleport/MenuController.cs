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
    bool isOpen = false;
  
    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        inventoryBtn = root.Q<Button>("inventoryBtn");
        teleBtn = root.Q<Button>("TeleportBtn");
        quitBtn = root.Q<Button>("QuitBtn");
        root.visible = isOpen;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            isOpen = !isOpen;
            root.visible = isOpen;
        }       
    }
}
