using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoUI : MonoBehaviour
{
    [SerializeField] Image[] hpImages;
    [SerializeField] Text eatText;
    [SerializeField] Text goldText;

    Player player;
    GameManager gm;

    private void Start()
    {
        player = Player.Instance;
        gm = GameManager.Instance;
    }


    void Update()
    {
        SetHPImage(player.Hp);
        eatText.text = gm.Eat.ToString("#,##0");
        goldText.text = gm.Gold.ToString("#,##0");
    }

    private void SetHPImage(int hp)
    {
        for(int i=0; i<3; ++i)
        {
            hpImages[i].enabled = i < hp;
        }
    }
}
