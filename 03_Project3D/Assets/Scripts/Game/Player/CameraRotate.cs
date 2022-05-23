using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : Singleton<CameraRotate>
{
    const string KEY_MOUSE_X = "SensitivityX";
    const string KEY_MOUSE_Y = "SensitivityY";

    [SerializeField] Transform playerBody;
    [SerializeField] Transform playerEye;

    [Range(-90.0f, 0.0f)]
    [SerializeField] float limitUp;
    [Range(0.0f, 90.0f)]
    [SerializeField] float limitDown;

    [Range(1f, 1000f)]
    [SerializeField] float sensitivityX; //수평 감도 
    [Range(1f, 1000f)]
    [SerializeField] float sensitivityY; //수직 감도 

    float rotateX; //수평 회전 각도
    Vector2 recoil; //총기 반동에 의한 값
    InventoryUI inventoryUI;

    private void Start()
    {
        sensitivityX = PlayerPrefs.GetFloat(KEY_MOUSE_X, 200f);
        sensitivityY = PlayerPrefs.GetFloat(KEY_MOUSE_Y, 100f);

        inventoryUI = InventoryUI.Instance;

        Cursor.lockState = CursorLockMode.Locked; //마우스 고정 
    }

    private void Update()
    {
        //GetAxisRaw : -1, 0, 1
        //GetAxis : -1.0f ~ 1.0f

        //인벤토리가 열려있으면 마우(시점)회전을 하지 않음 
        if(inventoryUI.isOpen)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * 0.005f; //마우스 x축 이동량
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * 0.005f; //마우스 y축 이동량 

        /*
         0 : 왼쪽버튼
         1 : 오른쪽 버튼
         2 : 휠 버튼
         3 : 보조 버튼 1
         4 : 보조 버튼 2
         GetMouseButton : 누르는 도중 계속
         GetMouseButtonDown : 누르는 순간 1회
         GetMouseButtonUp : 떼는 순간 1회 
        */

        OnMouseLook(new Vector2(mouseX, mouseY));
    }

    private void OnMouseLook(Vector2 axis)
    {
        //수직, 수평에 대한 반동 회전값 더하기
        axis += recoil;
        recoil = Vector2.zero;

        //수직 회전
        playerBody.Rotate(Vector2.up * axis.x);

        //수평 회전
        //마우스의 수직 이동량에 따라 rotateX의 값을 변환 (단, 각도에 제한을 둠)
        rotateX = Mathf.Clamp(rotateX - axis.y, limitUp, limitDown);
        playerEye.localRotation = Quaternion.Euler(rotateX, 0f, 0f);
    }

    public void AddRecoil(Vector2 recoil)
    {
        this.recoil = recoil;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(KEY_MOUSE_X, sensitivityX);
        PlayerPrefs.SetFloat(KEY_MOUSE_Y, sensitivityY);
    }
}
