using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class MainHealthUI : MonoBehaviour
{
    private VisualElement m_Healthbar;
    private Label hpText;
    public static MainHealthUI instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("currentHealth");
        SetHealthValue(1.0f);
        hpText = uiDocument.rootVisualElement.Q<Label>("hpText") ;
    }


    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }
    public void SetText(float currentHealth, float maxHealth)
    {
        hpText.text ="HP: " + currentHealth.ToString() + "/" + maxHealth.ToString();
    }

}
