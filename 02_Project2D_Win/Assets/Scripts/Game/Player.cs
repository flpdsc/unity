using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float GodModeTime;
    [SerializeField] int maxHp; //플레이어의 최대 체력

    //참조형 변수들은 캐싱해서 쓰는 것이 좋음
    //GetComponent는 오브젝트를 검색하기 때문에 메모리 효율이 좋지 않음
    //따라서 초기화 시점에 미리 검색하고 이후에는 변수를 사용함
    SpriteRenderer spriteRenderer;
    bool isGodMode;
    bool isFallDown; //플레이어가 아래로 떨어졌음
    int hp;   //현재 체력
    int coin; //보유 코인

    //프로퍼티
    public bool isDead => (hp <= 0) || isFallDown; //isDead는 참조만 가능하며 리턴값은 아래 조건 연산자의 결과 값임
    public int Hp => hp; //Hp를 참조하면 hp값을 리턴
    public int Coin => coin; //Coin을 참조하면 coin값을 리턴

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = maxHp;
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

        OnHit();
    }

    public void OnContactCoin(Coin target)
    {
        coin += 1;
    }

    public void OnFallDown()
    {
        isFallDown = true;
    }

    private void OnHit()
    {
        if (isGodMode)
        {
            return;
        }

        if ((hp -= 1) <= 0) //체력을 1 깎은 후 0 이하라면,
        {
            OnDead(); //죽음
        }
        else
        {
            isGodMode = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); //반투명 상태

            //Invoke(string, int) : void
            //특정함수를 n초 후에 호출

            //nameof(Method) : 함수명을 string 문자로 변환
            Invoke(nameof(ReleaseGodMode), GodModeTime);
            StartCoroutine(HitPlayer());
        }
    }

    private void OnDead()
    {

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

        for(int i=0; i<3; ++i)
        {
            spriteRenderer.color = red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
