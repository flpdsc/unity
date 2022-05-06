using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������Ʈ, �������̽� �Ϲ�ȭ.
// => ��������Ʈ �ڷ������� ������ ������ "�Լ��� �ּ�"�� ���� �� �ִ�.
public delegate void ReturnPoolEvent<PoolType>(PoolType pool);

// �������̽��� ����̴�.
// => �ش� �������̽��� ����� Ŭ������ �������̽� ������ �Լ��� "���� ����" �ؾ��Ѵ�.
public interface IObjectPool<PoolType>
{
    void Setup(ReturnPoolEvent<PoolType> onReturn);
}

// PoolType�ڷ����� Component����ϰ� �־�߸� �Ѵ�.
// �߰��� IObjectPool �������̽��� �����ϰ� �־�� �Ѵ�.
public class ObjectPool<ClassType, PoolType> : Singleton<ClassType>
    where ClassType : MonoBehaviour
    where PoolType : Component, IObjectPool<PoolType>
{
    [SerializeField] PoolType poolPrefab;
    [SerializeField] Transform storageParent;
    [SerializeField] int poolCount;

    Stack<PoolType> storage;

    private new void Awake()
    {
        base.Awake();
        storage = new Stack<PoolType>();
        for (int i = 0; i < poolCount; i++)
            CreatePool();
    }

    private void CreatePool()
    {
        PoolType newPool = Instantiate(poolPrefab); // PoolType�� ���� �𸣰����� ��� Object�� ����ϱ� �ֱ� ������ ���� ����.
        newPool.transform.SetParent(storageParent); // �θ� ������Ʈ�� storageParent ������Ʈ�� ����.
        newPool.Setup(OnReturnPool);                // ���� ���� pool�� �ǵ��ƿ��� �̺�Ʈ ���.
        storage.Push(newPool);                      // ���ÿ� ����.
    }

    private void OnReturnPool(PoolType pool)
    {
        pool.transform.SetParent(storageParent);    // �θ� ������Ʈ ����.
        storage.Push(pool);                         // ����ҿ� Push.
    }

    protected PoolType GetPool()
    {
        if (storage.Count <= 0)     // ������� ������ 0�� ���϶��.
            CreatePool();           // �ϳ� ���� �ִ´�.

        PoolType pool = storage.Pop();          // ����ҿ��� ������.
        pool.transform.SetParent(transform);    // �θ� ������Ʈ ����.
        return pool;                            // ��ȯ.
    }


}
