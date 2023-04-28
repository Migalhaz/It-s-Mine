using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem.Itens
{
    public class ItemManager : Singleton.Singleton<ItemManager>
    {
        [SerializeField] List<ItemModel> itemModel;

        [Header("Spawn")]
        [SerializeField] IntRange itensRange;
        [SerializeField] GameObject itemGameObject;
        [SerializeField] List<Transform> spawnItensPoints;
        List<Transform> availableSpawnPoints;

        private void Start()
        {
            availableSpawnPoints = spawnItensPoints;
            for (int i = 0; i < itensRange.GetRandomValue(); i++)
            {
                Transform _currentSpawnPoint = availableSpawnPoints.GetRandom();
                Instantiate(itemGameObject, _currentSpawnPoint.position, _currentSpawnPoint.rotation);
                availableSpawnPoints.Remove(_currentSpawnPoint);
            }
        }

        public GameObject GetModel(Core.ItemType _itemType)
        {
            GameObject _model = itemModel.Find(x => x.ItemType == _itemType)?.Model;
            if (_model == null)
            {
                Debug.Log($"{_itemType}".StringColor(Color.red) + " doesn't have a model defined");
            }
            return _model;
        }
    }
    
    [System.Serializable]
    class ItemModel
    {
        [SerializeField] Core.ItemType itemType;
        [SerializeField] GameObject model;

        public Core.ItemType ItemType => itemType;
        public GameObject Model => model;
    }
}