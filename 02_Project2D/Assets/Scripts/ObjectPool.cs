using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//델리게이트, 인터페이스 일반화
public delegate void ReturnPoolEvent<PoolType>(PoolType pool);

public interface IObjectPool<PoolType>
{
    void Setup(ReturnPoolEvent<PoolType> onReturn);
}

//PoolType자료형은 Component를 상속하고, IObjecctPool 인터페이스를 구현하고 있어야 함
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
        PoolType newPool = Instantiate(poolPrefab); // Instantiate는 Object 받아야 하므로
                                                    // where PoolType : Object 조건 필요
        newPool.transform.SetParent(storageParent); // 부모 오브젝트를 storageParent 오브젝트로 변경
                                                    // where PoolType : Component 조건 필요 
        newPool.Setup(OnReturnPool); //새로만든 pool에 되돌아오는 이벤트 등록 
        storage.Push(newPool); //스택에 저장 
    }

    private void OnReturnPool(PoolType pool)
    {
        pool.transform.SetParent(storageParent); //부모 오브젝트 변경 
        storage.Push(pool); //저장소에 Push 
    }

    protected PoolType GetPool()
    {
        if(storage.Count <= 0) //저장소의 개수가 0개 이하라면 
        {
            CreatePool(); //하나 만들어서 넣는다 
        }

        PoolType pool = storage.Pop(); //저장소에서 꺼냄 
        pool.transform.SetParent(transform); //부모 오브젝트 변경 
        return pool; //반환 
    }
}
