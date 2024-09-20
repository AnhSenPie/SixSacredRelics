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
    private VisualElement u_npcImage;

    public static NPCDialogs instance;


    private int currentDialogueIndex;
    private string[] dialogues;
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        
        talkerName = root.Q<Label>("TalkerName");
        Content = root.Q<Label>("Content");
        x2 = root.Q<Button>("x2Btn");
        auto = root.Q<Button>("autoBtn");
        skip = root.Q<Button>("skipBtn");
        buy = root.Q<Button>("interactBtn");
        cancel = root.Q<Button>("cancelBtn");
        next = root.Q<Button>("nextBtn");
        u_npcImage = root.Q<VisualElement>("npcImage");
        instance = this;
        x2.visible = false;
        auto.visible = false;
        skip.visible = false;
        buy.visible = false;
        cancel.visible = false;
        next.visible = true;
        next.clicked += ShowNextDialouge;
        buy.clicked += ShopOpener;
        cancel.clicked += OnCanceling;
    }

    void EndDialogue()
    {     
        buy.visible = true;
        cancel.visible = true;
    }
    void ShopOpener()
    {
        UIDocumentManager.instance.ShowUIDocument(5);
    }
    void OnCanceling()
    {
        UIDocumentManager.instance.DisableUI(4);    
    }
    public void StartDialogue(string npcName, string[] npcDialogues, Sprite npcImage) 
    {
        talkerName.text = npcName;
        dialogues = npcDialogues;
        u_npcImage.style.backgroundImage = npcImage.texture;
        
        currentDialogueIndex = 0;
        ShowNextDialouge();
    }
    public void ShowNextDialouge()
    {
        if(currentDialogueIndex < dialogues.Length)
        {
            Content.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

}
