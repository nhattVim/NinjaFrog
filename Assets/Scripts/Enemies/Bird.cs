using System.Collections;
using UnityEngine;

public class Bird : BaseEnemy
{
    private bool isChasing = false;
    private bool isWaitingToChase = false;
    private bool isAttacking = false;
    private Vector3 startPosition;
    private Vector3 randomFlyAwayPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            if (!isChasing && !isWaitingToChase && !isAttacking)
            {
                StartCoroutine(StartChasingAfterDelay(2f));
            }
        }
    }

    private IEnumerator StartChasingAfterDelay(float delay)
    {
        isWaitingToChase = true;
        yield return new WaitForSeconds(delay);
        isChasing = true;
        isWaitingToChase = false;
        StartCoroutine(AttackAndFlyAway());
    }

    private IEnumerator AttackAndFlyAway()
    {
        while (Vector2.Distance(transform.position, playerTransform.position) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            FlipTowardsPlayer();
            yield return null;
        }

        isAttacking = true;
        if (Time.time >= lastDamageTime + damageDelay)
        {
            playerScript = playerTransform.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(atk);
                Debug.Log("Enemy dealt damage: " + atk);
                lastDamageTime = Time.time;
            }
        }

        yield return new WaitForSeconds(1f);

        randomFlyAwayPosition = new Vector3(
            transform.position.x + Random.Range(5f, 10f) * (Random.Range(0, 2) == 0 ? -1 : 1),
            transform.position.y + Random.Range(1f, 3f) * (Random.Range(0, 2) == 0 ? -1 : 1),
            transform.position.z
        );

        while (Vector2.Distance(transform.position, randomFlyAwayPosition) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, randomFlyAwayPosition, speed * Time.deltaTime);
            FlipTowardsPlayer();
            yield return null;
        }

        isChasing = false;
        isAttacking = false;
    }

    private void FlipTowardsPlayer()
    {
        if (playerTransform != null)
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
    }
}
