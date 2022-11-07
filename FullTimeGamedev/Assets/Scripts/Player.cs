using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : PhysicsObject
{
    [Header("Movement Attributes")]
    [SerializeField] private float maxSpeed = 1;
    float groundedRemember = 0f;
    [SerializeField] float groundedRememberTimer = 0.25f;
    float jumpRemember = 0f;
    [SerializeField] float jumpRememberTimer = 0.25f;
    [SerializeField] private float JumpStrength = 1;
    bool facingRight;

    [Header("UI Attributes")]
    public float coinsCollected = 0f;
    public float health = 100f;
    Vector2 healthBarOrizSize;
    float maxHealth = 100f;

    //attack
    [Header("Attack Attributes")]
    [SerializeField] GameObject AttackBox;
    public float attackDamage = 10f;
    [SerializeField] float attackDuration = 0.05f;

    [Header("Animator")]
    [SerializeField] Animator anim;


    //Delcaring player as a Singleton
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<Player>();
            return instance;
        }
    }

    private void Awake()
    {
        //Destroy Scene Player if the player from prev scene loads in
        if (GameObject.Find("NewPlayer")) Destroy(gameObject);    
    }

    void Start()
    {
        //Progressive Game mode ... player won't die on load and his inventory will be carried over to the next lvl
        DontDestroyOnLoad(gameObject);
        gameObject.name = "NewPlayer";

        gameObject.transform.position = GameObject.Find("SpawnPoint").transform.position;
        //save healthbar sprite size in a variable
        healthBarOrizSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;

        //update ui according to values set in Inspecter
        UpdateUI();
    }

    void Update()
    {
        Move();
        Attack();
        Die();
    }

    void Move()
    {
        //left right movement
        targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0) * maxSpeed;
        if (targetVelocity.x > 0f && facingRight) Flip();
        if (targetVelocity.x < 0f && !facingRight) Flip();
        //jump
        groundedRemember -= Time.deltaTime;
        if (grounded)
        {
            // allows player to jump withing 0.1sec of leaving the platform
            groundedRemember = groundedRememberTimer;
        }


        jumpRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Vertical"))
        {
            anim.SetBool("Grounded", false);
            // allows player to jump if key was pressed just before getting grounded
            jumpRemember = jumpRememberTimer;
        }
        else
            anim.SetBool("Grounded", true);



        if ((jumpRemember > 0) && (groundedRemember > 0))
        {
            jumpRemember = 0;
            groundedRemember = 0;
            velocity.y = JumpStrength;
        }

        anim.SetFloat("VelocityX", MathF.Abs(velocity.x) / maxSpeed);
        anim.SetFloat("VelocityY", velocity.y);
    }

    //Flip character based on movement direction
    void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
    public void UpdateUI()
    {
        //coin stat update
        GameManager.Instance.coinsCollectedText.text = coinsCollected.ToString();
        //health stat update
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrizSize.x * (health / maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
            StartCoroutine(AttackDelay());
        }
    }

    private  IEnumerator AttackDelay()
    {
    
        AttackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        AttackBox.SetActive(false);
    }

    void Die()
    {
        if(health <= 0)
            SceneManager.LoadScene("SampleScene");
    }
}
