using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    public Transform[] spawnpos;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnpos.Length; i++)
        {
            if (Random.Range(0, 2) == 0)
                spawnpos[i].gameObject.GetComponent<SpawnPos>().Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
