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
        [field: SerializeField] public Transform PlayerHand { get; private set; }
        List<Transform> availableSpawnPoints;
        List<ItemScript> availableItens = new List<ItemScript>();

        private void Start()
        {
            availableSpawnPoints = spawnItensPoints;
            for (int i = 0; i < itensRange.GetRandomValue(); i++)
            {
                Transform _currentSpawnPoint = availableSpawnPoints.GetRandom();
                availableItens.Add(Instantiate(itemGameObject, _currentSpawnPoint.position, _currentSpawnPoint.rotation).GetComponent<ItemScript>());
                availableSpawnPoints.Remove(_currentSpawnPoint);
            }

            Debug.Log("Testando Cor".Color("#FFC0CB"));
            Debug.Log("Testando Cor".Color("FFC0CB"));
        }

        public void GetModel(Core.ItemType _itemType, GameObject _model)
        {
            ItemModel _currentItem = itemModel.Find(x => x.ItemType == _itemType);
            if (_currentItem == null)
            {
                Debug.Log($"{_itemType} doesn't have a model defined".Error());
                return;
            }
            //GameObject _model = _currentItem?.Model;
            _model.GetComponent<MeshFilter>().mesh = _currentItem?.Mesh;
            _model.GetComponent<MeshRenderer>().material = _currentItem?.Material;

            if (_currentItem.Mesh != null)
            {
                _model.AddComponent<BoxCollider>();
            }
        }

        public void ObjectDelivered(ItemScript _itemDelivered)
        {
            if (!availableItens.Contains(_itemDelivered))
            {
                Debug.LogWarning($"{_itemDelivered.name} isn't in the available itens list".Error());
                return;
            }
            Destroy(_itemDelivered.gameObject);
            availableItens.Remove(_itemDelivered);
            if (availableItens.Count <= 0)
            {
                Debug.Log("Todos os itens foram entregues!".Color(Color.green));
            }
        }
    }
    
    [System.Serializable]
    class ItemModel
    {
        [SerializeField] Core.ItemType itemType;
        [SerializeField] GameObject model;
        [SerializeField] Mesh mesh;
        [SerializeField] Material material;

        public Core.ItemType ItemType => itemType;
        public GameObject Model => model;
        public Mesh Mesh => mesh;
        public Material Material => material;
    }
}