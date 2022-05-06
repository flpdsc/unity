using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoUI : Singleton<WeaponInfoUI>
{
    [SerializeField] Text maxBulletText;
    [SerializeField] Text currentBulletText;

    private void Update()
    {
        maxBulletText.text = PlayerController.Instance.MaxBullet.ToString();
        currentBulletText.text = PlayerController.Instance.CurrentBullet.ToString();
    }
}
