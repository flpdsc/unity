using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour //Generic
    where T : MonoBehaviour               //T 자료형은 MonoBehaviour를 상속해야 하는 조건
{
    //Singleton : 어디에서든 해당 객체에 접근할 수 있는 디자인패턴
    //단, 해당 객체는 하나만 존재해야 함
    static T instance;
    public static T Instance => instance;

    //d
    protected void Awake()
    {
        instance = this as T; //this(Singleton인 나)를 T 자료형으로 형 변환
    }
}
