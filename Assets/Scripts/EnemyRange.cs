using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    [Header("Player Layer")]
    [SerializeField] LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float range = 4f;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance = 0.5f;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireObject;
    public AudioSource projectileSound;

    private PlayerScript psScript;
    private EnemyPatrol epScript;
    private bool isLeft = false;

    void Awake()
    {
        epScript = GetComponentInParent<EnemyPatrol>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (transform.position.x > epScript.rightEdge.position.x && isLeft == false)
        {
            //Debug.Log("Going Left");
            isLeft = true;
            firePoint.rotation = Quaternion.Euler(new Vector3(firePoint.rotation.x, firePoint.rotation.y, 180));
            //firePoint.Rotate(firePoint.rotation.x, firePoint.rotation.y, 180);
        }

        if (transform.position.x < epScript.leftEdge.position.x && isLeft == true)
        {
            //Debug.Log("Going Right");
            isLeft = false;
            firePoint.rotation = Quaternion.Euler(new Vector3(firePoint.rotation.x, firePoint.rotation.y, 0));
            //firePoint.Rotate(firePoint.rotation.x, firePoint.rotation.y, 180);
        }

        if (PlayerInSight())
        {
            if(cooldownTimer >= attackCooldown)
            {
                //RangeAnimationHere
                cooldownTimer = 0;
                RangedAttack();
            }
        }

        if(epScript != null)
        {
            epScript.enabled = !PlayerInSight();
        }
    }

    private void RangedAttack()
    {
        if (projectileSound != null)
        {
            projectileSound.Play();
        }
        Instantiate(fireObject, firePoint.position, firePoint.rotation);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

}
