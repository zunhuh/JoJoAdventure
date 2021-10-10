using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    public Transform[] spawnpos;
    public GameObject hero;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, 3);
    }

    // Update is called once per frame
    void Update()
    {

        Charic2DManager.Instance.Charics_update();
        
    }
    public void Spawn()
    {
        int rnd = Random.Range(0, spawnpos.Length);
        Instantiate(spawnpos[rnd], transform);

        Enemy enemy;
        enemy = (Enemy)Charic2DManager.Instance.Charic_add(0, CharicType.Enemy, "Enemy");
        enemy.target = hero;
        enemy.Charic_init();
    }

}
