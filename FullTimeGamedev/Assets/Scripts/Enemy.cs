using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [Header("Attributes")]
    [SerializeField] float attackDamage = 10f;
    public float health;
    float maxHealth = 50f;
    [SerializeField] private float maxSpeed;
    private int direction = 1;

    [Header("Raycast Attributes")]
    [SerializeField] private Vector2 ledgeRayCastOffset;
    [SerializeField] private float ledgeRayCastLenght = 1;
    [SerializeField] private Vector2 wallRayCastOffset;
    [SerializeField] private float wallRayCastLenght = 1;
    [SerializeField] LayerMask raycastLayerMask; //filter the layers you want the raycast to work on

    RaycastHit2D rightLedgeRaycastHit;
    RaycastHit2D leftLedgeRaycastHit;
    RaycastHit2D rightWallRaycastHit;
    RaycastHit2D leftWallRaycastHit;

    //damage
    void Start()
    {
        health = maxHealth;
        Debug.Log(health);
    }

    void Update()
    {
        Move();
        Die();
    }

    void Move()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //change direction if there is no ground beneath
        //check for rightLegde
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) + ledgeRayCastOffset, Vector2.down, ledgeRayCastLenght);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y) + ledgeRayCastOffset, Vector2.down * ledgeRayCastLenght, Color.blue);
        if (rightLedgeRaycastHit.collider == null) direction = -1;

        //check for leftLedge
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - ledgeRayCastOffset, Vector2.down, ledgeRayCastLenght);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y) - ledgeRayCastOffset, Vector2.down * ledgeRayCastLenght, Color.blue);
        if (leftLedgeRaycastHit.collider == null) direction = 1;

        //check for Walls
        //check for rightWall
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) + wallRayCastOffset, Vector2.right, wallRayCastLenght, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y) + wallRayCastOffset, Vector2.right * wallRayCastLenght, Color.white);
        if (rightWallRaycastHit.collider != null) direction = -1;

        //check for leftWall
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y) - wallRayCastOffset, Vector2.left, wallRayCastLenght, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y) - wallRayCastOffset, Vector2.left * wallRayCastLenght, Color.white);
        if (leftWallRaycastHit.collider != null) direction = 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.health -= attackDamage;
            Player.Instance.UpdateUI();
        }
    }
    
    void Die()
    {
        if (health <= 0) Destroy(gameObject);
    }
}
