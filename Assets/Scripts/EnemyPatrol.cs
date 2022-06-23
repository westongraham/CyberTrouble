using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Range")]
    [SerializeField] public Transform leftEdge;
    [SerializeField] public Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemyObject;

    [Header("Movement Parameters")]
    [SerializeField] private float speed = 1f;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration = 1f;
    private float idleTimer;

    void Start()
    {
        speed = Random.Range(1, 3);
    }

    void Awake()
    {
        initScale = enemyObject.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemyObject.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (enemyObject.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void MoveInDirection(int directionPosition)
    {
        idleTimer = 0;

        enemyObject.localScale = new Vector3(Mathf.Abs(initScale.x) * directionPosition,
            initScale.y, initScale.z);

        enemyObject.position = new Vector3(enemyObject.position.x + Time.deltaTime * directionPosition * speed,
            enemyObject.position.y, enemyObject.position.z);

    }
}
