using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem
{
    using Core;
    public class ItemScript : MonoBehaviour
    {
        [ContextMenuItem("Generate Item", nameof(GenerateItem))]
        [SerializeField] Item item;
        public System.Action<ItemScript> OnSelectItem;
        [SerializeField] ScriptableObjects.StringEventScriptableObject Description;
        [SerializeField, Range(1, 100)] float mouseSmooth = 1f;
        Camera cam;
        Vector3 currentPosition;
        public Item Item => item;

        private void Start()
        {
            cam = Camera.main;
        }
        public void GenerateItem()
        {
            ItemType newItemType = (ItemType) Random.Range(0, typeof(ItemType).EnumLength());
            Colors newItemColor = (Colors) Random.Range(0, typeof(Colors).EnumLength());
            Shifts newItemShift = (Shifts) Random.Range(0, typeof(Shifts).EnumLength());
            Courses newItemCourse = (Courses) Random.Range(0, typeof(Courses).EnumLength());
            Period newItemPeriod = (Period) Random.Range(0, typeof(Period).EnumLength());

            int i = EnumExtend.EnumLength<ItemType>();

            item.Setup(newItemType, newItemColor, newItemShift, newItemCourse, newItemPeriod);
        }

        public void SetItem(Item _newItem)
        {
            item.Setup(_newItem.ItemType, _newItem.ItemColor, _newItem.Shift, _newItem.Course, _newItem.Period, _newItem.Register);
        }

        private void OnMouseEnter()
        {
            Description.Invoke(item.Description);
        }

        private void OnMouseExit()
        {
            Description.Invoke("Nenhum item selecionado!");
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(1))
            {
                OnSelectItem?.Invoke(this);
                Description.Invoke("Nenhum item selecionado!");
            }
        }

        private void OnMouseDrag()
        {
            SetPosition(mouseSmooth);
        }

        public void SetPosition(float _smothness)
        {
            Vector3 worldPos = Vector3.Lerp(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), Time.deltaTime * _smothness);
            currentPosition.Set(worldPos.x, worldPos.y, 0f);
            transform.position = currentPosition;
        }
    }
}

public static class EnumExtend
{
    public static int EnumLength(this System.Type _type)
    {
        return System.Enum.GetNames(_type).Length;
    }

    public static int EnumLength<TEnum>(this TEnum _enum) where TEnum : struct, System.Enum
    {
        return System.Enum.GetNames(typeof(TEnum)).Length;
    }

    public static int EnumLength<TEnum>() where TEnum : struct, System.Enum
    {
        return System.Enum.GetNames(typeof(TEnum)).Length;
    }
}