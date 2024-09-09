using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    Button map1;
    UIDocument uIDocument;
    VisualElement root;
    bool isOpen = false;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        map1 = root.Q<Button>("map1");
        root.visible = isOpen;
        map1.clicked += ()=>  teleClicked(1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isOpen = !isOpen;
            root.visible = isOpen;
        }
    }
    void teleClicked(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

}
