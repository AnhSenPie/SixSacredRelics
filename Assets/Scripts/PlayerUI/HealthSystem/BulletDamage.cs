using AnhSenPie;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnhSenPie { 
public class BulletDamage : MonoBehaviour
{
        private float amount;
        private int critRate;
        private float critDamge;
        private float def ;
        [SerializeField] float BulletScale;
        private void Awake()
        {
         amount = PlayerController.instance.baseAtk;
         critRate = PlayerController.instance.baseCrit;
         critDamge = PlayerController.instance.baseCritDmg;
        }
        void OnTriggerEnter2D(Collider2D other)
    {
        MonsterController controller = other.GetComponent<MonsterController>();


        if (controller != null)
        {
            int i = Random.Range(1, 100);
                amount = amount * BulletScale;
                float DmgDeal;
                if (i <= critRate)
                {
                    DmgDeal = amount * critDamge / 100 + amount;
                    controller.ChangeHealth(-DmgDeal);
                    DamageTextManager.instance.ShowDamageText(other.transform.position, (long)DmgDeal, true);
                }
                else
                {
                    DmgDeal = amount;
                    controller.ChangeHealth(-DmgDeal);
                    DamageTextManager.instance.ShowDamageText(other.transform.position, (long)DmgDeal, false);
                }
            Debug.Log(DmgDeal.ToString());
            Destroy(gameObject);
            
        }

    }

}
}
