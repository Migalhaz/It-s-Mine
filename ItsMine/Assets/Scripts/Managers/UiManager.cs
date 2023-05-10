using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.GameSystem.Ui {
    
    public class UiManager : Singleton.Singleton<UiManager>
    {
        [SerializeField] GameObject crosshair;
        [SerializeField] Inspect inspect;

        protected override void Awake()
        {
            base.Awake();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetCrosshairEnable(bool _enabled)
        {
            crosshair.SetActive(_enabled);
        }

        public void InspectItem(string _itemDescription, Itens.ItemScript _itemScript)
        {
            inspect.InspecItem(_itemDescription, _itemScript);
        }
    }
}