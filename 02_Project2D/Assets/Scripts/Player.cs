using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float GodModeTime;

    //참조형 변수들은 캐싱해서 쓰는 것이 좋음
    //GetComponent는 오브젝트를 검색하기 때문에 메모리 효율이 좋지 않음
    //따라서 초기화 시점에 미리 검색하고 이후에는 변수를 사용함
    SpriteRenderer spriteRenderer;
    bool isGodMode;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void OnContactTrap(TrapSpike trap)
    {
        if (isGodMode)
        {
            return;
        }
        //내 오브젝트에서 Movement 검색
        //이후 OnThrow함수를 trap의 transform으로 
        Movement movement = gameObject.GetComponent<Movement>();
        movement.OnThrow(trap.transform);
        //Debug.Log($"{trap.name}에 충돌함");

        isGodMode = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f); //반투명 상태

        //Invoke(string, int) : void
        //특정함수를 n초 후에 호출

        //nameof(Method) : 함수명을 string 문자로 변환
        Invoke(nameof(ReleaseGodMode), GodModeTime);
        StartCoroutine(HitPlayer());
    }

    private void ReleaseGodMode()
    {
        isGodMode = false;  //무적해제
        spriteRenderer.color = Color.white; //원색으로 되돌림
    }

    //코루틴
    private IEnumerator HitPlayer()
    {
        Color red = new Color(1, 0, 0, 0.5f);
        Color white = new Color(1, 1, 1, 0.5f);

        for (int i = 0; i < 3; ++i)
        {
            spriteRenderer.color = red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}