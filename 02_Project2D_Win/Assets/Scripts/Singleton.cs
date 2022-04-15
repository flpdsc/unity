using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour //Generic
    where T : MonoBehaviour               //T �ڷ����� MonoBehaviour�� ����ؾ� �ϴ� ����
{
    //Singleton : ��𿡼��� �ش� ��ü�� ������ �� �ִ� ����������
    //��, �ش� ��ü�� �ϳ��� �����ؾ� ��
    static T instance;
    public static T Instance => instance;

    //d
    protected void Awake()
    {
        instance = this as T; //this(Singleton�� ��)�� T �ڷ������� �� ��ȯ
    }
}
