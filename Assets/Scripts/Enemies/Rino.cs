using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : BaseEnemy
{
    public Transform posA;
    public Transform posB;
    private bool isWaiting = false;
    public float waitBeforeMove = 2f;

    private void Start()
    {
        ani.SetBool("isRun", true);
    }

    private void Update()
    {
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            ani.SetBool("isRun", true);
            MoveTowardsPlayer();
        }
        else
        {
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
        if (!isWaiting)
        {
            Transform targetPos = isFacingRight ? posB : posA;
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPos.position) < 0.5f)
            {
                StartCoroutine(WaitBeforeMoving());
            }
        }
    }

    private IEnumerator WaitBeforeMoving()
    {
        isWaiting = true;
        ani.SetBool("isRun", false);
        yield return new WaitForSeconds(waitBeforeMove);
        ani.SetBool("isRun", true);
        isWaiting = false;
        Flip();
    }
}
