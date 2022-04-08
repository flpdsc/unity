using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image[] hpImages;
    [SerializeField] Text coinText;

    void Update()
    {
        SetHPImage(player.Hp);
        SetCoinText(player.Coin);
    }

    private void SetHPImage(int hp)
    {
        for(int i=0; i<3; ++i)
        {
            //component.enabled : 해당 컴포넌트를 활성/비활성화 시킴
            hpImages[i].enabled = i < hp;
        }
    }

    private void SetCoinText(int coin)
    {
        //object.ToString() : int형 값 coin을 string으로 변환
        coinText.text = coin.ToString();
    }
}
