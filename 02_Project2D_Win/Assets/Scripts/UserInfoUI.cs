using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image[] hpImages;  //UI 이미지 배열
    [SerializeField] Text coinText;     //UI 텍스트


    private void Update()
    {
        SetHPImage(player.Hp);
        SetCoinText(player.Coin);
    }
    private void SetHPImage(int hp)
    {
        for (int i = 0; i < 3; ++i)
        {
            //component.enabled : 해당 컴포넌트를 활성/비활성화 시킴
            hpImages[i].enabled = i < hp;
        }
    }

    public void SetCoinText(int coin)
    {
        //int형 값 coin을 string으로 변환시켜야 함
        //object.ToString() : ToString은 문자열로 변경시켜라
        coinText.text = coin.ToString();
    }
}
