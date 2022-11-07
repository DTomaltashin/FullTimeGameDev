using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Player.Instance.gameObject)
        {
            SceneManager.LoadScene(nextSceneName);
            Player.Instance.gameObject.transform.position = GameObject.Find("SpawnPoint").transform.position;
        }
    }
}
