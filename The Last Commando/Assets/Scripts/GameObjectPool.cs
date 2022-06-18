using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public GameObject prefab;

    private Queue<GameObject> _objects = new Queue<GameObject>();
    private int _totalObjects = 0;

    public GameObject Get()
    {
        if(_objects.Count == 0)
        {
            AddObjects(1);
            _totalObjects++;
        }
        return _objects.Dequeue();
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        _objects.Enqueue(objectToReturn);
    }

    private void AddObjects(int count)
    {
        var newObject = GameObject.Instantiate(prefab);
        newObject.SetActive(false);
        _objects.Enqueue(newObject);

        newObject.GetComponent<IPooledGameObject>().Pool = this;
    }

    public bool AllObjectsInPool()
    {
        if (_objects.Count == _totalObjects)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
