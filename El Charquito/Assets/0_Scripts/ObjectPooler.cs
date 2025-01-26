using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] int _poolSize;
    private List<GameObject> _pool = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in _pool)
        {
            if (!obj.activeInHierarchy) 
                return obj;
        }
        return createNewObj();
    }
    private GameObject createNewObj()
    {
        GameObject obj = Instantiate(prefab,transform);
        obj.SetActive(false);
        return obj;
    }
}
