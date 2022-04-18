using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] float GodModeTime;
    [SerializeField] int maxHp;

    //참조형 변수들은 캐싱해서 쓰는 것이 좋음
    //GetComponent는 오브젝트를 검색하기 때문에 메모리 효율이 좋지 않음
    //따라서 초기화 시점에 미리 검색하고 이후에는 변수를 사용함
    SpriteRenderer spriteRenderer;
    Movement movement;
    Animator anim;

    bool isGodMode;
    bool isFallDown;
    int hp;
    int coin;

    //프로퍼티
    public bool isDead => hp <= 0 || isFallDown; //isDead는 참조만 가능하며 리턴값은 조건 연산자의 결과값
    public int Hp => hp;    //Hp를 참조하면 hp값을 리턴
    public int Coin => coin; //Coin을 참조하면 coin값을 리턴

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = gameObject.GetComponent<Movement>();
        anim = GetComponent<Animator>();

        hp = maxHp;

        StartPoint.Instance.SetStartPoint(transform);
    }

    public void OnContactTrap(TrapSpike trap)
    {
        if (isGodMode)
        {
            return;
        }
        StartCoroutine(OnHit(trap.transform));
    }

    public void OnFallDown()
    {
        isFallDown = true;
    }

    public void OnContactCoin(Coin target)
    {
        coin += 1;
    }

    public void OnSwitchLockControl(bool isLock)
    {
        movement.OnSwitchLockControl(isLock);
    }

    public void OnStartHitAnim()
    {
        anim.SetBool("isHit", true);
    }

    public void OnEndHitAnim()
    {
        anim.SetBool("isHit", false);
    }

    private IEnumerator OnHit(Transform target)
    {
        if((hp-=1) <= 0) //체력을 1 깎은 후 0 이하라면
        {
            OnDead();
            Collider2D collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }

        isGodMode = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f); //반투명 상태
        anim.SetTrigger("onHit");

        //Invoke(string, int) : void
        //특정함수를 n초 후에 호출

        //nameof(Method) : 함수명을 string 문자로 변환
        Invoke(nameof(ReleaseGodMode), GodModeTime);
        StartCoroutine(HitPlayer());

        yield return null;
        movement.OnThrow(target);
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

        for (int i = 0; i < 3; ++i)
        {
            spriteRenderer.color = red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}