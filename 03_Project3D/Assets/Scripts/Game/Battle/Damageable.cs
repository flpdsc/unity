using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] Status stat;
    [SerializeField] UnityEvent<int> onDamageEvent;
    [SerializeField] UnityEvent onDeadEvent;

    public void OnDamaged(int power)
    {
        if(stat.hp <= 0) return;

        stat.hp = Mathf.Clamp(stat.hp - power, 0, stat.maxHp); //실제 hp 감소 
        onDamageEvent?.Invoke(power); //피격 이벤트 발생 
        if(stat.hp <= 0) //사망 시 
        {
            onDeadEvent?.Invoke(); //죽음 이벤트 발생 
            Destroy(gameObject); //오브젝트 삭제 
        }
    }

}
