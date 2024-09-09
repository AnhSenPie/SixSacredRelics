using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnhSenPie
{
    public class MonsterController : MonoBehaviour
    {
        // Public variables
        public float speed;
        public float changeTime = 3.0f;
        public float expAmount = 1.0f;
        [SerializeField] ExpCollectible exp;
        // Private variables
        Rigidbody2D rigidbody2d;
        Animator animator;
        float timer;
        int direction = 1;

        //Health system
        public float maxHealth = 500;
        public float currentHealth = 0;
        public float health { get { return currentHealth; } }
        bool onetime = false;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            timer = changeTime;
            currentHealth = maxHealth;
            

        }


        // FixedUpdate has the same call rate as the physics system
        void FixedUpdate()
        {
            timer -= Time.deltaTime;


            if (timer < 0)
            {
                direction = -direction;
                timer = changeTime;
            }

            Vector2 position = rigidbody2d.position;

            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("MoveX", direction);
            rigidbody2d.MovePosition(position);
            if(currentHealth <= 0)
            {
                animator.SetBool("die", true);     
                if(!onetime)
                {
                    DropExp();
                    onetime = true;
                }
                Destroy(gameObject, 0.5f);             
            }
        }

        public void ChangeHealth(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
           // Debug.Log(currentHealth + "/" + maxHealth);
        }
        private void DropExp()
        {
            Vector2 spawn = new Vector2(rigidbody2d.position.x + 0.5f, rigidbody2d.position.y + 0.5f);
            ExpCollectible projectileObject = Instantiate(exp, spawn, Quaternion.identity);
        }
    }
}