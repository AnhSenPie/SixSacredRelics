using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using AnhSenPie;

namespace AnhSenPie
{
    public class WeaponController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            MonsterController monster = other.GetComponent<MonsterController>();
            if (monster != null)
            {
                Debug.Log("monser");
            }
        }
    }
}