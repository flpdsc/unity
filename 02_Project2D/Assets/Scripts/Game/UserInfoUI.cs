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
            hpImages[i].enabled = i < hp;
        }
    }

    private void SetCoinText(int coin)
    {
        coinText.text = coin.ToString();
    }
}
