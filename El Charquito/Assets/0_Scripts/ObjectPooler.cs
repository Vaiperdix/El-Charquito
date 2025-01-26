using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] int _poolSize;
    private List<BubbleBehaviour> _pool = new List<BubbleBehaviour>();

    void Start()
    {
        initializePool();
    }

    private void initializePool()
    {
        for (int i = 0; i<_poolSize;i++){
            _pool.Add(createNewObj());
        }
    }
    public BubbleBehaviour GetPooledObject()
    {
        foreach(BubbleBehaviour obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy) 
                return obj;
        }
        return createNewObj();
    }
    private BubbleBehaviour createNewObj()
    {
        GameObject obj = Instantiate(prefab,transform);
        BubbleBehaviour bubbleBehaviour = obj.GetComponent<BubbleBehaviour>();
        obj.SetActive(false);
        return bubbleBehaviour;
    }
}
