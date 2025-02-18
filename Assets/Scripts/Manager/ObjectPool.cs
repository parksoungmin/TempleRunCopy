using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private Queue<T> pool;
    private Queue<T>[] pools;
    private T prefab;
    private Transform parent;

    public ObjectPool(T prefab, Transform parent, int initialSize)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new Queue<T>();

        for (int i = 0; i < initialSize; ++i)
        {
            T obj = GameObject.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public ObjectPool(T[] prefab, Transform parent)
    {
        pools = new Queue<T>[prefab.Length];
        for (int i = 0; i < prefab.Length; ++i)
        {
            pools[i] = new Queue<T>();
            
                this.prefab = prefab[i];
                this.parent = parent;
                T obj = GameObject.Instantiate(prefab[i], parent);
                obj.gameObject.SetActive(false);
                pools[i].Enqueue(obj);
            
        }
    }
    public T GetObject()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T obj = GameObject.Instantiate(prefab, parent);
            return obj;
        }
    }
    public T GetObject(int index)
    {
        if (pools[index].Count > 0)
        {
            T obj = pools[index].Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            T obj = GameObject.Instantiate(prefab, parent);
            return obj;
        }
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public void ReturnObject(T obj, int index)
    {
        obj.gameObject.SetActive(false);
        pools[index].Enqueue(obj);
    }
}