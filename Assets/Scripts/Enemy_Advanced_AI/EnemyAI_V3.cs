using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_V3 : Fighter
{
    // Patrol/moving variables

    public int xpValue = 5;
    protected BoxCollider2D boxCollider;
    protected RaycastHit2D hit;
    public Transform[] patrolPoints;
    //public float speed;
    Transform currentPatrolPoint;
    int currentPatrolIndex;
    protected Vector3 moveDelta;
    public float ySpeed = 0.25f;
    public float xSpeed = 0.25f;

    // Chase variables

    public Transform target;
    public float chaseRange;

    // Melee variables

    // public float attackRange;
    // public float damage;
    // private float lastAttackTime;
    // public float attackDelay;

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = input;

        /* Checking in which direction the character is going.
         * Then changing the direction of the character sprite to the corresponding direction (i.e. right or left)*/
        
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * ySpeed), LayerMask.GetMask("Creatures", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime * ySpeed, 0); // X = 0 and Z = 0, because we only check collision for the y direction.
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime * xSpeed), LayerMask.GetMask("Creatures", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * xSpeed, 0, 0); // Y = 0 and Z = 0, because we only check collision for the x direction.
        }


        if (moveDelta.x < 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Start()
    {
        target = GameObject.Find("Player_0").transform;
        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
  
        /**Vector3 patrolPointDir = currentPatrolPoint.position - transform.position; // Finding the direction Vector, which points to the next patrol point
        float angle = Mathf.Atan2(patrolPointDir.y, patrolPointDir.x) * Mathf.Rad2Deg - 90f; // Calculate needed rotation to turn towards the point

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward); // Made/ Calculate needed rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180f); // Apply rotation to enemy**/

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if(distanceToTarget < chaseRange)
        {
            /**Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);
            transform.Translate(Vector3.up * Time.deltaTime * ySpeed);**/
            UpdateMotor((target.position - transform.position).normalized);
        }
        else
        {
            moveDelta = (currentPatrolPoint.position - transform.position).normalized;
            UpdateMotor(moveDelta);


            //transform.Translate(Vector3.up * Time.deltaTime * speed);

            if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .1f)
            {
                if (currentPatrolIndex + 1 < patrolPoints.Length)
                {
                    currentPatrolIndex++;
                }
                else
                {
                    currentPatrolIndex = 0;
                }

                currentPatrolPoint = patrolPoints[currentPatrolIndex];
            }
        }

        /**float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer < attackRange && (lastAttackTime + attackDelay < Time.time))
        {
            target.SendMessage("ReceiveDamage", damage);
            lastAttackTime = Time.time;
        }**/
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText(xpValue + "xp", 25, Color.green, transform.position, Vector3.up * 40, 1.5f);
    }

}
