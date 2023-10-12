using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameter")]
    [SerializeField] private float patrolSpeed = 3f;
    [SerializeField] private float detectionDistance = 5f; 
    private bool movingLeft;

    private Quaternion initialRotation;
    private Transform player;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        initialRotation = enemy.rotation;
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        enemy.position = rightEdge.position;
    }
    private void Update()
    {
        if (Vector3.Distance(enemy.position, player.position) <= detectionDistance)
        {
           
            LookAtPlayer();
            Debug.Log("Disabling Animator");
            anim.SetBool("Idle",true);

        }
        else
        {
            
            Patrol();
        }
    }

    private void Patrol()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveDirection(1);
            else
                DirectionChange();
        }
        anim.SetBool("Idle", false);
    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;

        float newYRotation = movingLeft ? -110 : 110f;
        enemy.rotation = Quaternion.Euler(initialRotation.eulerAngles.x, newYRotation, initialRotation.eulerAngles.z);
    }

    private void MoveDirection(int direction)
    {
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * patrolSpeed,
        enemy.position.y, enemy.position.z);
    }

    private void LookAtPlayer()
    {
        Vector3 directionToPlayer = player.position - enemy.position;
        directionToPlayer.y = 0;
        enemy.rotation = Quaternion.LookRotation(directionToPlayer);
        Debug.Log("enemy look at");
       
    }
}
