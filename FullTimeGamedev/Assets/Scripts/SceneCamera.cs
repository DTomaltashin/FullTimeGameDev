using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    void Start()
    {
        playerFollowCamera.Follow = Player.Instance.transform;
    }
}
