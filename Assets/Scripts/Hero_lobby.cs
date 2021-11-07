using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero_lobby : Charic2D
{
    public SpriteRenderer c_spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        HeroMove();
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
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "portal") SceneManager.LoadScene(2); //¾ÀÀüÈ¯
    }
}
