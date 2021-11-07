using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayScene : MonoBehaviour
{
    public Transform[] spawnpos;
    public GameObject hero;
    Hero heroSc;
    public int killCount = 0;
    public CinemachineVirtualCamera cmVcam;
    public GameObject gameclearPanel;
    public GameObject gameoverPanel;
    bool isplaying = false;
    bool isgameclear = true;
    bool isgameover = true;

    // Start is called before the first frame update
    void Start()
    {
        Charic2DManager.Instance.kCharicList.Clear();
        heroSc = (Hero)Charic2DManager.Instance.Charic_add(0, CharicType.Hero, "Hero");
        heroSc.Charic_init();
        hero = heroSc.kGO;
        hero.transform.position = new Vector3(0,0,0);
        cmVcam.Follow = hero.transform;

        InvokeRepeating("Spawn", 0, 3);

        isplaying = true;
        isgameclear = false;
        isgameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isplaying) return;

        Charic2DManager.Instance.Charics_update();
        GameClear();
        GameOver();
        
    }
    public void Spawn()
        
    {
        if (!isplaying) return;
        int rnd = Random.Range(0, spawnpos.Length);
        //print(rnd);
        //print(spawnpos[rnd].name);
        Enemy enemy;
        enemy = (Enemy)Charic2DManager.Instance.Charic_add(1, CharicType.Enemy, "Enemy");
        enemy.target = hero;
        enemy.transform.position = spawnpos[rnd].transform.position;
        enemy.Charic_init();
    }
    //게임오버
    public void GameOver()
    {   if (isgameover || isgameclear) return;
        if (heroSc.hp_cur <= 0)
        {
            print("GameOver");
            gameoverPanel.SetActive(true);
            Charic2DManager.Instance.MonsterRemove();
            isgameover = true;
            isplaying = false;
           
        }
    }
    //게임클리어
    public void GameClear()
    {
        if (isgameover || isgameclear) return;
        if (killCount >= 10)
        {
            print("GameClear");  //todo UI
            gameclearPanel.SetActive(true);
            Charic2DManager.Instance.MonsterRemove();
            isgameclear = true;
            isplaying = false;
        }
    }
    public void OnClickScenePlay()
    {
        SceneManager.LoadScene(1);
    } 
    
}
