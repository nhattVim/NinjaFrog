using System.Collections;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Requirement")]
    public GameObject dashEffectObj;
    private Rigidbody2D rb;
    private Animator ani;

    [HideInInspector]
    public float currentHP;
    [HideInInspector]
    public float maxHP;
    [HideInInspector]
    public float currentMana;
    [HideInInspector]
    public float maxMana;
    [HideInInspector]
    public int numOfCherries;

    [Header("Player stats")]
    public float dashForce = 40f;
    public float dashCooldown = 0.5f;
    public float moveSpeed;
    public float dashTime = 0.1f;
    private bool isDashing = false;
    private float currentDashCooldown = 0f;

    [Header("Hit")]
    public GameObject swordSlash;
    public GameObject sword;
    public float atk;
    public float attackSpeed = 0.5f;
    private Animator swordAni;
    private Animator slashAni;
    private bool canSwordAttack = true;

    [Header("Shooting")]
    public ObjectPool bulletPool;
    public Transform shootFirePoint;
    public float shootCD = 0.5f;
    private float lastShootTime;

    [Header("Skill E")]
    public Image EIcon;
    public GameObject EPrefab;
    public Transform EFirePoint;
    public float ECooldown = 5f;
    private float currentECooldown = 0f;

    [Header("Skill Q")]
    public Image QIcon;
    public GameObject QPrefab;
    public Transform QFirePoint;
    public float QCooldown = 5f;
    private float currentQCooldown = 0f;

    private AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        swordAni = sword.GetComponent<Animator>();
        slashAni = swordSlash.GetComponent<Animator>();
    }

    public void TakeDamage(float atk)
    {
        audioManager.PlaySFX(audioManager.takeDameClip);
        ani.SetTrigger("Hit");
        Variables.Object(gameObject).Set("currentHP", Mathf.Clamp(currentHP -= atk, 0, maxHP));
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Cherries"))
        {
            if (audioManager != null && audioManager.CherriesClip != null)
            {
                audioManager.PlaySFX(audioManager.CherriesClip);
            }
            else
            {
                Debug.LogWarning("AudioManager hoặc cherrisClip không được thiết lập.");
            }
            numOfCherries += 1;
            Variables.Object(gameObject).Set("numOfCherries", numOfCherries);
        }
    }

    void Update()
    {
        currentMana = (float)Variables.Object(gameObject).Get("currentMana");
        currentHP = (float)Variables.Object(gameObject).Get("currentHP");
        maxMana = (float)Variables.Object(gameObject).Get("maxMana");
        maxHP = (float)Variables.Object(gameObject).Get("maxHP");
        atk = (float)Variables.Object(gameObject).Get("atk");
        moveSpeed = (float)Variables.Object(gameObject).Get("moveSpeed");
        numOfCherries = (int)Variables.Object(gameObject).Get("numOfCherries");

        if (!isDashing)
        {
            float moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

            if (moveInput < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
            else if (moveInput > 0 && transform.localScale.x < 0)
            {
                Flip();
            }
        }

        if (currentDashCooldown > 0)
        {
            currentDashCooldown -= Time.deltaTime;
        }

        if (currentECooldown > 0)
        {
            currentECooldown -= Time.deltaTime;
            EIcon.fillAmount = currentECooldown / ECooldown;
        }

        if (currentQCooldown > 0)
        {
            currentQCooldown -= Time.deltaTime;
            QIcon.fillAmount = currentQCooldown / QCooldown;
        }

        if (Input.GetMouseButtonDown(0) && canSwordAttack)
        {
            StartCoroutine(Hit());
            audioManager.PlaySFX(audioManager.hitClip);
        }

        if (Input.GetMouseButtonDown(1) && Time.time > lastShootTime + shootCD)
        {
            audioManager.PlaySFX(audioManager.shurikenClip);
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && currentDashCooldown <= 0)
        {
            audioManager.PlaySFX(audioManager.dashClip);
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.E) && currentECooldown <= 0 && currentMana >= EPrefab.GetComponent<EnergyBlast>().manaCost)
        {
            SkillE();
        }

        if (Input.GetKeyDown(KeyCode.Q) && currentQCooldown <= 0 && currentMana >= QPrefab.GetComponent<Tornado>().manaCost)
        {
            SkillQ();
        }
    }

    IEnumerator Hit()
    {
        canSwordAttack = false;
        swordAni.SetTrigger("Hit");
        slashAni.SetTrigger("Hit");
        swordSlash.GetComponent<SwordSlash>().SetDamage(atk);
        yield return new WaitForSeconds(attackSpeed);
        canSwordAttack = true;
    }

    private void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - shootFirePoint.position).normalized;

        GameObject bullet = bulletPool.GetObject();
        bullet.transform.position = shootFirePoint.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction, bulletPool);
        }

        lastShootTime = Time.time;
    }


    private void SkillE()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - EFirePoint.position).normalized;

        GameObject skillInstance = Instantiate(EPrefab, EFirePoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        skillInstance.transform.rotation = Quaternion.Euler(0, 0, angle);

        EnergyBlast skillScript = skillInstance.GetComponent<EnergyBlast>();
        if (skillScript != null)
        {
            skillScript.SetDirection(direction);
        }

        Variables.Object(gameObject).Set("currentMana", currentMana -= skillScript.manaCost);
        Debug.Log($"Mana hiện tại: {currentMana}");

        currentECooldown = ECooldown;
    }

    private void SkillQ()
    {
        Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        GameObject skillInstance = Instantiate(QPrefab, QFirePoint.position + new Vector3(0f, 1f, 0f), Quaternion.identity);

        Tornado skillScript = skillInstance.GetComponent<Tornado>();
        if (skillScript != null)
        {
            skillScript.SetDirection(direction);
        }

        Variables.Object(gameObject).Set("currentMana", currentMana -= skillScript.manaCost);
        Debug.Log($"Mana hiện tại: {currentMana}");

        currentQCooldown = QCooldown;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        float moveDirection = transform.localScale.x > 0 ? 1f : -1f;
        rb.velocity = new Vector2(dashForce * moveDirection, rb.velocity.y);
        dashEffectObj.SetActive(true);

        yield return new WaitForSeconds(dashTime);

        rb.velocity = new Vector2(0f, rb.velocity.y);
        dashEffectObj.SetActive(false);

        isDashing = false;
        currentDashCooldown = dashCooldown;
    }
}
