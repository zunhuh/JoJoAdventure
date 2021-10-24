using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

// Type ------------------------------------
public enum CharicType
{
    None,
    Hero = 1,
    Enemy = 2,
    Boss = 3,
};

public class Charic2D : MonoBehaviour
{
    public int  ID = -1;			// unity GetInstanceID();
    public bool	bActive = false;    // 활성여부, false 면 제거.
    public CharicType kType;      // 캐릭터 종류
    public GameObject kGO;          // GameObject

    public Rigidbody2D      rigid2D;

    public SpriteRenderer   spriteRenderer;
    public Animator         animator;
        

    // move -----------------------------------
    public float            MoveSpeed = 0;  


    // Act -------------------------------------
    public enum eAct // animation + transform + state
    {
        appear,     // create, pos
        disappear,  // delete
        idle,
        run,
        attack,   // target
        hit, 
        die
    };
    public eAct act_cur;           //액션
    public eAct act_old;
    public float act_time = 0.0F;   //액션 시간.

    // Ability ---------------------------------
    public int hp_cur = 0;          //생명력
    public int hp_max = 0;          //
    public int ap_cur = 0;          //공격력
    public int ap_max = 0;          //
    public int dp_cur = 0;          //방어력
    public int dp_max = 0;          //
    public float aspeed = 0;        //공속

    protected float fAttackTime = 0;          //공격간격 제어.

    // target charic
    public Charic2D target_charic = null;
    public GameObject target = null;       // target GameObject   

    
    public delegate void Callback_charic(Hashtable _data);
	public Callback_charic OnCallback_charic;   //외부에서 이벤트 처리

    string anim_cur = "idle"; //애니메이션제어
    string anim_old = "";

    //--------------------------------------------------------------
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    public virtual void Charic_init()   //CharicManager
    {
        //data table 
        hp_cur = 100;           //생명력
        hp_max = 100;           //
        ap_cur = 30;            //공격력
        ap_max = 30;            //
        dp_cur = 0;             //방어력
        dp_max = 0;             //
        aspeed = 1.0f;          //공속

        //행동패턴
        Act_start(Charic2D.eAct.idle);
        fAttackTime = Time.time;

        bActive = true;
    }

    public virtual void Charic_update() //CharicManager
    {
        if (!bActive) return;
        Act_update();
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        if ( act_cur == eAct.run) Move();
    }

    void Move()
    {
        Vector2 moveDir = Vector2.zero;
        if (target != null)
        {
            Vector3 dir = GetDir2D(target.transform.position, transform.position );
            moveDir = new Vector2(dir.x, dir.y);
        }
        rigid2D.position += moveDir * MoveSpeed * Time.deltaTime;
    }

    //-----------------------------------------------------------------------------------------
    public void Animation_set(string anim)
    {
        //if (curAnimation != null) curAnimation.Play(_name);

        anim_cur = anim;
        if (anim_old == anim_cur) return;

        animator.Play(anim_cur);
    }


    //1. 캐릭터 액션 함수 호출하기    //  _charic.Act_start(Charic.eAct.appear);
    //2. 캐릭터 액션 함수 호출 (Hashtable 사용)    //  Hashtable hash = new Hashtable(); hash.Add("time", 1.0f); _charic.Act_start(Charic.eAct.appear, hash);
    //3. Hashtable 사용     //  float time = (float)args["time"];

    //--------------------------------------------------------------------------
    public virtual int Act_start(eAct _act, Hashtable args = null, bool _force = true)
    {
        //동일 액션 회피 ---------------------------------
        if (!_force) if (_act == act_cur) return -1;

        //액션 우선순위 체크 ------------------------------
        switch (_act)
        {
            case eAct.run:
            case eAct.attack:
                if (act_cur == eAct.die) return -1;
                if (act_cur == eAct.disappear) return -1;
                break;
            case eAct.hit:
                if (act_cur == eAct.attack) return -1; // 공격중에는 데미지만 처리.
                if (act_cur == eAct.die) return -1;
                if (act_cur == eAct.disappear) return -1;
                break;
            case eAct.die:
                if (act_cur == eAct.disappear) return -1;
                break;
        }

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
                MoveSpeed = 0;
                break;
            case eAct.run:
                MoveSpeed = 2.0f;
                break;
            case eAct.attack:
                MoveSpeed = 0;
                break;
            case eAct.hit:
                MoveSpeed = 0;
                break;
            case eAct.die:
                MoveSpeed = 0;
                break;
        }
        return 0;
    }

    public virtual void Act_update()
    {
        switch (act_cur)
        {
            case eAct.appear:
                break;
            case eAct.disappear:
                break;
            case eAct.idle:
                if (target == null) break;
                if (GetDistrance2D(target.transform.position, transform.position) < 3)
                Act_start(eAct.run);
                break;
            case eAct.run:
                if (target == null) { Act_start(eAct.idle); break; }
                if (GetDistrance2D(target.transform.position, transform.position) > 6)
                Act_start(eAct.idle);
                break;
            case eAct.attack:
                break;
            case eAct.hit:
                break;
            case eAct.die:
                break;
        }
    }

    //-----------------------------------------------------------------------------
    public Vector2 GetDir2D(Vector3 target, Vector3 source)
    {
        Vector3 dir = (target - source).normalized;
        return new Vector2(dir.x, dir.y);
    }

    public float GetDistrance2D(Vector3 target, Vector3 source)
    {
        Vector2 tpos = new Vector2(target.x, target.y);
        Vector2 spos = new Vector2(source.x, source.y);        
        return Vector2.Distance(tpos, spos);
    }

    //-----------------------------------------------------------------------------
    public bool IsEnemy()
    {
        if (kType == CharicType.Enemy) return true;
        return false;
    }
    public bool IsIdle()
    {
        if (act_cur == eAct.idle) return true;
        return false;
    }
    public bool IsDie()
    {
        if (act_cur == eAct.die || act_cur == eAct.disappear)
        {
            return true;
        }
        return false;
    }
}
