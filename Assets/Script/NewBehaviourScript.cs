using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ҫ����Ϸ����İ�ť��ǩ����
public class NewBehaviourScript : MonoBehaviour
{
    //�������ֱ�ǩ
    public Text Text_NumYouHave, Text_GiveYouJingyanping, Text_LevelView;
    public Text Text_DatetimeBetween, Text_DatetimeFront, Text_DatetimeAfter;
    //���Ӿ�������ѵı���
    int Jingyan, Levelnum, JingyanNeed;
    //int escapeTimes = 1;
    long DatetimeFront, DatetimeAfter, DatetimeBetween ;
    long Jingyanping;
    long OfflineSec;


    //��ť�ķ���
    public void Button_AddExp_F()
    {
        if (Jingyanping > 0)
        {
            Jingyanping--;
            Text_GiveYouJingyanping.text = "�Զ��ַ�����ǰ��ӵ�о���ƿ" + Jingyanping;
            Jingyan = Jingyan + 1;
            Text_NumYouHave.text = "�����1������ƿ����ǰ����" + Jingyan;
            Levelup_F();
        }
        else{
            Text_NumYouHave.text = "����ƿ���㣬���Ժ�";
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
    //����Ϸ����ʾ��Ϣ
        Text_GiveYouJingyanping.text = "�Զ��ַ�����ǰ��ӵ�о���ƿ" + Jingyanping;
        Text_LevelView.text = "�ȼ���" + Levelnum + "(" + Jingyan +"/" + JingyanNeed +")";
        Text_NumYouHave.text = "������";
    }

    void Time_F()
    {
        Jingyanping = Jingyanping + 1;
        Text_GiveYouJingyanping.text = "�Զ��ַ�����ǰ��ӵ�о���ƿ" + Jingyanping;
    }

    //�˳���ť
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
            //������ʾ
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

   
    //����Ͷ�ȡ����
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
    //�ȼ�������ʾ
    void Levelup_F()
    {
        if (Jingyan >= JingyanNeed)
        {
            Levelnum++;
            Jingyan = Jingyan - JingyanNeed;
            JingyanNeed = JingyanNeed * 2;
            Text_LevelView.text = "�ȼ���" + Levelnum + "(" + Jingyan + "/" + JingyanNeed + ")";
        }
        else
        {
            Text_LevelView.text = "�ȼ���" + Levelnum + "(" + Jingyan + "/" + JingyanNeed + ")";
        }
    }

    //���ߺ�Ľ�������
    void OfflineRewards()
    {
        //�Ѳ����ߵ�ʱ��ӵ�����ƿ��ȥ
        Jingyanping = Jingyanping + OfflineSec;
        
        
    }

    void SaveDatetimeFront()
    {
        DateTime _time = DateTime.Now;
        TimeSpan timeSpan = _time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        DatetimeFront = Convert.ToInt64(timeSpan.TotalSeconds);
        //Text_DatetimeFront.text = "�ϴ�����ʱ�䣺" + DatetimeFront;
        //string save = System.Convert.ToString(DatetimeFront);
        PlayerPrefs.SetString("DatetimeFront", Convert.ToString(DatetimeFront));
        PlayerPrefs.Save();

    }

    void GetDatetimeFront()
    {
        string DatetimeFront = PlayerPrefs.GetString("DatetimeFront", "1533521300");
        Text_DatetimeFront.text = "�ϴ�����ʱ�䣺" + Convert.ToInt64(DatetimeFront);
    }

    void GetDatetimeAfter()
    {
        DateTime _time = DateTime.Now;
        TimeSpan timeSpan = _time.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        DatetimeAfter = Convert.ToInt64(timeSpan.TotalSeconds);
        Text_DatetimeAfter.text = "���ε�¼ʱ�䣺" + Convert.ToInt64(DatetimeAfter);
        PlayerPrefs.SetString("DatetimeAfter", System.Convert.ToString(DatetimeAfter));
        PlayerPrefs.Save();
    }

    void ViewDatetimeBetween()
    {
        string DatetimeFront = PlayerPrefs.GetString("DatetimeFront", "1533521300");
        string DatetimeAfter = PlayerPrefs.GetString("DatetimeAfter", "1533521300");
        long DatetimeBetween = Convert.ToInt64(DatetimeAfter) - Convert.ToInt64(DatetimeFront);
        Text_DatetimeBetween.text = "������" + Convert.ToInt64(DatetimeBetween);
        OfflineSec = Convert.ToInt64(DatetimeBetween);
    }

}
