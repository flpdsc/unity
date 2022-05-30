using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Camera cam;
    [SerializeField] RectTransform panel;
    [SerializeReference] RectTransform joystick;
    [SerializeField] RectTransform stick;

    [Header("Events")]
    [SerializeField] UnityEvent<float, float> OnMoveStick;

    Vector2 originPos; //스틱 원위치
    float maxDistance; //최대로 움직일 수 있는 거리 
    bool isOperated;

    private void Start()
    {
        originPos = stick.localPosition;
        maxDistance = joystick.rect.width / 2f;

    }

    private void Update()
    {
        if (!isOperated)
            return;

        //Canvas의 모드가 ScreenSpace - Camera이기 때문에 실제 해상도와 차이가 있음
        //따라서 해당 캔버스가 비치는 카메라의 값에 따라 마우스 위치 조정 필요
        Vector2 mousePos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(panel, Input.mousePosition, cam, out mousePos);

        //최대 거리 제한 
        float currentDistance = Vector2.Distance(originPos, mousePos);
        if(currentDistance > maxDistance)
        {
            Vector2 dir = (mousePos - originPos).normalized; //거리에 따른 방향값에 차이가 발생하기 때문에 "정규화" 시켜줌
            mousePos = originPos + (dir * maxDistance);
        }

        stick.localPosition = mousePos;

        //이벤트 발생 (내가 움직인 방향 전달)
        Vector2 moveDir = (mousePos - originPos).normalized;
        OnMoveStick?.Invoke(moveDir.x, moveDir.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isOperated = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isOperated = false;
        stick.localPosition = originPos;
    }
}