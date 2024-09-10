using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class startScreenUI : MonoBehaviour
{
    UIDocument uiDocument;
    public float blinkSpeed = 1.0f;

    private Button myButton;
    private VisualElement gameName;
    //private SceneController sceneController;
    private bool isBlinking = false;
    private bool isRunning = true;
    private Color currentColor = Color.white;
    private float lerp = 0; //noi suy

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
       //sceneController = GetComponent<SceneController>();
        var root = uiDocument.rootVisualElement;
        myButton = root.Q<Button>("startGame");
        gameName = root.Q<VisualElement>("gameName");
        StartBlinking();
        StartColorChange();
        myButton.clicked += Onclick;
    }

    private void Update()
    {
        if (isRunning)
        {
            lerp += Time.deltaTime * 1.0f;
            if(lerp >= 1)
            {
                lerp = 1;
                isRunning = false;
            }
        }
        else
        {
            lerp -= Time.deltaTime * 1.0f;
            if(lerp < 0)
            {
                lerp = 0;
                isRunning = true;
            }
        }
        currentColor = Color.Lerp(Color.white, Color.red, lerp);
        gameName.style.unityBackgroundImageTintColor = currentColor;
        
    }
    void StartColorChange()
    {
        
        isRunning = true;
        lerp = 0;
    }
    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }
    }

    public void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            StopCoroutine(Blink());
            myButton.style.opacity = 1.0f;
        }
    }

    private System.Collections.IEnumerator Blink()
    {
        while (isBlinking)
        {

            myButton.style.opacity = 0.5f;
            yield return new WaitForSeconds(1 / blinkSpeed);


            myButton.style.opacity = 1.0f;
            yield return new WaitForSeconds(1 / blinkSpeed);
        }
    }
    void Onclick()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}