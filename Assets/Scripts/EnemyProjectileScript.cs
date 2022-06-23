using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float minSpeed = 1.0f;
    public float maxSpeed = 2.0f;
    private float speed = 1.0f;
    public float destroyTime = 10.0f;
    public bool isBoss = false;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        if (isBoss == true)
        {
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            Physics2D.IgnoreCollision(boss.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void FixedUpdate()
    {
        if (destroyTime <= 0.0f)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 forward = new Vector2(transform.right.x, transform.right.y);
            rb.MovePosition(rb.position + forward * Time.fixedDeltaTime * speed);
            destroyTime -= Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
