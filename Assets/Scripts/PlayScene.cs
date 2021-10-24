using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    public Transform[] spawnpos;
    public GameObject hero;
    Hero heroSc;
    public int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        heroSc = (Hero)Charic2DManager.Instance.Charic_add(0, CharicType.Hero, "Hero");
        heroSc.Charic_init();
        hero = heroSc.kGO;
        hero.transform.position = new Vector3(0,0,0);
        

        InvokeRepeating("Spawn", 0, 3);        
    }

    // Update is called once per frame
    void Update()
    {

        Charic2DManager.Instance.Charics_update();
        GameClear();
        
    }
    public void Spawn()
    {
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
   // public void GameOver()
    //{
        //if (heroSc.hp_cur <= 0)
        //{
           // print("GameOver");
        //}
    //}
    //게임클리어
    public void GameClear()
    {
        if (killCount >= 10)
        {
            print("GameClear");  //todo UI
        }
    }
    
}
