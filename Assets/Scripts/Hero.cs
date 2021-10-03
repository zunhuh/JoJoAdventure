using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public SpriteRenderer p_spriteRenderer;
    public SpriteRenderer c_spriteRenderer;
    public Animator animator;
    public float movespeed;
    bool a;
    public GameObject player;
    public GameObject bullet;
    public GameObject firepos;
    public GameObject monster;

    string anim_cur = "idle";
    string anim_old ="";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            GameObject go = GameObject.Instantiate(bullet);            go.transform.position = firepos.transform.position;            go.GetComponent<Bullet>().Setup(monster);        }

    
}


    void SetAnimation(string anim)
    {
        anim_cur = anim;
        if (anim_old == anim_cur) return;

        animator.Play(anim_cur);
    }
    private void Move()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        if (move.x > 0.01f)
        {
            if (p_spriteRenderer.flipX == true)
                p_spriteRenderer.flipX = false;
            c_spriteRenderer.flipX = true;
        }
        else if (move.x < -0.01f)
        {
            if (p_spriteRenderer.flipX == false)
                p_spriteRenderer.flipX = true;
            c_spriteRenderer.flipX = false;
        }

        if (move.x == 0 && move.y == 0) SetAnimation("idle");
        else SetAnimation("walk");
        transform.transform.position += new Vector3(move.x, move.y, 0) * movespeed * Time.deltaTime;
    }
    
}
