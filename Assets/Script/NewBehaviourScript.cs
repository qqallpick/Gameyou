using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//要让游戏界面的按钮标签听话
public class NewBehaviourScript : MonoBehaviour
{
    //关联文字标签
    public Text Text_NumYouHave, Text_GiveYouJingyanping, Text_LevelView;
    public Text Text_DatetimeBetween, Text_DatetimeFront, Text_DatetimeAfter;
    //增加经验和葡萄的变量
    int Jingyan, Levelnum, JingyanNeed;
    //int escapeTimes = 1;
    long DatetimeFront, DatetimeAfter, DatetimeBetween ;
    long Jingyanping;
    long OfflineSec;


    //按钮的方法
    public void Button_AddExp_F()
    {
        if (Jingyanping > 0)
        {
            Jingyanping--;
            Text_GiveYouJingyanping.text = "自动分发：当前你拥有经验瓶" + Jingyanping;
            Jingyan = Jingyan + 1;
            Text_NumYouHave.text = "你吃了1个经验瓶，当前经验" + Jingyan;
            Levelup_F();
        }
        else{
            Text_NumYouHave.text = "经验瓶不足，等稍候";
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Load_F();
        GetDatetimeFront();
        GetDatetimeAfter();
        ViewDatetimeBetween();
        OfflineRewards();
        InvokeRepeating("Time_F", 0, 1f);
    //打开游戏就显示信息
        Text_GiveYouJingyanping.text = "自动分发：当前你拥有经验瓶" + Jingyanping;
        Text_LevelView.text = "等级：" + Levelnum + "(" + Jingyan +"/" + JingyanNeed +")";
        Text_NumYouHave.text = "修炼中";
    }

    void Time_F()
    {
        Jingyanping = Jingyanping + 1;
        Text_GiveYouJingyanping.text = "自动分发：当前你拥有经验瓶" + Jingyanping;
    }

    //退出按钮
    public void Button_QuitGame()
    {
        Save_F();
        SaveDatetimeFront();
        Application.Quit();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //弹出提示
            if (true)
            {
                Save_F();
                SaveDatetimeFront();
                Application.Quit();
            }
            else
            {

            }
        }
    }

    //IEnumerator resetTimes()
    //{
    //    yield return new WaitForSeconds(2);
    //    escapeTimes = 1;
    //}

   
    //保存和读取功能
    void Save_F()
    {
        PlayerPrefs.SetString("Jingyanping", Convert.ToString(Jingyanping));
        PlayerPrefs.SetInt("Jingyan", Jingyan);
        PlayerPrefs.SetInt("Levelnum", Levelnum);
        PlayerPrefs.SetInt("JingyanNeed", JingyanNeed);
        PlayerPrefs.Save();
    }
    void Load_F()
    {
        Jingyanping = Convert.ToInt64(PlayerPrefs.GetString("Jingyanping", "0"));
        Jingyan = PlayerPrefs.GetInt("Jingyan", 0);
        Levelnum = PlayerPrefs.GetInt("Levelnum", 1);
        JingyanNeed = PlayerPrefs.GetInt("JingyanNeed", 1);
    }
    //等级提升显示
    void Levelup_F()
    {
        if (Jingyan >= JingyanNeed)
        {
            Levelnum++;
            Jingyan = Jingyan - JingyanNeed;
            JingyanNeed = JingyanNeed * 2;
            Text_LevelView.text = "等级：" + Levelnum + "(" + Jingyan + "/" + JingyanNeed + ")";
        }
        else
        {
            Text_LevelView.text = "等级：" + Levelnum + "(" + Jingyan + "/" + JingyanNeed + ")";
        }
    }

    //离线后的奖励结算
    void OfflineRewards()
    {
        //把不在线的时间加到经验瓶里去
        Jingyanping = Jingyanping + OfflineSec;
        
        
    }

    void SaveDatetimeFront()
    {
        DateTime _time = DateTime.Now;
        TimeSpan timeSpan = _time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        DatetimeFront = Convert.ToInt64(timeSpan.TotalSeconds);
        //Text_DatetimeFront.text = "上次离线时间：" + DatetimeFront;
        //string save = System.Convert.ToString(DatetimeFront);
        PlayerPrefs.SetString("DatetimeFront", Convert.ToString(DatetimeFront));
        PlayerPrefs.Save();

    }

    void GetDatetimeFront()
    {
        string DatetimeFront = PlayerPrefs.GetString("DatetimeFront", "1533521300");
        Text_DatetimeFront.text = "上次离线时间：" + Convert.ToInt64(DatetimeFront);
    }

    void GetDatetimeAfter()
    {
        DateTime _time = DateTime.Now;
        TimeSpan timeSpan = _time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        DatetimeAfter = Convert.ToInt64(timeSpan.TotalSeconds);
        Text_DatetimeAfter.text = "本次登录时间：" + Convert.ToInt64(DatetimeAfter);
        PlayerPrefs.SetString("DatetimeAfter", System.Convert.ToString(DatetimeAfter));
        PlayerPrefs.Save();
    }

    void ViewDatetimeBetween()
    {
        string DatetimeFront = PlayerPrefs.GetString("DatetimeFront", "1533521300");
        string DatetimeAfter = PlayerPrefs.GetString("DatetimeAfter", "1533521300");
        long DatetimeBetween = Convert.ToInt64(DatetimeAfter) - Convert.ToInt64(DatetimeFront);
        Text_DatetimeBetween.text = "秒数相差：" + Convert.ToInt64(DatetimeBetween);
        OfflineSec = Convert.ToInt64(DatetimeBetween);
    }

}
