using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float rotationSpeed = 20f; 
    public Transform player; 
    public float attackDistance = 5f; 
    public bool isPlayerInRange = false;
    public Transform skelMesh;

    private Vector3 target;
    public Animator animator;
    

    void Start()
    {
        target = pointA.position; 
        
        
    }

    void Update()
    {
        if (!isPlayerInRange)
        {
            
            Patrol();
        }
        else
        {
            
            AttackPlayer();
        }

        if (skelMesh != null)
        {
            skelMesh.rotation = transform.rotation; // Synchroniser la rotation du SkelMesh avec celle de Ennemie
        }
    }

    void Patrol()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);


        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            LookAtTarget(target);
        }


        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void AttackPlayer()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        
        

       
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            
            animator.SetBool("isPunching", true);
        }

        player.GetComponent<CharacterStats>().TakeDamage(10);
    }



    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            animator.SetBool("isPunching", false);
            target = pointA.position; 
        }
    }
}
