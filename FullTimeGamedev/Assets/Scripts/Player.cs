using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : PhysicsObject
{
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float JumpStrength = 1;

    public float coinsCollected = 0f;
    public float health = 100f;
    private float maxHealth = 100f;
    [SerializeField] Vector2 healthBarOrizSize;

    public TextMeshProUGUI coinsCollectedText;
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //save healthbar sprite size in a variable
        healthBarOrizSize = healthBar.rectTransform.sizeDelta;

        //update ui according to values set in Inspecter
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        //left right movement
        targetVelocity = new Vector2(Input.GetAxis("Horizontal"),0)*maxSpeed;

        //jump
        if(Input.GetButtonDown("Vertical") && grounded)
        {
            velocity.y = JumpStrength;
        }
    }

    public void UpdateUI()
    {
        //coin stat update
        coinsCollectedText.text = coinsCollected.ToString();
        //health stat update
        healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrizSize.x*(health/maxHealth),healthBar.rectTransform.sizeDelta.y);
    }
}
