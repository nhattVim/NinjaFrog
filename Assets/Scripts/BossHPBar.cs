using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public Image currentHPImage;
    public TextMeshProUGUI currentHPText;
    public float smoothSpeed = 5;
    private float hptargetfillamount;
    private float currentHP;
    private float maxHP;
    private BossJ97 boss;
    private bool bossDied = false;

    void Start()
    {
        boss = FindAnyObjectByType<BossJ97>();
        if (boss != null)
        {
            maxHP = boss.GetComponent<BaseEnemy>().HP;
        }
    }

    void Update()
    {
        if (boss == null || bossDied == true) return;

        currentHP = boss.GetComponent<BaseEnemy>().HP;
        hptargetfillamount = currentHP / maxHP;

        if (currentHPImage != null)
        {
            currentHPImage.fillAmount = Mathf.Lerp(currentHPImage.fillAmount, hptargetfillamount, Time.deltaTime * smoothSpeed);
        }

        if (currentHPText != null)
        {
            currentHPText.text = $"{Mathf.CeilToInt(currentHP)} / {Mathf.CeilToInt(maxHP)}";
        }

        if (currentHP <= 0)
        {
            bossDied = true;
            gameObject.SetActive(false);
        }
    }
}
