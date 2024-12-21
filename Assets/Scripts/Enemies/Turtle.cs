using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : BaseEnemy
{
    public float detectionRadius1 = 10f;

    private void Update()
    {
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius1)
        {
            ani.SetBool("detec1", true);
        }
        else
        {
            ani.SetBool("detec1", false);
        }

        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            ani.SetBool("detec2", true);
        }
        else
        {
            ani.SetBool("detec2", false);
        }

        FlipTowardsPlayer();
    }

    private void FlipTowardsPlayer()
    {
        if (playerTransform.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (playerTransform.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    public override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius1);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
