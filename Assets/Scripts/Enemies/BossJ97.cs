using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BossJ97 : BaseEnemy
{
    // public GameObject HPPotion;
    // public GameObject ManaPotion;
    public float HPPotionTimeSpawn;
    public float ManaPotionTimeSpawn;
    private float hpPotionSpawnTimer = 0f;
    private float manaPotionSpawnTimer = 0f;

    [Header("Skill List")]
    public GameObject[] skillList;

    [Header("Movement Area")]
    public Vector3 areaCenter;
    public Vector3 areaSize;

    public float startAfterDelay = 5f;
    private Vector3 targetPosition;
    private bool canUseSkill = false;
    private bool isDead = false;
    private bool rage = false;
    private bool hasRage = false;
    private float maxHP;

    void Start()
    {
        HPPotion = GetComponent<BaseEnemy>().HPPotion;
        ManaPotion = GetComponent<BaseEnemy>().ManaPotion;

        maxHP = HP;
        if (skillList == null || skillList.Length == 0)
        {
            Debug.LogError("Skill list is empty or null. Please assign skills in the Inspector.");
            return;
        }
        else
        {
            foreach (GameObject skill in skillList)
            {
                if (skill != null)
                {
                    skill.SetActive(false);
                }
            }
        }
    }

    void Update()
    {
        if (rage || isDead) return;

        if (!canUseSkill)
        {
            startAfterDelay -= Time.deltaTime;
            if (startAfterDelay <= 0)
            {
                canUseSkill = true;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }

        if (HP < maxHP / 2 && !hasRage)
        {
            StartCoroutine(EnrageMode());
        }

        if (canUseSkill)
        {
            if (hasRage)
            {
                UseRandomSkill(3);
            }
            else
            {
                UseRandomSkill(1);
            }
        }

        hpPotionSpawnTimer -= Time.deltaTime;
        manaPotionSpawnTimer -= Time.deltaTime;

        if (hpPotionSpawnTimer <= 0f)
        {
            SpawnPotion(HPPotion);
            hpPotionSpawnTimer = HPPotionTimeSpawn;
        }

        if (manaPotionSpawnTimer <= 0f)
        {
            SpawnPotion(ManaPotion);
            manaPotionSpawnTimer = ManaPotionTimeSpawn;
        }
    }

    private void SetRandomTargetPosition()
    {
        Bounds movementBounds = new Bounds(areaCenter, areaSize);

        targetPosition = new Vector3(
            Random.Range(movementBounds.min.x, movementBounds.max.x),
            Random.Range(movementBounds.min.y, movementBounds.max.y),
            transform.position.z
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawCube(areaCenter, areaSize);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }

    private IEnumerator EnrageMode()
    {
        rage = true;
        hasRage = true;
        GetComponent<Collider2D>().enabled = false;
        ani.SetBool("Rage", true);
        yield return new WaitForSeconds(2f);
        HP = maxHP;
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = true;
        rage = false;
    }

    private void SpawnPotion(GameObject potionPrefab)
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2),
            Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2),
            areaCenter.z
        );

        Instantiate(potionPrefab, spawnPosition, Quaternion.identity);
    }

    private void UseRandomSkill(float skillRate)
    {
        if (Random.Range(0, 100) < skillRate)
        {
            int randomIndex = Random.Range(0, skillList.Length);
            switch (randomIndex)
            {
                case 0:
                    StartCoroutine(UseSkill1());
                    break;
                case 1:
                    StartCoroutine(UseSkill2());
                    break;
                case 2:
                    StartCoroutine(UseSkill3());
                    break;
                case 3:
                    StartCoroutine(UseSkill4());
                    break;
                default:
                    Debug.LogWarning("Không tìm thấy skill với index này.");
                    break;
            }
        }
    }

    private IEnumerator UseSkill1()
    {
        GameObject skill = skillList.FirstOrDefault(s => s != null && s.name == "Skill1");

        if (skill == null || playerTransform == null)
        {
            Debug.LogWarning("Skill1 hoặc playerTransform không được thiết lập.");
            yield break;
        }

        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Vector3 spawnPosition = playerTransform.position;

            spawnPosition.y = GetGroundY(spawnPosition.x) + 2f;

            GameObject skillInstance = Instantiate(skill, spawnPosition, Quaternion.identity);
            skillInstance.transform.localScale = new Vector3(1, 1, 1);

            skillInstance.SetActive(true);
            ani.SetTrigger("Skill1");

            yield return new WaitForSeconds(0.5f);
            Animator animator = skillInstance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("End");
            }

            Destroy(skillInstance, 0.5f);
        }
    }

    private float GetGroundY(float x)
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(x, 10f), Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return hit.point.y;
        }
        else
        {
            Debug.LogWarning("Skill 1 không tìm thấy mặt đất!");
            return 0f;
        }
    }

    private IEnumerator UseSkill2()
    {
        GameObject skill = skillList.FirstOrDefault(s => s != null && s.name == "Skill2");

        if (skill == null || playerTransform == null)
        {
            Debug.LogWarning("Skill2 hoặc playerTransform không được thiết lập.");
            yield break;
        }

        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Vector3 spawnPosition = transform.position;
            GameObject skillInstance = Instantiate(skill, spawnPosition, Quaternion.identity);
            skillInstance.transform.localScale = Vector3.one;
            skillInstance.SetActive(true);
            ani.SetTrigger("Skill2");

            Vector2 direction = (playerTransform.position - skillInstance.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            skillInstance.transform.rotation = Quaternion.Euler(0, 0, angle);

            Rigidbody2D rb = skillInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float speed = 10f;
                rb.velocity = direction * speed;
            }

            float maxLifetime = 5f;
            float maxDistance = 15f;
            float elapsedTime = 0f;

            while (skillInstance != null)
            {
                if (Vector2.Distance(skillInstance.transform.position, playerTransform.position) <= 0.5f)
                {
                    Animator animator = skillInstance.GetComponent<Animator>();
                    if (animator != null)
                    {
                        animator.SetTrigger("End");
                        float destroyDelay = animator.GetCurrentAnimatorStateInfo(0).length;
                        Destroy(skillInstance, destroyDelay);
                    }
                    else
                    {
                        Destroy(skillInstance, 0.5f);
                    }
                    yield break;
                }

                elapsedTime += Time.deltaTime;
                if (elapsedTime >= maxLifetime || Vector2.Distance(skillInstance.transform.position, transform.position) > maxDistance)
                {
                    Destroy(skillInstance);
                    yield break;
                }

                yield return null;
            }
        }
    }

    private IEnumerator UseSkill3()
    {
        GameObject skill = skillList.FirstOrDefault(s => s != null && s.name == "Skill3");

        if (skill == null || playerTransform == null)
        {
            Debug.LogWarning("Skill3 hoặc playerTransform không được thiết lập.");
            yield break;
        }

        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Vector3 spawnPosition = playerTransform.position + new Vector3(0, 1f, 0);

            GameObject skillInstance = Instantiate(skill, spawnPosition, Quaternion.identity);
            skillInstance.transform.localScale = new Vector3(1, 1, 1);

            skillInstance.SetActive(true);
            ani.SetTrigger("Skill3");

            yield return new WaitForSeconds(0.5f);
            Animator animator = skillInstance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("End");
            }

            Destroy(skillInstance, 0.5f);
        }

        yield return new WaitForSeconds(0.5f);
    }

    private IEnumerator UseSkill4()
    {
        GameObject skill = skillList.FirstOrDefault(s => s != null && s.name == "Skill4");

        if (skill == null || playerTransform == null)
        {
            Debug.LogWarning("Skill4 hoặc playerTransform không được thiết lập.");
            yield break;
        }

        if (Vector2.Distance(transform.position, playerTransform.position) <= detectionRadius)
        {
            Vector3 spawnPosition = playerTransform.position + new Vector3(0, 1f, 0);

            spawnPosition.y = GetGroundY(spawnPosition.x) + 2f;

            GameObject skillInstance = Instantiate(skill, spawnPosition, Quaternion.identity);
            skillInstance.transform.localScale = new Vector3(1, 1, 1);

            skillInstance.SetActive(true);
            ani.SetTrigger("Skill3");

            yield return new WaitForSeconds(0.5f);
            Animator animator = skillInstance.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("End");
            }

            Destroy(skillInstance, 0.5f);
        }

        yield return new WaitForSeconds(0.5f);
    }

    public override void TakeDamage(float damage)
    {
        if (!isDead)
        {
            HP -= damage;
            ani.SetTrigger("Hurt");
            if (HP <= 0)
            {
                Die();
            }
        }
    }

    protected override void Die()
    {
        Debug.Log("Enemy has died.");
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(DelayedFall());
    }

    private IEnumerator DelayedFall()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            if (Time.time >= lastDamageTime + damageDelay)
            {
                collision.gameObject.GetComponent<Player>()?.TakeDamage(atk);
                Debug.Log("Kẻ địch gây sát thương: " + atk);
                lastDamageTime = Time.time;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !isDead)
        {
            if (Time.time >= lastDamageTime + damageDelay)
            {
                coll.GetComponent<Player>()?.TakeDamage(atk);
                Debug.Log("Kẻ địch gây sát thương: " + atk);
                lastDamageTime = Time.time;
            }
        }
    }

}
