using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject target;       //Å¸°Ù    
    Rigidbody2D rigidbody2D;    
    Vector2 moveDir = Vector2.zero;
    float mspeed = 8.0f;

    public void Setup( GameObject target )
    {
        this.target = target;
        moveDir = GetDir2D(target.transform.position, transform.position);
        transform.right = moveDir;
    }

    void Start()
    {   
        rigidbody2D = GetComponent<Rigidbody2D>();
        //Destroy(this.gameObject, 5);
       
      
    }
        
    void Update()
    {
        Move();
    }
    void Move()
    {
        rigidbody2D.position += moveDir * mspeed * Time.deltaTime;
        
    }

    public Vector2 GetDir2D(Vector3 target, Vector3 source)
    {
        Vector3 dir = (target - source).normalized;
        return new Vector2(dir.x, dir.y);
    }
    public void OnTriggerEnter2D(Collider2D collider)
    { 
        if (collider.name != "Hero")
        {
            Destroy(this.gameObject);
        }
    }
}
