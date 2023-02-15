using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;//скорость движения
    [SerializeField] private int lives = 5;// кол-во жизней
    [SerializeField] private float jumpForce = 7f;//сила прыжка
    [SerializeField] private Transform player;
    private bool check_start;
    private bool check3;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    public static Hero Instance { get; set; }
    public GameObject l1; 
    public GameObject l2; 
    public GameObject l3; 
    public GameObject l4; 
    public GameObject l5;


    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set{anim.SetInteger("state",(int)value);}
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (isGrounded) State = States.idle;
        if(Input.GetButton("Horizontal"))
            Run();
        if(isGrounded&&Input.GetButtonDown("Jump"))
            Jump();
        if (player.transform.position.y < -50.0f)
        {
            Check();
        }

        if(lives<=0)
            Die();
        if (!isGrounded) State = States.jump;
    }
    
    private void Run()
    {
        if (isGrounded) State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;//поворот челика в стороны
    }

    private void Jump()
    {
        rb.AddForce(transform.up*jumpForce,ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1.1f);
        isGrounded = collider.Length > 1;
    }
    
    public void ToDie(string die)
    {
        SceneManager.LoadScene(die);
    }
    private void Die()
    {   
        Debug.Log("здох");
        ToDie(die:"die");
    }
    

    public void GetDamage()
    {
        lives -= 1;
        Debug.Log("lives: "+lives);
        string liv = "live" + lives;
        Destroy(GameObject.Find(liv));
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Wood"))
        {
            Debug.Log("die");
            GetDamage();
        }
        if (col.gameObject.CompareTag("Check3"))
        {
            Debug.Log("CheckPoint 3 done!");
            check3 = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Panel")||col.gameObject.tag.Equals("Panel_Final"))
        {
            this.transform.parent = col.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Panel")||other.gameObject.tag.Equals("Panel_Final"))
        {
            this.transform.parent = null;
        }
    }

    private void Check()
    {
        if (check3)
        {
            lives--;
            string liv2 = "live" + lives;
            Destroy(GameObject.Find(liv2));
            player.position = new Vector2(70, -5f);
        }
        else
        {
            lives--;
            string liv2 = "live" + lives;
            Destroy(GameObject.Find(liv2));
            player.position = new Vector2(-7.68f, 0f);
        }
    }
}

public enum States
{
    idle,
    run,
    jump
}
