using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.GameSystem.Ui
{
    public class Inspect : MonoBehaviour
    {
        [SerializeField] Player.PlayerController playerController;
        [Header("Inspec Itens")]
        [SerializeField] RectTransform inspecItempanel;
        [SerializeField] float typeSpeed = 0.01f;
        [SerializeField] float panelSmothness = 5f;
        string itemDescription;
        [SerializeField] TextMeshProUGUI itemDescriptionTMPRO;
        bool inspectEnabled = false;
        bool typing = false;
        Game.GameSystem.Itens.ItemScript currentItem;

        private void Update()
        {
            if (inspectEnabled)
            {
                inspecItempanel.localScale = Vector3.Lerp(inspecItempanel.localScale, Vector3.one, panelSmothness * Time.deltaTime);
            }
            else
            {
                inspecItempanel.localScale = Vector3.Lerp(inspecItempanel.localScale, Vector3.zero, panelSmothness * Time.deltaTime);
            }
        }

        public void InspecItem(string _itemDescription, Itens.ItemScript _currentItem)
        {
            if (typing) return;
            currentItem = _currentItem;
            itemDescriptionTMPRO.text = "";
            EnableInspect();
            itemDescription = _itemDescription;
            StartCoroutine(TypeDescription());
        }

        void EnableInspect()
        {
            inspectEnabled = true;
            playerController.SetFollowMouse(false);
        }

        public void DisableInspect()
        {
            playerController.SetFollowMouse(true);
            inspectEnabled = false;
            typing = false;
        }

        public void PickItem()
        {
            currentItem.PickThisItem();
        }

        IEnumerator TypeDescription()
        {
            typing = true;
            foreach (char c in itemDescription)
            {
                itemDescriptionTMPRO.text += c;
                yield return new WaitForSeconds(typeSpeed);
            }
            typing = false;
        }
    }
}