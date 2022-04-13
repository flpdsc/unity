using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��������Ʈ, �������̽� �Ϲ�ȭ
public delegate void ReturnPoolEvent<PoolType>(PoolType pool);
public interface IObjectPool<PoolType>
{
    void Setup(ReturnPoolEvent<PoolType> onReturn);
}

//PoolType�ڷ����� Component ����ϰ� �־�� �ϸ�
//IObjectPool �������̽��� �����ϰ� �־�� ��
public class ObjectPool<ClassType, PoolType> : Singleton<ClassType>
    where ClassType : MonoBehaviour
    where PoolType : Component, IObjectPool<PoolType>
{
    [SerializeField] PoolType poolPrefab;
    [SerializeField] Transform storageParent;
    [SerializeField] int poolCount = 5;

    Stack<PoolType> storage;

    private new void Awake()
    {
        base.Awake();
        storage = new Stack<PoolType>();
        for(int i=0; i<poolCount; ++i)
        {
            CreatePool();
        }
    }

    private void CreatePool()
    {
        PoolType newPool = Instantiate(poolPrefab); // Instantiate�� Object �޾ƾ� �ϹǷ� where PoolType : Object ���� �ʿ�
        newPool.transform.SetParent(storageParent); // �θ� ������Ʈ�� storageParent ������Ʈ�� �����ؾ� �ϹǷ� where PoolType : Component ���� �ʿ�         newPool.Setup(OnReturnPool); //���� ���� pool�� �ǵ��� ���� �̺�Ʈ ���
        newPool.Setup(OnReturnPool);
        storage.Push(newPool); //���ÿ� ����
    }

    private void OnReturnPool(PoolType pool)
    {
        pool.transform.SetParent(storageParent); //�θ� ������Ʈ ����
        storage.Push(pool); //����ҿ� Push
    }

    protected PoolType GetPool()
    {
        if(storage.Count<=0) // ������� ������ 0�� ���ϸ� �ϳ� ���� ����
        {
            CreatePool();
        }
        PoolType pool = storage.Pop();
        pool.transform.SetParent(transform);
        return pool;
    }
}
