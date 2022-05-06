using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour       // �Ϲ�ȭ
    where T : MonoBehaviour                     // T�ڷ����� MonoBehaviour�� ����ϰ� �־���Ѵ�.
{

    // �̱���(Singleton)
    // => ��𿡼��� �ش� ��ü�� ������ �� �ִ� ������ ���� �� �ϳ�.
    //    ��, �ش� ��ü�� �ϳ��� �����ؾ��Ѵ�.
    static T instance;
    public static T Instance => instance;

    protected void Awake()
    {
        instance = this as T;       // this(Singleton�� ��)�� T�ڷ������� �� ��ȯ �õ�.
    }
}
