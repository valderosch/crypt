using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class MainScreen : MonoBehaviour
{
    [SerializeField] int money;
    public int[] costInt;
    private int clickValue = 1;
    public int[] passiveBonus;
    public int totalBonus;
    public GameObject ShopPanel;
    public GameObject SettingsPanel;
    public GameObject AchievementPanel;

    public Text[] CostText;
    public Text moneyCounter;
    public Text[] AchievementsText;
    public Text[] AchievementsCost;
    public Text Achievement1NameText;

    private Save save = new Save();


    [Header("Achievements")]
    private int Achievement1Max;
    private bool isAchievement1 = true;
   

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SAVE"))
        {
            save = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SAVE"));
            money = save.money;
            costInt = save.costInt;
            clickValue = save.clickValue;
            passiveBonus = save.passiveBonus;
            Achievement1Max = save.Achievement1Max;
            isAchievement1 = save.isAchievement1;

            for (int i = 0; i < 1; i++)
            {
                passiveBonus[i] = save.passiveBonus[i];
                totalBonus += save.passiveBonus[i];
            }

            for (int i = 0; i < 2; i++)
            {
                costInt[i] = save.costInt[i];
                CostText[i].text = save.costInt[i].ToString();
            }
        }
    }

    

    private void Start()
    {
        StartCoroutine(passiveEarning());

        DateTime dt = new DateTime(save.date[0], save.date[1], save.date[2], save.date[3], save.date[4], save.date[5]);
        TimeSpan ts = DateTime.Now - dt;
        money += (int)ts.TotalSeconds * totalBonus;
        print("EARNED: " + (int)ts.TotalSeconds * totalBonus);
    }   

    public void OnButtonClick()
    {
        money += clickValue;
        if (isAchievement1 == true)
        {
            Achievement1Max++;
        }

    }

    void Update()
    {
        moneyCounter.text = money.ToString();
    }

    public void ShopInteract()
    {
        ShopPanel.SetActive(!ShopPanel.activeSelf);
    }

    public void SettingsInteract()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);
    }

    public void AchiementsIneract()
    {
        AchievementPanel.SetActive(!AchievementPanel.activeSelf);
    }

    public void OnClickBuyLevel()
    {
        if(money >= costInt[0])
        {
            money -= costInt[0];
            costInt[0] *= 2;
            clickValue *= 2;
            CostText[0].text = costInt[0].ToString();
            print("Buyed level");
        }

    }

    public void OnClickBuyPassive()
    {
        if (money >= costInt[1])
        {
            money -= costInt[1];
            costInt[1] *= 2;
            passiveBonus[0] += 10;
            CostText[1].text = costInt[1].ToString();
            print("Buyed Passive");
        }

    }

    IEnumerator passiveEarning()
    {
        while (true)
        {
            money += passiveBonus[0];
            yield return new WaitForSeconds(1);
        }
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if(pause){
            save.money = money;
            save.clickValue = clickValue;
            save.passiveBonus = new int[1];
            save.costInt = new int[2];
            save.Achievement1Max = Achievement1Max;
            save.isAchievement1 = isAchievement1;

            for (int i = 0; i < 1; i++)
            {
                save.passiveBonus[i] = passiveBonus[i];
            }

            for (int i = 0; i < 2; i++)
            {
                save.costInt[i] = costInt[i];
            }
            save.date[0] = DateTime.Now.Year; save.date[1] = DateTime.Now.Month; save.date[2] = DateTime.Now.Day; save.date[3] = DateTime.Now.Hour; save.date[4] = DateTime.Now.Minute; save.date[5] = DateTime.Now.Second;
            PlayerPrefs.SetString("SAVE", JsonUtility.ToJson(save));
        }
    }
#else
    private void OnApplicationQuit()
    {
        save.money = money;
        save.clickValue = clickValue;
        save.passiveBonus = new int[1];
        save.costInt = new int[2];
        save.Achievement1Max = Achievement1Max;
        save.isAchievement1 = isAchievement1;

        for(int i = 0; i<1; i++)
        {
           save.passiveBonus[i] = passiveBonus[i];
        }

        for (int i = 0; i < 2; i++)
        {
            save.costInt[i] = costInt[i];
        }
        save.date[0] = DateTime.Now.Year; save.date[1] = DateTime.Now.Month; save.date[2] = DateTime.Now.Day; save.date[3] = DateTime.Now.Hour; save.date[4] = DateTime.Now.Minute; save.date[5] = DateTime.Now.Second; 
        PlayerPrefs.SetString("SAVE", JsonUtility.ToJson(save));
    }
#endif
    public void OnClickAchievement1Button()
    {
        if(isAchievement1 == true && Achievement1Max == 100)
        {
            money += 500;
            isAchievement1 = false;
        }
    }
}




[Serializable]
public class Save{

    public int money;
    public int clickValue;
    public int[] costInt;
    public int[] passiveBonus;
    public int[] date = new int[6];
    public int Achievement1Max;
    public bool isAchievement1;


}
