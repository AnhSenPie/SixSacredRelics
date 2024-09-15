using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AnhSenPai
{
    public class ExpCollectible : MonoBehaviour
    {
        public float expAmount;
        public Rigidbody2D rg;
        public PlayerController playerController;
       // private float v = 2.0f;
        public static ExpCollectible instance { get; private set; }
        private void Awake()
        {
            rg = GetComponent<Rigidbody2D>();
            instance = this;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ExpMove();
            }
            
        }
        public void ExpMove()
        {

        }
     
    }
}