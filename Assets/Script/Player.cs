using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    public Transform player;

    float moveSpeed = 15f;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        //카메라 shaking Test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.GetComponent<CameraMgr>().ShakeCamera(0.5f);
        }
    }

    public void PlayMove(Vector3 axis)
    {
        player.eulerAngles = new Vector3(player.eulerAngles.x,
            Mathf.Atan2(axis.x, axis.y) * Mathf.Rad2Deg, player.eulerAngles.z);

        player.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
