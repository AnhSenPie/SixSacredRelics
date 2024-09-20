using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsController : MonoBehaviour
{
    public GameObject key;
    public GameObject button;
    private bool isPlayerNear = false;
    public string NpcName;
    public string[] NpcDialogues;
    public Sprite NpcImage;
    void Update()
    {
        if (isPlayerNear)
        {
            button.SetActive(true);
            key.SetActive(true);
            ActiveDialogue();
        }
        else
        {
            button.SetActive(false); 
            key.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
    void ActiveDialogue()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UIDocumentManager.instance.ShowUIDocument(4);
            NPCDialogs.instance.StartDialogue(NpcName, NpcDialogues, NpcImage);
        }
    }
}
