using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : BaseEnemy
{
    public GameObject bullet;
    public Transform firePoint;
    public float destroyTime = 5f;

    private void Update()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            if (Time.time >= lastDamageTime + damageDelay)
            {
                ani.SetTrigger("Hit");
                lastDamageTime = Time.time;
            }
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

    private void Shoot()
    {
        Vector3 direction = transform.localScale.x < 0 ? Vector3.right : Vector3.left;

        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        EnemyBullet enemyBulletScript = newBullet.GetComponent<EnemyBullet>();
        if (enemyBulletScript != null)
        {
            enemyBulletScript.SetDirection(direction, destroyTime);
        }
    }
}
