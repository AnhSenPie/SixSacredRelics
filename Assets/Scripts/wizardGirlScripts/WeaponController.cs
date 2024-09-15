
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
        Vector3 mousePos;
        public Vector3 direction;
        public float angle;

        private void Awake()
        {
            player = GetComponent<PlayerController>();
            instance = this;
            
        }
        private void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - transform.position).normalized;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        public void CastSkill(int weaponIndex) //index require
        {
            int amount = SkillList[weaponIndex].ManaConsume;
            if (PlayerController.instance.currentMP >= amount)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    StartCoroutine(player.Launch(3, SkillList[weaponIndex].qPrefab, 5));
                    PlayerController.instance.ChangeMana(-amount);
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    StartCoroutine(player.Launch(3, SkillList[weaponIndex].wPrefab, 5));
                    PlayerController.instance.ChangeMana(-amount);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(player.Launch(3, SkillList[weaponIndex].ePrefab, 5));
                    PlayerController.instance.ChangeMana(-amount);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StartCoroutine(player.Launch(3, SkillList[weaponIndex].rPrefab, 5));
                    PlayerController.instance.ChangeMana(-amount);
                }
            }
            else
            {
                Debug.Log("Run out of Mana");
            }
        }
    }
}