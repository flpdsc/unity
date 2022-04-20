using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Image[] hpImages;  //UI �̹��� �迭
    [SerializeField] Text eatText;     //UI �ؽ�Ʈ
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
            //component.enabled : �ش� ������Ʈ�� Ȱ��/��Ȱ��ȭ ��Ŵ
            hpImages[i].enabled = i < hp;
        }
    }
}
