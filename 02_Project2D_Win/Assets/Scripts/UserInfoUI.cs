using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image[] hpImages;  //UI �̹��� �迭
    [SerializeField] Text coinText;     //UI �ؽ�Ʈ


    private void Update()
    {
        SetHPImage(player.Hp);
        SetCoinText(player.Coin);
    }
    private void SetHPImage(int hp)
    {
        for (int i = 0; i < 3; ++i)
        {
            //component.enabled : �ش� ������Ʈ�� Ȱ��/��Ȱ��ȭ ��Ŵ
            hpImages[i].enabled = i < hp;
        }
    }

    public void SetCoinText(int coin)
    {
        //int�� �� coin�� string���� ��ȯ���Ѿ� ��
        //object.ToString() : ToString�� ���ڿ��� ������Ѷ�
        coinText.text = coin.ToString();
    }
}
