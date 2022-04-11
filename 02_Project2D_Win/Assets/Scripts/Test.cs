using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] string playerName;
    [SerializeField] int hp;
    [SerializeField] float power;
    [SerializeField] Vector3 pos3;
    [SerializeField] Vector2 pos2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(playerName);
        Debug.Log(hp);
        Debug.Log(power);
        Debug.Log(pos3);
        Debug.Log(pos2);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
