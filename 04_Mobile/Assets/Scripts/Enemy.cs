using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum STATE
    {
        WaitPatrol, //정찰 후 대기 
        Patrol, //정찰중 
        Chase, //추적중 
        Attack, //공격중 
    }

    [Header("Target")]
    [SerializeField] STATE state;
    [SerializeField] Transform player;
    [SerializeField] LayerMask targetMask;

    [Header("Range")]
    [SerializeField] float patrolRadius;
    [SerializeField] float searchRadius;
    [SerializeField] float attackRadius;

    [Header("Time")]
    [SerializeField] float waitNextPatrolTime; //다음 정찰시간 
    [SerializeField] float waitNextAttackTime; //다음 공격시간 

    [Header("Weapon")]
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform bulletPivot;
    [SerializeField] float bulletSpeed;

    private bool isInSearchRange; //탐지 범위에 들어옴 
    private bool isInAttackRange; //공격 범위에 들어옴

    private Vector3 patrolPos;
    private Vector3 originPos; //원래 위치
    private NavMeshAgent agent; //네브 매쉬 


    private float nextPatrolTime;
    private float nextAttackTime;

    private void Start()
    {
        originPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        isInSearchRange = Physics.CheckSphere(transform.position, searchRadius, targetMask);
        isInAttackRange = Physics.CheckSphere(transform.position, attackRadius, targetMask);

        switch (state)
        {
            case STATE.WaitPatrol:
                WaitPatrol();
                break;
            case STATE.Patrol:
                Patrol();
                break;
            case STATE.Chase:
                ChaseToPlayer();
                break;
            case STATE.Attack:
                AttackToPlayer();
                break;
        }
    }

    private void WaitPatrol()
    {
        //다음 정찰 시간까지 대기 
        if (nextPatrolTime <= Time.time)
        {
            state = STATE.Patrol;

            //랜덤한 구의 Vector3 포지션을 0.0 ~ 1.0 비율값으로 받아옴
            patrolPos = originPos + (Random.insideUnitSphere * patrolRadius);
            patrolPos.y = transform.position.y;
            agent.SetDestination(patrolPos);
        }
        //뛰고있는데 탐지범위에 들어오면 추적 
        else if(isInSearchRange)
        {
            state = STATE.Chase;
        }
    }

    private void Patrol()
    {
        //목적지에 도착했다면 
        if(!agent.hasPath)
        {
            state = STATE.WaitPatrol;
            nextPatrolTime = Time.time + waitNextPatrolTime;
        }
        else if(isInSearchRange)
        {
            state = STATE.Chase;
        }
    }


    private void ChaseToPlayer()
    {
        //공격 범위에 들어왔을 때 
        if(isInAttackRange && IsLockOn())
        {
            agent.SetDestination(transform.position); //공격상태가 되면 내 위치로 멈춤
            state = STATE.Attack;
        }
        //탐지 범위를 벗어났을 때 
        else if(!isInSearchRange)
        {
            state = STATE.Patrol;
        }
        //플레이어 추적 중일 때 
        else
        {
            agent.SetDestination(player.position);
        }
    }

    private void AttackToPlayer()
    {
        if(isInAttackRange==false || IsLockOn()==false)
        {
            state = STATE.Chase;
        }
        //공격시간이 되었다면 공격
        else if (nextAttackTime <= Time.time)
        {
            Attack();
            nextAttackTime = Time.time + waitNextAttackTime;
        }
    }

    private bool IsLockOn()
    {
        //나와 상대 사이가 뚫려있다면 true 
        Vector3 targetDir = (player.position - transform.position).normalized;
        RaycastHit hit;
        Ray ray = new Ray(transform.position, targetDir);

        return Physics.Raycast(ray, out hit) && hit.collider.GetComponent<Player>() != null;
    }

    private void Attack()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletPivot.position, Quaternion.identity);
        Vector3 dir = (player.position - bulletPivot.position).normalized;
        bullet.Shoot(dir, bulletSpeed);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(patrolPos, 0.2f);

        Vector3 pivot = Application.isPlaying ? originPos : transform.position;

        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(pivot, Vector3.up, patrolRadius);

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, searchRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, attackRadius);

    }
#endif
}