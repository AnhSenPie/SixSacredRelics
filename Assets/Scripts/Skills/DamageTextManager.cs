using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace AnhSenPie
{
    public class DamageTextManager : MonoBehaviour
    {
        public VisualTreeAsset damageTextTemplate;
        private VisualElement rootElement;
        public static DamageTextManager instance;

        private void Awake()
        {          
            rootElement = GetComponent<UIDocument>().rootVisualElement;
            instance = this;
        }

        public void ShowDamageText(Vector3 worldPosition, long damageAmount, bool isCritical)
        {
            VisualElement damageText = damageTextTemplate.Instantiate();
            Label textLabel = damageText.Q<Label>("DamageText");

            
            textLabel.text = damageAmount.ToString();

            Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

            damageText.style.left = screenPosition.x;
            damageText.style.top = screenPosition.y;

            if (isCritical)
            {
                textLabel.AddToClassList("critText");
            }
            else
            {
                textLabel.AddToClassList("normalText");
            }

            rootElement.Add(damageText);

            StartCoroutine(RemoveDamageTextAfterSeconds(damageText, 1.0f));
        }

        private IEnumerator RemoveDamageTextAfterSeconds(VisualElement element, float delay)
        {
            yield return new WaitForSeconds(delay);
            element.RemoveFromHierarchy();
        }
    }
}