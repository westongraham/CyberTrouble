using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Ability")]
    private float movementSpeed = 8.0f;
    public float jumpForce = 8.0f;
    private Rigidbody2D rb;

    [Header("Player Health")]
    private int currentHealth;
    private int maxHealth = 10;
    public HealthBarScript HBScript;
    
    [Header("Player Attack")]
    private float timeBtwAtack;
    public float startTimeBtwAttack = 0.2f;
    public GameObject attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange = 1.0f;
    public int damage = 1;

    [Header("MISC")]
    public Animator animator;
    public AudioSource jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        ShowHPBar();
    }

	void Update()
	{
        if(timeBtwAtack <= 0)
        {
            if(Input.GetKey(KeyCode.M))
            {
                //AttackAnimationHere
                animator.SetBool("Attack", true);
               
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.transform.position, attackRange, whatIsEnemies);
                
                for(int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyScript>().TakeDamage(damage);
                }
            }
            timeBtwAtack = startTimeBtwAttack;
        }
        else
        {
            animator.SetBool("Attack", false);
            if (timeBtwAtack == 0)
            {
                timeBtwAtack = -1;
            }
            else
            {
                timeBtwAtack -= Time.deltaTime;
            }
        }

    	var movement = Input.GetAxis("Horizontal");
    	transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;

        bool grounded;
        animator.SetFloat("Speed", Mathf.Abs(movement));

        if (!Mathf.Approximately(0, movement))
    	{
        	transform.rotation = movement > 0 ? Quaternion.Euler(0, -180, 0) : Quaternion.identity;
        }

        grounded = true;
        if (grounded == true)
        {
            animator.SetBool("Jump", false);
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            jumpSound.Play();
            //JumpAnimationHere
            grounded = false;
            if(grounded == false)
            {
                animator.SetBool("Jump", true);
            }
            
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        
        if (currentHealth == 0 || transform.position.y < -20)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("2 Game Over Scene");
        }
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.transform.position, attackRange);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss")
        {
            currentHealth--;
            ShowHPBar();
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        ShowHPBar();
    }

    private void ShowHPBar()
    {
        HBScript.setHealth(currentHealth, maxHealth);
    }
}
