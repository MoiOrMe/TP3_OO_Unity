using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesMovement : MonoBehaviour
{
    public Transform[] patrolPoints;  // Array of points for the enemy to patrol
    public float patrolSpeed = 2f;    
    public float detectionRange = 5f; 
    public float attackRange = 2f;    

    private Transform player;
    private int currentPatrolIndex;
    private bool playerInRange;

    private Animator animator;

    void Start()
    {
        currentPatrolIndex = 0;
        player = GameObject.FindWithTag("Player").transform;  
        animator = GetComponent<Animator>();
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (!playerInRange) 
            {
                Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
                transform.position = Vector3.MoveTowards(transform.position, targetPatrolPoint.position, patrolSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPatrolPoint.position) < 0.2f)
                {
                    currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                }

                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false); 
            }
            yield return null;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= detectionRange)
        {
            playerInRange = true;
            ChasePlayer();
        }
        else
        {
            playerInRange = false;
        }

        // If player is in attack range, stop moving and attack
        if (playerInRange && distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, patrolSpeed * Time.deltaTime);
        transform.LookAt(player);  // Make the enemy face the player
        animator.SetBool("isRunning", true);
    }

    void AttackPlayer()
    {
        // Stop movement and trigger attack animation
        animator.SetBool("isRunning", false);
        animator.SetTrigger("Attack");
    }
}
