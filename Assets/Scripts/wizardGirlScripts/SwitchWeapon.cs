
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace AnhSenPai.Weapon
{
    public class SwitchWeapon : MonoBehaviour
    {
     
        public int  weaponIndex;
        public List<GameObject> WeaponList;

        private void Awake()
        {
           for(int i = 0; i < WeaponList.Count; i++)
            {
                WeaponList[i].SetActive(false);
            }
  
        }

    
        private void Update()
        {
            getKey();
            SwapWeapon();

        }
        void getKey()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                weaponIndex = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weaponIndex = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                weaponIndex = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                weaponIndex = 3;
            }
        }
        void SwapWeapon()
        {
            if (weaponIndex > WeaponList.Count)
            {
                Debug.Log("OutRange");
            }
            else
            {
                for (int i = 0; i < WeaponList.Count; i++)
                {
                    if(i != weaponIndex)
                    {
                        WeaponList[i].SetActive(false) ;
                    }
                    if(WeaponList[weaponIndex].activeInHierarchy == false)
                    {
                        WeaponList[weaponIndex].SetActive(true);
                    }
                    
                }
            }
            
        }

    }
}