using UnityEngine;

public class AngryPig : BaseEnemy
{
    public Transform posA;
    public Transform posB;

    private void Update()
    {
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            ani.SetBool("isWalk", true);
            ani.SetBool("isRun", true);
            MoveTowardsPlayer();
        }
        else
        {
            ani.SetBool("isRun", false);
            ani.SetBool("isWalk", true);
            Patrol(); 
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        if (playerTransform.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (playerTransform.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    private void Patrol()
    {
        if (isFacingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, posB.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, posB.position) < 0.5f)
            {
                Flip(); 
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, posA.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, posA.position) < 0.5f)
            {
                Flip(); 
            }
        }
    }
}
