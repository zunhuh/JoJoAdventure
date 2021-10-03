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
        }
    }

    // add charic
    public Charic2D Charic_add(int _id, CharicType _type, string _resource)
    {
        //스크립트 Charic2D 추가된 GameObject 불러오기
        GameObject go = GameObject_from_prefab(_resource);  //"Prefabs/" + _resource
        Charic2D kCharic;

        switch ((CharicType)_type)
        {
            case CharicType.Hero:
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
    // GameObject에 prefab을 로드
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

    //find
    public Charic2D Charic_find(int _id)
    {
        for (int i = 0; i < kCharicList.Count; i++)
        {
            Charic2D obj = (Charic2D)kCharicList[i];
            if (obj == null) continue;  //캐릭 사라지는 경우.

            if (obj.ID == _id)
            {
                return obj;
            }
        }
        return null;
    }
}


