using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI data")]
    public TextMeshProUGUI coinsCollectedText;
    public Image healthBar;
    //singleton Declaration
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Awake()
    {
        if (GameObject.Find("Active Game Manager")) Destroy(gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "Active Game Manager";
    }
    void Update()
    {
        
    }
}
