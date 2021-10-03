using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charic2D
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

        //행동패턴
        Act_start(Charic2D.eAct.idle);
        fAttackTime = Time.time;

        bActive = true;
    }

    //--------------------------------------------------------------------------
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

    public override void Act_update()
    {
        switch (act_cur)
        {
            case eAct.appear:
                break;
            case eAct.disappear:
                break;
            case eAct.idle:
                if (target == null) break;
                if (GetDistrance2D(transform.position, target.transform.position) < 3)
                    Act_start(eAct.run);
                break;
            case eAct.run:
                if (target == null) { Act_start(eAct.idle); break; }
                if (GetDistrance2D(transform.position, target.transform.position) > 6)
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
}
