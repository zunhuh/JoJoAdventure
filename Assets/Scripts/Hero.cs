using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Charic2D
{
    public override void Charic_init()   //CharicManager
    {
        //data table 
        hp_cur = 100;           //생명력
        hp_max = 100;           //
        ap_cur = 30;            //공격력
        ap_max = 30;            //
        dp_cur = 0;             //방어력
        dp_max = 0;             //
        aspeed = 1.0f;          //공속
    }
    public SpriteRenderer c_spriteRenderer;
    public GameObject bullet;
    public GameObject firepos;
    bool fire_check = false;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 4;
        
    }

    // Update is called once per frame
    void Update()
    {
        HeroMove();
        if (Input.GetKey(KeyCode.Space) && fire_check == false)
        {
            StartCoroutine(Fire(ap_cur, aspeed));

        }
    }
    IEnumerator Fire(int p, float t)   //총알발사
    {


        Charic2D mm = Charic2DManager.Instance.Charic_find_enemy(transform);
        if (mm != null)
        {
            fire_check = true;

            GameObject go = GameObject.Instantiate(bullet);
            go.transform.position = firepos.transform.position;
            go.GetComponent<Bullet>().Setup(mm.kGO);

            yield return new WaitForSeconds(aspeed);

            fire_check = false;
        }

    }
    private void HeroMove()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        if (move.x > 0.01f)
        {
            if (spriteRenderer.flipX == true)
                spriteRenderer.flipX = false;
            c_spriteRenderer.flipX = true;
        }
        else if (move.x < -0.01f)
        {
            if (spriteRenderer.flipX == false)
                spriteRenderer.flipX = true;
            c_spriteRenderer.flipX = false;
        }

        if (move.x == 0 && move.y == 0) Animation_set("idle");
        else Animation_set("walk");
        transform.transform.position += new Vector3(move.x, move.y, 0) * MoveSpeed * Time.deltaTime;
    }
}
