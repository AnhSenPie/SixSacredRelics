using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnhSenPai.Weapon
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName ="Weapons/NewWeapon")]
    public class Weapon : ScriptableObject
    {
        public string UID;
        public string Name;
        public string Description;
        public Sprite shape;
        public WeaponType type;
        public int baseAtk;
        public int bonusEffects;
    }
    public enum WeaponType
    {
        Slash,
        Shoot
    }
}