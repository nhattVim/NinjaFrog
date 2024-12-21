using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [Header("HpBar")]
    public Image currentHPImage;
    public float smoothSpeed = 5;
    private float hptargetfillamount;

    [Header("ManaBar")]
    public Image currentManaImage;
    private float manaTargetFillAmount;

    private float currentHP;
    private float currentMana;
    private float maxHP;
    private float maxMana;

    private Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            currentHP = player.GetComponent<Player>().currentHP;
            currentMana = player.GetComponent<Player>().currentMana;
            maxHP = player.GetComponent<Player>().maxHP;
            maxMana = player.GetComponent<Player>().maxMana;

            hptargetfillamount = currentHP / maxHP;

            manaTargetFillAmount = currentMana / maxMana;

            if (currentHPImage != null)
            {
                currentHPImage.fillAmount = Mathf.Lerp(currentHPImage.fillAmount, hptargetfillamount, Time.deltaTime * smoothSpeed);
            }

            if (currentManaImage != null)
            {
                currentManaImage.fillAmount = manaTargetFillAmount;
            }
        }
    }
}
