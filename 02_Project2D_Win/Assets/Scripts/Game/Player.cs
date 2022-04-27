using System.Collections;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] float GodModeTime;
    [SerializeField] int maxHp; //플레이어의 최대 체력

    [Header("Attack")]
    [SerializeField] Transform footPivot;
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask attackMask;

    //참조형 변수들은 캐싱해서 쓰는 것이 좋음
    //GetComponent는 오브젝트를 검색하기 때문에 메모리 효율이 좋지 않음
    //따라서 초기화 시점에 미리 검색하고 이후에는 변수를 사용함
    SpriteRenderer spriteRenderer;
    Movement movement;
    Animator anim;
    Rigidbody2D rigid;

    bool isGodMode;
    bool isFallDown; //플레이어가 아래로 떨어졌음
    int hp;   //현재 체력

    //프로퍼티
    public bool isDead => (hp <= 0) || isFallDown; //isDead는 참조만 가능하며 리턴값은 아래 조건 연산자의 결과 값임
    public int Hp => hp; //Hp를 참조하면 hp값을 리턴

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = gameObject.GetComponent<Movement>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        hp = maxHp;

        StartPoint.Instance.SetStartPoint(transform);
    }

    private void Update()
    {
        OnCheckAttack();
    }

    private void OnCheckAttack()
    {
        if(rigid.velocity.y >= 0f)
        {
            return;
        }
        Collider2D contact = Physics2D.OverlapCircle(footPivot.position, attackRadius, attackMask);

        if(contact != null)
        {
            EnemyTree enemy = contact.GetComponent<EnemyTree>();
            if(enemy!=null)
            {
                enemy.OnDamaged();
                rigid.velocity = new Vector2(rigid.velocity.x, 0f);
                rigid.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            }
        }
    }

    public void OnContactTrap(GameObject trap)
    {
        if (isGodMode)
        {
            return;
        }
        StartCoroutine(OnHit(trap.transform.position));
    }

    public void OnContactCoin(Coin target)
    {
        GameManager.Instance.AddEatCount(1);
    }

    public void OnFallDown()
    {
        isFallDown = true;
    }

    public void OnSwitchLockControl(bool isLock)
    {
        movement.OnSwitchLockControl(isLock);
    }

    public void OnEndHitAnim()
    {
        anim.SetBool("isHit", false);
    }

    private IEnumerator OnHit(Vector3 hitPosition)
    {
        if ((hp -= 1) <= 0) //체력을 1 깎은 후 0 이하라면,
        {
            OnDead(); //죽음
            Collider2D collider = GetComponent<Collider2D>(); //콜라이더 검색
            collider.isTrigger = true; //트리거로 변경
        }

        isGodMode = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f); //반투명 상태
        anim.SetBool("isHit", true);
        anim.SetTrigger("onHit"); //피격 애니메이션 트리깅

        //Invoke(string, int) : void
        //특정함수를 n초 후에 호출

        //nameof(Method) : 함수명을 string 문자로 변환
        Invoke(nameof(ReleaseGodMode), GodModeTime);
        StartCoroutine(HitPlayer());
        //내 오브젝트에서 Movement 검색
        //이후 OnThrow함수를 trap의 transform으로 보내 호출
        yield return null;
        movement.OnThrow(hitPosition);
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

    private void OnDrawGizmos()
    {
        if(footPivot!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(footPivot.position, attackRadius);
        }
    }
}
