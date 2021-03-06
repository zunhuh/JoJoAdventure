using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Enemy enemy0;

enemy0 = (Enemy)Charic2DManager.Instance.Charic_add(0, CharicType.Enemy, "Enemy");
enemy0.target = hero;
enemy0.Charic_init();

Charic2DManager.Instance.Charics_update();
*/

public class Charic2DManager : MonoBehaviour
{
    public List<Charic2D> kCharicList = new List<Charic2D>();
    private List<Charic2D> kCharicDieList = new List<Charic2D>();
    public PlayScene playsc;

    int uid_seed = 0;

    //GameObject kRoot; 

    private static Charic2DManager s_Instance = null;
    public static Charic2DManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance //= new CharicManager();
                    = FindObjectOfType(typeof(Charic2DManager)) as Charic2DManager;
            }
            return s_Instance;
        }
    }

    void Awake()
    {
        
        if (s_Instance != null)
        {
            //Debug.LogError("Cannot have two instances of CharicManager.");
            return;
        }
        s_Instance = this;

        DontDestroyOnLoad(this);
        Debug.Log("CharicManager Awake");
    }

    void Start()
    {
        playsc = GameObject.Find("PlayScene").GetComponent<PlayScene>();
        //kRoot = GameObject.Find("root_charic");
        //Debug.Log("CharicManager Start");
    }

    void Update()
    {
        //if( Input.GetKeyDown(KeyCode.A) )
        //{	
        //Vector3 pos = new Vector3(2, 0, 0);
        //AddClone( 1 , pos );
        //}
    }

    // create     
    public Charic2DManager()
    {
        uid_seed = 100;
    }

    public void Charics_update()
    {
        foreach (Charic2D obj in kCharicList)
        {
            obj.Charic_update();
            if (obj.act_cur == Charic2D.eAct.disappear) kCharicDieList.Add(obj);
        }
        foreach (Charic2D obj in kCharicDieList)
        {
            playsc.killCount += 1;
            Charic_remove(obj);
            
        }
        kCharicDieList.Clear();

    }

        // add charic
        public Charic2D Charic_add(int _id, CharicType _type, string _resource)
    {
        //???????? Charic2D ?????? GameObject ????????
        GameObject go = GameObject_from_prefab(_resource);  //"Prefabs/" + _resource
        Charic2D kCharic;

        switch ((CharicType)_type)
        {
            case CharicType.Hero:

                {
                    kCharic = go.transform.GetComponent<Hero>();
                    kCharic.kGO = go;
                    kCharic.kGO.name = "charic_" + kCharic.ID;
                    kCharic.ID = _id;                           //id
                    kCharic.kType = (CharicType)_type;      //type
                }
                break;
            case CharicType.Enemy:
                {
                    kCharic = go.transform.GetComponent<Enemy>();
                    kCharic.kGO = go;
                    kCharic.kGO.name = "charic_" + kCharic.ID;
                    kCharic.ID = _id;                           //id
                    kCharic.kType = (CharicType)_type;      //type
                }
                break;
            case CharicType.Boss:
            default:
                {
                    kCharic = go.transform.GetComponent<Charic2D>(); //new Charic2D();
                    kCharic.kGO = go;
                    kCharic.kGO.name = "charic_" + kCharic.ID;
                    kCharic.ID = _id;                           //id
                    kCharic.kType = (CharicType)_type;      //type

                    //kCharic.kTable = CGameTable.Instance.Get_TableInfo_charic(_table_index);
                    //kCharic.kGO.transform.localScale = new Vector3(kCharic.kTable.scale, kCharic.kTable.scale, kCharic.kTable.scale);

                    //kCharic.kEC = (EffectController)kGO.AddComponent<EffectController>();
                    //kCharic.kEC.kGameObject = kCharic.gameObject;
                }
                break;
        }

       

        kCharicList.Add(kCharic);
        return kCharic;
    }
    // GameObject?? prefab?? ????
    public GameObject GameObject_from_prefab(string _prefab_name)
    {
        GameObject go = (GameObject)Instantiate(Resources.Load(_prefab_name, typeof(GameObject)));
        return go;
    }

    //remove
    public void Charic_remove(Charic2D _charic)
    {
        Destroy(_charic.kGO);
        kCharicList.Remove(_charic);
    }
    public void Charic_remove_all()
    {
        foreach (Charic2D obj in kCharicList) Destroy(obj.kGO);
        kCharicList.Clear();
    }
    public void MonsterRemove()
    {

        foreach (Charic2D obj in kCharicList)
            if (obj.kType != CharicType.Hero) Destroy(obj.gameObject);
    }

    //find
    public Charic2D Charic_find(int _id)
    {
        for (int i = 0; i < kCharicList.Count; i++)
        {
            Charic2D obj = (Charic2D)kCharicList[i];
            if (obj == null) continue;  //???? ???????? ????.

            if (obj.ID == _id)
            {
                return obj;
            }
        }
        return null;
    }

    public Charic2D Charic_find_enemy(Transform tr) //???? ?????? ????
    {
        Charic2D monster = null;
        float dist = 6;
        for (int i = 0; i < kCharicList.Count; i++)
        {
            Charic2D obj = (Charic2D)kCharicList[i];
            if (obj.kType == CharicType.Hero) continue;
            float dist_cur;
            dist_cur = Vector3.Distance(tr.position, obj.kGO.transform.position);
            if ( dist_cur < dist)
            {   
                dist = dist_cur;
                monster = obj;                
            }
        }
        
        return monster;
    }
}