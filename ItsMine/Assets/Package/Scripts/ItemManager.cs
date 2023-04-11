using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem
{
    public class ItemManager : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField] ScriptableObjects.PoolDataScriptableObject itemPool;
        Pool.PoolManager poolManager;
        [SerializeField, Min(0)] int minItens;
        [SerializeField, Min(0)] int maxItens;

        [Header("Player Settings")]
        [SerializeField] ItemScript currentItem;


        [Header("Events")]
        [SerializeField] ScriptableObjects.StringEventScriptableObject mouseOverItem;

        [Header("UI")]
        [SerializeField] TMPro.TextMeshProUGUI tmpro;

        
        void Start()
        {
            mouseOverItem += UpdateDescription;
            poolManager = Pool.PoolManager.Instance;
            int itensLength = Random.Range(minItens, maxItens);

            for (int i = 0; i < itensLength; i++)
            {
                ItemScript item = poolManager.GetFreeGameObjectFromPool<ItemScript>(itemPool);
                item.GenerateItem();
                item.OnSelectItem += SelectItem;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DropItem();
            }
        }

        void UpdateDescription(string _description)
        {
            tmpro.text = _description;
        }

        void SelectItem(ItemScript _item)
        {
            DropItem();
            currentItem = _item;
            poolManager.ReturnUsingGameObjectToPool(itemPool, _item.gameObject);

        }

        void DropItem()
        {
            if (currentItem == null) return; 
            ItemScript item = poolManager.GetFreeGameObjectFromPool<ItemScript>(itemPool);
            item.SetItem(currentItem.Item);
            item.SetPosition(999);
            currentItem = null;
        }
    }
}