using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem.Pool
{
    using ScriptableObjects;
    [Serializable]
    public class Pool 
    {
        #region Variables
        [SerializeField] PoolDataScriptableObject poolData;

        Transform poolParent;
        List<GameObject> freeObjects;
        List<GameObject> inUseObjects;
        #region Getters
        public PoolDataScriptableObject PoolData => poolData;
        public List<GameObject> FreeObjects => freeObjects;
        public List<GameObject> InUseObjects => inUseObjects;
        #endregion

        #endregion

        #region Methods
        public bool CompareTag(string _tag)
        {
            return poolData.CompareTag(_tag);
        }

        public void Setup(Transform _parent)
        {
            freeObjects = new List<GameObject>();
            inUseObjects = new List<GameObject>();
            poolParent = _parent;
        }

        public void CreateObject()
        {
            GameObject newGameObject = UnityEngine.Object.Instantiate(poolData.Prefab, poolParent);
            newGameObject.SetActive(false);
            freeObjects.Add(newGameObject);
        }

        public GameObject GetFreeGameObject()
        {
            if (FreeObjects.Count <= 0)
            {
                if (!poolData.ExpandablePool) return null;
                else CreateObject();
            }

            GameObject poolGameObject = FreeObjects[FreeObjects.Count - 1];
            freeObjects.Remove(poolGameObject);
            inUseObjects.Add(poolGameObject);
            poolGameObject.SetActive(true);
            return poolGameObject;
        }

        public List<GameObject> GetAllGameObjects()
        {
            List<GameObject> gameObjects = new List<GameObject>();
            gameObjects.AddRange(InUseObjects);
            gameObjects.AddRange(FreeObjects);
            return gameObjects;
        }

        public List<T> GetAllGameObjects<T>() where T : Component
        {
            List<T> t = new();
            GetAllGameObjects().ForEach(x => t.Add(x.GetComponent<T>()));
            return t;
        }

        public void ReturnObject(GameObject _activeGameObject)
        {
            if (!inUseObjects.Contains(_activeGameObject)) return;

            freeObjects.Add(_activeGameObject);
            inUseObjects.Remove(_activeGameObject);
            _activeGameObject.SetActive(false);
        }

        public void ReturnAlllObjects()
        {
            freeObjects.AddRange(inUseObjects);
            inUseObjects.Clear();
            freeObjects.ForEach(x => x.SetActive(false));
        }

        
        #endregion
    }
    public class PoolManager : Singleton.Singleton<PoolManager>
    {
        [SerializeField] List<Pool> pools = new List<Pool>() { new Pool()};

        protected override void Awake()
        {
            base.Awake();

            foreach (Pool pool in pools)
            {
                pool.Setup(new GameObject($"{pool.PoolData.PoolTag} Pool").transform);
                for (int i = 0; i < pool.PoolData.PoolSize; i++)
                {
                    pool.CreateObject();
                }
            }
        }
        public Pool GetPool(string _poolTag)
        {
            return pools.Find(x => x.CompareTag(_poolTag));
        }

        public Pool GetPool(PoolDataScriptableObject _poolData)
        {
            return pools.Find(x => x.PoolData == _poolData);
        }

        public GameObject GetFreeGameObjectFromPool(string _poolTag)
        {
            return GetPool(_poolTag).GetFreeGameObject();
        }
        public T GetFreeGameObjectFromPool<T>(string _poolTag) where T : Component
        {
            GameObject g = GetFreeGameObjectFromPool(_poolTag);
            if (g == null) return null;

            if (g.TryGetComponent(out T _t))
            {
                return _t;
            }

            return null;
        }

        public GameObject GetFreeGameObjectFromPool(PoolDataScriptableObject _poolTag)
        {
            return GetPool(_poolTag).GetFreeGameObject();
        }
        public T GetFreeGameObjectFromPool<T>(PoolDataScriptableObject _poolTag) where T : Component
        {
            GameObject g = GetFreeGameObjectFromPool(_poolTag);
            if (g == null) return null;

            if (g.TryGetComponent(out T _t))
            {
                return _t;
            }

            return null;
        }

        public void ReturnUsingGameObjectToPool(string _poolTag, GameObject _gameObject)
        {
            GetPool(_poolTag).ReturnObject(_gameObject);
        }

        public void ReturnUsingGameObjectToPool(PoolDataScriptableObject _poolTag, GameObject _gameObject)
        {
            GetPool(_poolTag).ReturnObject(_gameObject);
        }

        public void ReturnAllObjectsToPool(string _poolTag)
        {
            GetPool(_poolTag).ReturnAlllObjects();
        }

        public void ReturnAllObjectsToPool(PoolDataScriptableObject _poolTag)
        {
            GetPool(_poolTag).ReturnAlllObjects();
        }
    }
}
