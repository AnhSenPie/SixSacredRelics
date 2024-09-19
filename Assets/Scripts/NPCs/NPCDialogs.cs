using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCDialogs : MonoBehaviour
{
    public VisualElement root;
    public Label talkerName;
    public Label Content;
    public Button x2, skip, auto, buy, cancel, next;

    public static NPCDialogs instance;


    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        instance = this;
        talkerName = root.Q<Label>("TalkerName");
        Content = root.Q<Label>("Content");
        x2 = root.Q<Button>("x2Btn");
        auto = root.Q<Button>("autoBtn");
        skip = root.Q<Button>("skipBtn");
        buy = root.Q<Button>("interactBtn");
        cancel = root.Q<Button>("cancelBtn");
        next = root.Q<Button>("nextBtn");
    }
    void Start()
    {
        x2.visible = false;
        auto.visible = false;
        skip.visible = false;
        buy.visible = false;
        cancel.visible = false;
        next.visible = true;
        next.clicked += EndDialogue;
        buy.clicked += EndDialogue;
        cancel.clicked += OnCanceling;
    }

    
    void EndDialogue()
    {
        buy.visible = true;
        cancel.visible = true;
    }
    void ShopOpener()
    {
        Debug.Log("Buy Clicked");
    }
    void OnCanceling()
    {
        UIDocumentManager.instance.DisableUI(4);
    }
}
