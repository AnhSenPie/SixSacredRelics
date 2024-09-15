using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class MainManaUI : MonoBehaviour
{
    private VisualElement m_Manabar;
    private Label mpText;
    public static MainManaUI instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Manabar = uiDocument.rootVisualElement.Q<VisualElement>("mpBar");
        SetValue(1.0f);
        mpText = uiDocument.rootVisualElement.Q<Label>("manaText");
    }


    public void SetValue(float percentage)
    {
        m_Manabar.style.width = Length.Percent(100 * percentage);
    }
    public void SetText(float currentMP, float maxMP)
    {
        mpText.text = "MP: " + currentMP.ToString() + "/" + maxMP.ToString();
    }

}
