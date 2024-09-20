using AnhSenPai.Weapon;
using AnhSenPai.Inventory;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using AnhSenPai.Music;

namespace AnhSenPai
{
    public class PlayerController : MonoBehaviour
    {
        public float movePower = 10f;
        public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D rb;
        private Animator anim;
        
        Vector3 movement;
        private int direction = 1;
        public bool isJumping = false;
        private bool alive = true;
        //HEALTH SYSTE
        public float maxHealth = 500;
        public float maxMP = 100f;
        public float currentMP;
        public float currentHealth;
       // public float health { get { return currentHealth; } }
        // Variables related to projectiles
        public GameObject lightningBullet; //đạn sét
        public GameObject ThunderSpear; //Thương lôi
        public Transform firePoint;

        //Level System
        public float currentExp, maxExp;
        public int currentLevel = 1;
        string currentTuvi;

        //Character combat index
        public float baseAtk;
        public float baseDef;
        public int baseCrit;
        public float baseCritDmg;
        //Inventory
        public int currentSlot;
        public int maxSlot = 200;

        //weapon controll
        SwitchWeapon weapon;


        //scene
        Scene scene;
   

        public static PlayerController instance;
        //Move Tech
        
        Vector2 moveDirection = new Vector2(1, 0);
        private bool doubleJump;
        void Start()
        {
       
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();

            GetBasicIndex();

            MainHealthUI.instance.SetText(currentHealth, maxHealth);   
            MainManaUI.instance.SetText(currentMP, maxMP);    
            ExpManager.instance.SetExpText(currentExp, maxExp);
            ExpManager.instance.SetLvlNum(currentLevel);
            ExpManager.instance.CircularAnimateUI();
            
            currentSlot = maxSlot;

            //firePoint = GetComponent<Transform>();
            weapon = GetComponent<SwitchWeapon>();


            
            instance = this;

            scene = SceneManager.GetActiveScene();
           
        }

        private void Update()
        {
            Restart();        
            if (alive && UIDocumentManager.instance.uiOnEnable == false)
            {               
                //Hurt();
                Die();
                Attack();               
                Jump();
                Run();
                UpdateInfo();
                LevelUp();
                Fly();
                WeaponController.instance.CastSkill(weapon.weaponIndex);
            }
            ChangeSound();
            scene = SceneManager.GetActiveScene();
            SaveBasicIndex();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {   
            if(other.gameObject.CompareTag("ground"))
            {
                anim.SetBool("isJump", false);
                rb.gravityScale = 1f;
                isJumping = false;
            }
            
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if(collision.collider.tag == "ground")
            isJumping = true;
        }
        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);


            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(direction, 1, 1);
                
                if (!isJumping)
                    anim.SetBool("isRun", true);
                

            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(direction, 1, 1);
                
                if (!isJumping)
                    anim.SetBool("isRun", true);

            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
          
            moveDirection.Set(direction, 0);
        }
        void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJump", true);
            }          
        }
        void Attack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("attack");
                switch(weapon.weaponIndex)
                {
                    case 0:
                        ResetAttackAnim();
                        anim.SetFloat("Staff", 0.5f);
                        StartCoroutine(Launch(1, lightningBullet, 1));
                        break;
                    case 1:
                        ResetAttackAnim();
                        anim.SetFloat("Staff", 0.5f);
                     
                        break;
                }
            }
          
        }

        void ResetAttackAnim()
        {
            anim.SetFloat("Staff", 0);
            anim.SetFloat("Spear", 0);
        }
        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
                ChangeHealth(-100);
            }
        }
        void Die()
        {
            if (currentHealth == 0)
            {
                anim.SetTrigger("die");
                alive = false;
            }
        }
        void Fly()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                rb.gravityScale = 0;
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJump", true);
            }
            if(rb.gravityScale == 0)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    rb.AddForce(Vector2.down * jumpPower, ForceMode2D.Impulse);
                    anim.SetBool("isJump", true);
                }
                currentMP -= Time.deltaTime;
            }
            if(currentMP <= 0)
            {
                rb.gravityScale = 1;
            }

        }

        public void ChangeHealth(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        }
        public void ChangeMana(float amount)
        {
            currentMP = Mathf.Clamp(currentMP + amount, 0, maxMP);
        }
        //Exp Sys Func
        public void ChangeExp(float amount)
        {
            currentExp = Mathf.Clamp(currentExp + amount, 0, maxExp*2);
                
        }

        void ExpCollected()
        {        
        }
        void UpdateInfo()
        {
            MainHealthUI.instance.SetText(currentHealth, maxHealth);
            MainHealthUI.instance.SetHealthValue(currentHealth / maxHealth);
            ExpManager.instance.SetExpText(currentExp, maxExp);
            MainManaUI.instance.SetText(currentMP, maxMP);
            MainManaUI.instance.SetValue(currentMP/maxMP);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            ExpCollectible exp = other.GetComponent<ExpCollectible>();
            if(exp != null)
            {
                currentExp += exp.expAmount;                
                Destroy(exp.gameObject);
            }             
        }
        public void LevelUp()
        {
            if(currentExp >= maxExp)
            {
                currentLevel += 1;
                currentTuvi = LevelName.instance.CanhGioi(currentLevel);
                currentExp = currentExp - maxExp;
                
                maxExp +=  LevelName.instance.ExpBonus;
                maxHealth += 1000;
                baseCrit += 2;
                baseCritDmg += 10;
                baseAtk += LevelName.instance.atkBonus;
                baseDef += LevelName.instance.defBonus;
                maxMP += LevelName.instance.mpBonus;
                ChangeHealth(maxHealth - currentHealth);
                ChangeMana(maxMP - currentMP);
                ExpManager.instance.SetLvlNum(currentLevel);                
                ExpManager.instance.SetLevelName(currentTuvi);
                Debug.Log(LevelName.instance.ExpBonus);
            }
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }
        public IEnumerator Launch(int n, GameObject prefabs, float v)
        {
            for (int i = 0; i < n; i++)
            {
                Vector2 spawn = new Vector2(firePoint.position.x + 0.5f, firePoint.position.y + 1.0f);
                GameObject projectileObject = Instantiate(prefabs, spawn, firePoint.rotation);
                Projectile projectile = projectileObject.GetComponent<Projectile>();

                    projectile.GetComponent<Projectile>().transform.localScale = new Vector3(-1, 1, 0);
                    projectile.Launch(WeaponController.instance.direction, 10*v);

                Destroy(projectileObject, 3.0f);
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void AddItemByUID(string UID, int n)
        {
            InventoryController.instance.AddItemByUID(UID, n);
        }
        void ChangeSound()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                AudioManager.instance.PlayMusic(AudioManager.instance.musicSounds[scene.buildIndex].name);
            }
        }
        void SaveBasicIndex()
        {
            PlayerPrefs.SetFloat("currentMP", currentMP);
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            PlayerPrefs.SetFloat("currentHP", currentHealth);
            PlayerPrefs.SetFloat("currentExp", currentExp);
            //maxhp, maxmp, maxexp, basecrit, basecd, baseatk, basedef
            PlayerPrefs.SetFloat("maxHP", maxHealth);
            PlayerPrefs.SetFloat("maxMP", maxMP);
            PlayerPrefs.SetFloat("maxExp", maxExp);
            PlayerPrefs.SetFloat("baseAtk", baseAtk);
            PlayerPrefs.SetFloat("baseDef", baseDef);
            PlayerPrefs.SetInt("baseCrit", baseCrit);
            PlayerPrefs.SetFloat("baseCritDmg", baseCritDmg);

        }
        public void GetBasicIndex()
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel", 1);
            currentMP = PlayerPrefs.GetFloat("currentMP", 100);
            currentHealth = PlayerPrefs.GetFloat("currentHP", 500);
            currentExp = PlayerPrefs.GetFloat("currentExp", 0);
            maxHealth = PlayerPrefs.GetFloat("maxHP", 500);
            maxMP = PlayerPrefs.GetFloat("maxMP", 100);
            maxExp = PlayerPrefs.GetFloat("maxExp", 10);
            baseAtk = PlayerPrefs.GetFloat("baseAtk", 10);
            baseCrit = PlayerPrefs.GetInt("baseCrit", 5);
            baseCritDmg = PlayerPrefs.GetFloat("baseCritDmg", 10);
            baseDef = PlayerPrefs.GetFloat("baseDef", 10);
        }
    }
}

public static class Utility
{
    public static void Invoke(this MonoBehaviour mb, Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }

    private static IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }
}