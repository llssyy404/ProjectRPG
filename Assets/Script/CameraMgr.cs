using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    public enum state { Title, Play };
    public state CameraState { get; private set; }

    private Vector3 startPlayPos = new Vector3(0, 26.75f, -55);
    private Vector3 startPlayRot = new Vector3(35, 0.0f, 0.0f);

    private Vector3 startTitlePos = new Vector3(25, 35, -300);
    private Vector3 startTitleRot = new Vector3(15, 270, 0.0f);

    public GameObject player;

    public float offsetX = 0f;
    public float offsetY = 25f;
    public float offsetZ = -35f;

    public float shakes = 0f;
    public float shakeAmout = 0.7f;
    public float decreaseFactor = 1f;

    Vector3 originalPos;
    bool CameraShaking;

    Vector3 cameraPosition;


    // Use this for initialization
    void Start()
    {
        CameraState = state.Title;
        this.transform.position = startTitlePos;
        this.transform.localRotation = Quaternion.Euler(startTitleRot);

        player = GameManager.GetInstance().GetPlayer().gameObject;
        originalPos = transform.position;
        CameraShaking = false;
    }

    public void ShakeCamera(float shaking)
    {
        shakes = shaking;
        originalPos = transform.position;
        CameraShaking = true;
    }

    //카메라 shaking
    void FixedUpdate()
    {
        if (CameraState == state.Play)
            PlayCameraMove();
        else if (CameraState == state.Title)
            TitleCameraMove();

    }

    //카메라 move
    void LateUpdate()
    {
        if (CameraState == state.Title)
            return;

        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;
        transform.position = Vector3.Lerp(transform.position, cameraPosition, 5f * Time.deltaTime);
    }

    void TitleCameraMove()
    {
        transform.Translate(-Time.deltaTime, 0, 0, Space.World);
    }

    void PlayCameraMove()
    {
        if (CameraShaking)
        {
            if (shakes > 0)
            {
                transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmout;
                //  transform.position += new Vector3(0f, 0f, -30f);

                shakes -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                shakes = 0f;
                transform.localPosition = originalPos;
                CameraShaking = false;
            }
        }
    }

    public void SetCameraState(state state)
    {
        CameraState = state;

        if (CameraState == state.Play)
        {
            this.transform.position = startPlayPos;
            this.transform.localRotation = Quaternion.Euler(startPlayRot);
        }
        else if (CameraState == state.Title)
        {
            this.transform.position = startTitlePos;
            this.transform.localRotation = Quaternion.Euler(startTitleRot);
        }
           
    }
    
}
