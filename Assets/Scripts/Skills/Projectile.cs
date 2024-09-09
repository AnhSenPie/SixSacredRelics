using AnhSenPie;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AnhSenPie
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody2D rigidbody2d;
        public Animator animator;
        private float v = 3.0f;

        // Awake is called when the Projectile GameObject is instantiated
        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();    
            animator = GetComponent<Animator>();    
        }

        void Update()
        {
       
        }


        public void Launch(Vector2 direction, float force)
        {
            rigidbody2d.AddForce(direction * force * v);
         
        }

        /*    void OnCollisionEnter2D(Collision2D other)
            {
                EnemyController enemy = other.collider.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.Fix();
                }

                Destroy(gameObject);
            }*/


    }
}
