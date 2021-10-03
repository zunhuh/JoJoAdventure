using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject target;
    public float mspeed;
    protected Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    public enum eAct // animation + transform + state
    {
        appear,
        disappear,
        idle,
        run,
        attack,
        hit,
        die
    }
    public eAct kAct_cur;           //액션
    public eAct kAct_old;
    public float fAct_time = 0.0F;   //액션 시간.

    void Start()
    {
        Action_start(eAct.idle);
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
   

    // Update is called once per frame
    void Update()
    {
        Action_update();
           
        

    }
    public void FixedUpdate()
    {

        if (kAct_cur == eAct.run) Move(target);
    }

    public void Move(GameObject target)
    {
        Vector2 move = Vector2.zero;
        if (target != null)
        {
            Vector3 t = new Vector3(target.transform.position.x, target.transform.position.y, 0);
            Vector3 s = new Vector3(transform.position.x, transform.position.y, 0);
            Vector3 dir = (t - s).normalized;
            move = new Vector2(dir.x, dir.y);

        }
        rigidbody2D.position += new Vector2(move.x, move.y) * mspeed * Time.deltaTime;

    }
    public void Action_start(eAct act)
    {
        kAct_cur = act;

        switch (kAct_cur)
        {
            case eAct.appear:
                break;
            case eAct.disappear:
                break;
            case eAct.idle:
                if (target == null) target = GameObject.Find("Hero");
                mspeed = 0;
                break;
            case eAct.run:
                mspeed = 3.0f;
                break;
            case eAct.attack:
                mspeed = 0;
                break;
             case eAct.hit:
                mspeed = 0;
                break;
            case eAct.die:
                mspeed = 0;
                break;
        }
    }

    public void Action_update()
    {
        switch (kAct_cur)
        {
            case eAct.appear:
                break;
            case eAct.disappear:
                break;
            case eAct.idle:
                if (target == null) break;
                if (GetDistance2D(transform.position, target.transform.position) < 3)
                    Action_start(eAct.run);
                break;
            case eAct.run:
                if (target == null) {Action_start(eAct.idle); break; }
                if (GetDistance2D(transform.position, target.transform.position) > 10)
                    Action_start(eAct.idle);
                break;
            case eAct.attack:
                break;
            case eAct.hit:
            case eAct.die:
                break;
        }
    }
    public float GetDistance2D(Vector3 source, Vector3 target)
    {
        Vector2 tpos = new Vector2(target.x, target.y);
        Vector2 spos = new Vector2(source.x, source.y);
        return Vector2.Distance(tpos, spos);

    }
    

}
