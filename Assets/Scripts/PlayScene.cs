using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScene : MonoBehaviour
{
    public Transform[] spawnpos;

    Enemy enemy0;

    // Start is called before the first frame update
    void Start()
    {
        enemy0 = (Enemy)Charic2DManager.Instance.Charic_add(0, CharicType.Enemy, "Enemy");
        enemy0.target = null;
        enemy0.Charic_init();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
