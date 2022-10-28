using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

[Serializable]
public class Pool
{
    public string Id;
    public GameObject Prefab;
    public int Size;
    public bool AutoGrow = true;

    [HideInInspector]
    public List<GameObject> PooledObjects;
}
public class PoolingSystem : Singleton<PoolingSystem>
{
    [SerializeField] private Pool[] Pools;

    [HideInInspector]
    public UnityEvent OnPoolInitialised = new UnityEvent();

    private void Awake()
    {
        InitialisePools();
    }

    private void InitialisePools()
    {
        foreach (var pool in Pools)
        {
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject instance = Instantiate(pool.Prefab, transform);
                pool.PooledObjects.Add(instance);
                instance.SetActive(false);
            }
        }

        OnPoolInitialised.Invoke();
    }

    public GameObject InstantiatePoolObject(string Id)
    {
        GameObject instance = GetPoolObjectById(Id);
        instance.SetActive(true);
        instance.transform.SetParent(null);

        return instance;
    }
    public void DestroyPoolObject(GameObject go)
    {
        go.transform.SetParent(transform);
        go.SetActive(false);
    }

    #region Instantiate Overloads
    public GameObject InstantiatePoolObject(string Id, Transform parent)
    {
        GameObject instance = InstantiatePoolObject(Id);
        instance.transform.SetParent(parent);

        return instance;
    }

    public GameObject InstantiatePoolObject(string Id, Vector3 position)
    {
        GameObject instance = InstantiatePoolObject(Id);
        instance.transform.position = position;

        return instance;
    }

    public GameObject InstantiatePoolObject(string Id, Vector3 position, Quaternion rotation)
    {
        GameObject instance = InstantiatePoolObject(Id);
        instance.transform.position = position;
        instance.transform.rotation = rotation;

        return instance;
    }

    public GameObject InstantiatePoolObject(string Id, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject instance = InstantiatePoolObject(Id);
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.transform.SetParent(parent);

        return instance;
    }
    #endregion

    #region Destroy Overloads
    public void DestroyPoolObject(GameObject go, float delay)
    {
        StartCoroutine(DestroyPoolObjectCo(go, delay));
    }

    private IEnumerator DestroyPoolObjectCo(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        DestroyPoolObject(go);
    }
    #endregion

    #region HelperMethods
    private GameObject GetPoolObjectById(string Id)
    {
        Pool selectedPool = null;

        foreach (var pool in Pools)
        {
            if (pool.Id == Id)
            {
                selectedPool = pool;
                break;
            }
        }

        if (selectedPool == null)
        {
            Debug.LogWarning("There is no pool that has " + Id + "id. Please check if ID is correct.");
        }

        return GetInactivePoolObject(selectedPool);
    }

    private GameObject GetInactivePoolObject(Pool pool)
    {
        GameObject go = null;

        foreach (var poolObj in pool.PooledObjects)
        {
            if (!poolObj.activeSelf)
            {
                go = poolObj;
                break;
            }
        }

        if (go == null && pool.AutoGrow)
        {
            go = Instantiate(pool.Prefab, transform);
            pool.PooledObjects.Add(go);
            go.SetActive(false);
        }

        if (go == null)
        {
            Debug.LogWarning("There is no available object to spawn. Increase the size of pool or enable auto grow.");
        }

        return go;
    }
    #endregion
}