using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class IncreaseHpATKSpeed : MonoBehaviour
{
    public GameObject MenuManger;
    private List<Image> hpList = new List<Image>();
    private List<Image> atkList = new List<Image>();
    private List<Image> speedList = new List<Image>();
    private int HpUpdate;
    private int AtkUpdate;
    private int SpeedUpdate;
    private Player player;
    private GameObject playerObj;

    [SerializeField] private float additionDame;
    [SerializeField] private float additionHP;
    [SerializeField] private float additionSpeed;

    private void Start()
    {
        HpUpdate = (int)Variables.Object(MenuManger).Get("HpUpdate");
        AtkUpdate = (int)Variables.Object(MenuManger).Get("AtkUpdate");
        SpeedUpdate = (int)Variables.Object(MenuManger).Get("SpeedUpdate");

        player = FindObjectOfType<Player>();
        playerObj = GameObject.FindGameObjectWithTag("Player");

        Transform increaseHpBar = transform.Find("IncreaseMaxHPBar");
        Transform increaseATKBar = transform.Find("IncreaseMaxATKBar ");
        Transform increaseSpeedBar = transform.Find("IncreaseMaxSpeedBar ");

        PlaceImage(increaseHpBar, hpList);
        PlaceImage(increaseATKBar, atkList);
        PlaceImage(increaseSpeedBar, speedList);
    }

    void Update()
    {
        ReColor(hpList, HpUpdate, Color.red);
        ReColor(atkList, AtkUpdate, Color.cyan);
        ReColor(speedList, SpeedUpdate, Color.blue);
    }

    private void PlaceImage(Transform imageBar, List<Image> listImage)
    {
        if (imageBar != null)
        {
            foreach (Transform child in imageBar)
            {
                Image hpImage = child.GetComponent<Image>();
                if (hpImage != null)
                {
                    listImage.Add(hpImage);
                }
            }
        }
    }

    private void ReColor(List<Image> listImage, int levelUpdate, Color color)
    {
        if (levelUpdate > 0)
        {
            int maxIndex = Mathf.Min(levelUpdate, listImage.Count);

            for (int i = 0; i < maxIndex; i++)
            {
                listImage[i].color = Color.Lerp(color, Color.white, 0.5f);
            }

            if (maxIndex < listImage.Count)
            {
                listImage[maxIndex].color = Color.yellow;
            }
        }
    }

    public void OnLevelUpHP()
    {
        if (HpUpdate < hpList.Count)
        {
            if ((int)Variables.Object(playerObj).Get("numOfCherries") < 25)
            {
                Debug.Log("Không đủ cherries");
                return;
            }
            else
            {
                Variables.Object(playerObj).Set("numOfCherries", player.numOfCherries -= 25);
                Debug.Log("Nâng cấp thành công cherries trừ 25");
            }

            hpList[HpUpdate].color = Color.Lerp(Color.red, Color.white, 0.5f);

            Variables.Object(playerObj).Set("maxHP", player.maxHP += additionHP);
            Debug.Log("máu tối đa của player là: " + player.maxHP);

            if (HpUpdate + 1 < hpList.Count)
            {
                hpList[HpUpdate + 1].color = Color.yellow;
            }

            HpUpdate++;
            Variables.Object(MenuManger).Set("HpUpdate", HpUpdate);
        }
        else
        {
            Debug.Log("Đã Max cấp HP");
            // UpdateImageColors(Color.green);
        }
    }


    public void OnLevelUpATK()
    {
        if (AtkUpdate < atkList.Count)
        {
            if ((int)Variables.Object(playerObj).Get("numOfCherries") < 25)
            {
                Debug.Log("Không đủ cherries");
                return;
            }
            else
            {
                Variables.Object(playerObj).Set("numOfCherries", player.numOfCherries -= 25);
                Debug.Log("Nâng cấp thành công cherries trừ 25");
            }

            atkList[AtkUpdate].color = Color.Lerp(Color.cyan, Color.white, 0.5f);

            Variables.Object(playerObj).Set("atk", player.atk += additionDame);
            Debug.Log("Tấn công hiện tại là: " + player.atk);

            if (AtkUpdate + 1 < atkList.Count)
            {
                atkList[AtkUpdate + 1].color = Color.yellow;
            }

            AtkUpdate++;
            Variables.Object(MenuManger).Set("AtkUpdate", AtkUpdate);
        }
        else
        {
            Debug.Log("Đã Max cấp Atk");
            //  UpdateImageColors(Color.green);
        }
    }

    public void OnLevelUpSpeed()
    {
        if (SpeedUpdate < speedList.Count)
        {
            if ((int)Variables.Object(playerObj).Get("numOfCherries") < 25)
            {
                Debug.Log("Không đủ cherries");
                return;
            }
            else
            {
                Variables.Object(playerObj).Set("numOfCherries", player.numOfCherries -= 25);
                Debug.Log("Nâng cấp thành công cherries trừ 25");
            }

            speedList[SpeedUpdate].color = Color.Lerp(Color.blue, Color.white, 0.5f);

            Variables.Object(playerObj).Set("moveSpeed", player.moveSpeed += additionSpeed);
            Debug.Log("tốc độ là " + player.moveSpeed);

            if (SpeedUpdate + 1 < speedList.Count)
            {
                speedList[SpeedUpdate + 1].color = Color.yellow;
            }

            SpeedUpdate++;
            Variables.Object(MenuManger).Set("SpeedUpdate", SpeedUpdate);
        }
        else
        {
            Debug.Log("Đã Max cấp Speed");
            // UpdateImageColors(Color.green);
        }
    }
}
