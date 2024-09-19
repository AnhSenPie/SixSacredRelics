using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    Button map1;
    Button map2;
    Button map3;
    Button map4;
    Button map5;
    UIDocument uIDocument;
    VisualElement root;

    private void Awake()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        map1 = root.Q<Button>("map1");
        map2 = root.Q<Button>("map2");
        map3 = root.Q<Button>("map3");
        map4 = root.Q<Button>("map4");
        map5 = root.Q<Button>("map5");
  
        map1.clicked += ()=>  teleClicked(1);
        map2.clicked += () => teleClicked(2);
        map3.clicked += () => teleClicked(3);
        map4.clicked += () => teleClicked(4);
        map5.clicked += () => teleClicked(5);
    }
    void teleClicked(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

}
