
using AnhSenPie;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


namespace AnhSenPai.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        public SkillSys[] SkillList;
        PlayerController player;
        public static WeaponController instance;
        private void Awake()
        {
            player = GetComponent<PlayerController>();
            instance = this;
        }

        public void CastSkill() //index require
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(player.Launch(3, SkillList[0].qPrefab, 5));
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                StartCoroutine( player.Launch(3, SkillList[0].wPrefab, 5));
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine( player.Launch(3, SkillList[0].ePrefab, 5));
            }
            if (Input.GetKeyDown(KeyCode.R))
            {           
                StartCoroutine(player.Launch(3, SkillList[0].rPrefab, 5));
            }
        }
    }
}