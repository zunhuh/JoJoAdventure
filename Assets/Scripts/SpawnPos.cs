using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    public GameObject[] enemys;

    void Start()
    {        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        int rnd = Random.Range(0, enemys.Length);
        Instantiate(enemys[rnd], transform);
    }
}
