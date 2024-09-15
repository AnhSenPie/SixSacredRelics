
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace AnhSenPai
{
    public class ExpManager : MonoBehaviour
    {
        private VisualElement m_Expbar;
        private VisualElement m_AvatarOuter;
        private Label lvNum;
        private Label expPercentage;
        private Label lvName;
        public VisualElement m_Root;
        public static ExpManager instance { get; private set; }

        private void Awake()
        {
            instance = this;
            m_Root = GetComponent<UIDocument>().rootVisualElement;
            UIDocument uiDocument = GetComponent<UIDocument>();
            m_Expbar = uiDocument.rootVisualElement.Q<VisualElement>("ExpImg");
            m_AvatarOuter = uiDocument.rootVisualElement.Q<VisualElement>("khungavt");

            lvName = uiDocument.rootVisualElement.Q<Label>("levelName");
            expPercentage = uiDocument.rootVisualElement.Q<Label>("expText");
            lvNum = uiDocument.rootVisualElement.Q<Label>("levelNum");
        }

        public void SetLevelName(string currentTuVi)
        {

            lvName.text = currentTuVi.ToString();
        }
        public void SetExpText(float currentExp, float maxExp)
        {
            float tg = 0.0f;
            if (currentExp <= maxExp)
            {
                tg = (currentExp / maxExp) * 100;
            }
            else if (currentExp > maxExp)
            {
                currentExp = currentExp - maxExp;
                maxExp += 100;
                tg = (currentExp / maxExp) * 100;
            }
            if (tg > 100f)
            {
                tg -= 100f;
            }
            else if (tg == 100f)
            {
                tg = 0.0f;
            }
            expPercentage.text = "+ " + tg.ToString("F2") + "%";
            //CircularAnimateUI();
        }
        public void SetLvlNum(float currentLvl)
        {
            lvNum.text = "Level " + currentLvl.ToString();
        }
        public void CircularAnimateUI()
        {
            //Khung avatar animation
            Tween outerTween = DOTween.To(()
            => m_AvatarOuter.worldTransform.rotation.eulerAngles,
            x => m_AvatarOuter.transform.rotation = Quaternion.Euler(x),
            new Vector3(0, 0, 360), 3.0f)
            .SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

            //Background avatar animation
            Tween innerTween = DOTween.To(()
            => m_Expbar.worldTransform.rotation.eulerAngles,
            x => m_Expbar.transform.rotation = Quaternion.Euler(x),
            new Vector3(0, 0, -360), 3.0f)
            .SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);

        }
        public void Dotpha(int currentLevel)
        {
            if(currentLevel > 1 && (currentLevel-1) % 3 + 1 == 1)
            {
                // Đột phá tiểu cảnh giới
            }
            if((currentLevel - 1) % 10 + 1 == 1 && currentLevel > 1)
            {
                // Đột phá đại cảnh giới
            }
        }
    }
}