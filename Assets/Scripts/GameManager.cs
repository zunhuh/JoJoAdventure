using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManager.Instance.Play();

public class GameManager : MonoBehaviour
{
    //SoundManager sound;

    //------------------------------------------------------------------------------------
    private static GameManager instance = null; //싱글톤
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                //instance = new GameManager(); //C#
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            //Debug.LogError("Cannot have two instances of Singletone.");
            return;
        }
        instance = this;

        //Screen.SetResolution(720, 1280, true);  //해상도 강제 세팅
        //Input.multiTouchEnabled = false;    //멀티터치 끄기
        //Screen.sleepTimeout = SleepTimeout.NeverSleep;

        DontDestroyOnLoad(this);    //씬전환할때 사라지지 않음
    }

    void Start()
    {
        GameInfo_load();
    }

    void OnDisable()
    {
        GameInfo_save();
    }

    public int stageNumber = 0;
    public int stageClear = 0;      //클리어한 스테이지 번호. 1,2,3,4,5

    public void GameInfo_load()
    {
        //string v = PlayerPrefs.GetString( "game_info", "default");
        stageClear = PlayerPrefs.GetInt("stageClear", 0);
    }
    public void GameInfo_save()
    {
        //PlayerPrefs.SetString("game_info", " ");
        PlayerPrefs.SetInt("stageClear", stageClear);
        PlayerPrefs.Save();  //By default Unity writes preferences to disk during OnApplicationQuit()
    }

/*
    public void PlaySound(eSound snd)
    {
        sound.PlaySound(snd);
    }
*/

}
