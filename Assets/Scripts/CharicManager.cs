using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicManager : MonoBehaviour
{
    List<Charic> list = new List<Charic>();
    class Charic
    {
        public int id;
        public enum eType
        {
            None,
            Hero = 1,
            Enemy = 2,
            Boss = 3,
        };
        public eType type;
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Charic Charic_add(int _id, string _resource, Charic.eType _type)
    {   
        GameObject go = GameObject_from_prefab
        Charic aCharic = new Charic(); 
    }
    


    }
}
