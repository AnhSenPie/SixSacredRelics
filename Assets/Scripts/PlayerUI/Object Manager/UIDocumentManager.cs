using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDocumentManager : MonoBehaviour
{
    public UIDocument[] uiDocuments;
    public static UIDocumentManager instance;
    int currentIndex = -1;
    public bool uiOnEnable;
    void Start()
    {
        NoneDisPlayUI();
        ShowUIDocument(3);
        uiOnEnable = false;
        instance = this;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            UsingUIDocument(2);
        }
        CheckUIEnable();
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UsingUIDocument(0);
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            UsingUIDocument(1);
        }
    }
    public void ShowUIDocument(int index)
    {
        NoneDisPlayUI();  
        if (index >= 0 && index < uiDocuments.Length )
        {
            uiDocuments[index].rootVisualElement.style.display = DisplayStyle.Flex;
        }
  
    }
    public void NoneDisPlayUI()
    {
        foreach (var doc in uiDocuments)
        {
            doc.rootVisualElement.style.display = DisplayStyle.None;
        }
    }
    public void UsingUIDocument(int index)
    {
        if (currentIndex == index)
        {
            DisableUI(index);
        }
        else
        {
            ShowUIDocument(index);
            currentIndex = index;
        }
    }
    public void DisableUI(int index)
    {
        uiDocuments[index].rootVisualElement.style.display = DisplayStyle.None;
        ShowUIDocument(3);
        currentIndex = -1;
    }
    void CheckUIEnable()
    {
        if (currentIndex != -1)
        {
            uiOnEnable = true;
        }
        else
        {
            uiOnEnable = false;
        }
    }
}
