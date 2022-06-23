using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    [Header("Normal Settings")]
    public int currentHealth;
    public int maxHealth = 5;
    public HealthBarScript HBScript;

    [Header("Boss Settings")]
    public bool isBoss = false;
    public Transform[] platforms;
    private int currentPlatformIndex = -1;

    void Start()
    {
        currentHealth = maxHealth;
        ShowHPBar();
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            if (isBoss == true)
            {
                SceneManager.LoadScene("3 Winner Scene");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isBoss == true)
        {
            int randomIndex = Random.Range(0, platforms.Length);
            if (randomIndex == currentPlatformIndex)
            {
                while (randomIndex == currentPlatformIndex)
                {
                    randomIndex = Random.Range(0, platforms.Length);
                }
            }
            currentPlatformIndex = randomIndex;
            transform.position = new Vector2(platforms[randomIndex].position.x + 4.0f, platforms[randomIndex].position.y + 1.0f);
        }
        currentHealth = currentHealth - damage;
        ShowHPBar();
    }

    private void ShowHPBar()
    {
        HBScript.setHealth(currentHealth, maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
