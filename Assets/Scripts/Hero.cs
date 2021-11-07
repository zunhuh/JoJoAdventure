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
    public override int Act_start(eAct _act, Hashtable args = null, bool _force = true)
    {
        ////동일 액션 회피 ---------------------------------
        //if (!_force) if (_act == act_cur) return -1;

        ////액션 우선순위 체크 ------------------------------
        //switch (_act)
        //{
        //    case eAct.run:
        //    case eAct.attack:
        //        if (act_cur == eAct.die) return -1;
        //        if (act_cur == eAct.disappear) return -1;
        //        break;
        //    case eAct.hit:
        //        if (act_cur == eAct.attack) return -1; // 공격중에는 데미지만 처리.
        //        if (act_cur == eAct.die) return -1;
        //        if (act_cur == eAct.disappear) return -1;
        //        break;
        //    case eAct.die:
        //        if (act_cur == eAct.disappear) return -1;
        //        break;
        //}

        //액션 변경 --------------------------------------		
        act_old = act_cur;
        act_cur = _act;

        switch (act_cur)
        {
            case eAct.appear:
                break;
            case eAct.disappear:
                break;
            case eAct.idle:
                break;
            case eAct.run:
                break;
            case eAct.attack:
                break;
            case eAct.hit:
                hp_cur -= 50;
                power = 2;
                powerDir = GetDir2D(target.transform.position, transform.position);
                fAttackTime = Time.time + 0.5f;
                break;
            case eAct.die:
                break;
        }
        return 0;
    }

    public override void Act_update()
    {
        switch (act_cur)
        {
            case eAct.appear:
                break;
            //매니저에서 삭제
            case eAct.disappear:
                break;
            case eAct.idle:
                break;
            case eAct.run:
               break;
            case eAct.attack:
                break;
            case eAct.hit:
                Knockback();
                if (Time.time > fAttackTime)
                {
                    if (hp_cur <= 0) Act_start(eAct.die);
                    Act_start(eAct.idle);

                }
                break;
            case eAct.die:
                break;
        }
    }
    
    public SpriteRenderer c_spriteRenderer;
    public GameObject bullet;
    public GameObject firepos;
    bool fire_check = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        MoveSpeed = 4;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Act_update();
        HeroMove();
        
        if (Input.GetKey(KeyCode.Space) && fire_check == false)
        {
            StartCoroutine(Fire(ap_cur, aspeed));

        }

    }
    IEnumerator Fire(int p, float t)   //총알발사
    {
        Charic2D target = Charic2DManager.Instance.Charic_find_enemy(transform);

        if (target != null)
        {
            fire_check = true;

            GameObject go = GameObject.Instantiate(bullet);
            go.transform.position = firepos.transform.position;
            go.GetComponent<Bullet>().Setup(target.kGO);
            

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
