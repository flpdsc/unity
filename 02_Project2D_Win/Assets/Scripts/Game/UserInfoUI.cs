using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Image[] hpImages;  //UI 이미지 배열
    [SerializeField] Text eatText;     //UI 텍스트
    [SerializeField] Text goldText;

    Player player;
    GameManager gm;

    private void Start()
    {
        player = Player.Instance;
        gm = GameManager.Instance;
    }
    private void Update()
    {
        SetHPImage(player.Hp);
        eatText.text = gm.Eat.ToString("#,##0");
        goldText.text = gm.Gold.ToString("#,##0");
    }
    private void SetHPImage(int hp)
    {
        for (int i = 0; i < 3; ++i)
        {
            //component.enabled : 해당 컴포넌트를 활성/비활성화 시킴
            hpImages[i].enabled = i < hp;
        }
    }
}
