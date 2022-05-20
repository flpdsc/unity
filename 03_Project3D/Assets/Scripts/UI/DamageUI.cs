using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageUI : MonoBehaviour, IObjectPool<DamageUI>
{
    private enum SHOW_TYPE
    {
        Fix, //고정 
        MoveUp, //위로 이동 
        Volcano, //화산 
    }

    [SerializeField] Text damageText;
    [SerializeField] Rigidbody rigid; //for volcano
    [SerializeField] float showTime;
    [SerializeField] float fadeTime;
    [SerializeField] SHOW_TYPE showType;

    bool isStartShow;
    bool isShow;
    float countdown;
    Transform cam;

    public void SetDamage(Vector3 position, int damage)
    {
        isStartShow = false;
        isShow = true;
        countdown = 0f;
        transform.position = position;
        damageText.text = damage.ToString();
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = true;

        ChangeAlpha(1f);
    }

    private void Update()
    {
        LookCamera();
        ShowType();

        //보여지는 시간 
        if (isShow)
        {
            Countdown();
        }
        else
        {
            Fade();
        }
    }

    private void LookCamera()
    {
        Vector3 dir = (transform.position - cam.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void Countdown()
    {
        countdown += Time.deltaTime;
        if (countdown >= showTime)
        {
            countdown = fadeTime;
            isShow = false;
        }
    }

    private void Fade()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0.0f)
        {
            onReturn(this);
        }
        else
        {
            ChangeAlpha(countdown / fadeTime);
        }
    }

    private void ChangeAlpha(float alpha)
    {
        Color textColor = damageText.color; //텍스트에서 색상 값을 대입 
        textColor.a = alpha; //색상값 중 알파값만 변경 (직접 대입 불가)
        damageText.color = textColor; //변경한 색상 대입 
    }

    private void ShowType()
    {
        switch(showType)
        {
            case SHOW_TYPE.Fix:
                break;
            case SHOW_TYPE.MoveUp:
                MoveUp();
                break;
            case SHOW_TYPE.Volcano:
                if(!isStartShow)
                    Volcano();
                break;
        }

        isStartShow = true;
    }

    private void MoveUp()
    {
        transform.position += Vector3.up * 0.2f * Time.deltaTime;
    }

    private void Volcano()
    {
        rigid.isKinematic = false;
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), 0f);
        rigid.AddForce(dir * 2f, ForceMode.Impulse);
    }

    //풀링 인터페이스 구현 
    private ReturnPoolEvent<DamageUI> onReturn;

    public void Setup(ReturnPoolEvent<DamageUI> onReturn)
    {
        this.onReturn = onReturn;
        cam = Camera.main.transform;
    }
}
