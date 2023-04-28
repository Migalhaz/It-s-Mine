using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem.Itens
{
    using Core;
    public class ItemScript : MonoBehaviour
    {
        [ContextMenuItem("Generate Item", nameof(GenerateItem))]
        [Header("Dados do Item")]
        [SerializeField] Item item;
        public Item Item => item;
        GameObject model;

        private void Start()
        {
            item = new();
            GenerateItem();
        }
        public void GenerateItem()
        {
            ItemType newItemType = (ItemType) Random.Range(0, typeof(ItemType).EnumLength());
            Shifts newItemShift = (Shifts) Random.Range(0, typeof(Shifts).EnumLength());
            Courses newItemCourse = (Courses) Random.Range(0, typeof(Courses).EnumLength());
            Period newItemPeriod = (Period) Random.Range(0, typeof(Period).EnumLength());

            

            item.Setup(newItemType, newItemShift, newItemCourse, newItemPeriod);
            UpdateModel();
        }

        public void SetItem(Item _newItem)
        {
            item.Setup(_newItem.ItemType, _newItem.Shift, _newItem.Course, _newItem.Period, _newItem.Register);
            UpdateModel();
        }

        void UpdateModel()
        {
            if (model != null) DestroyImmediate(model);

            model = ItemManager.Instance.GetModel(item.ItemType);

            if (model == null) return;
            model = Instantiate(model, transform);
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

public static class ListExtend
{
    public static T GetRandom<T>(this List<T> _list)
    {
        return _list[Random.Range(0, _list.Count)];
    }
}

public static class RangeExtend
{
    public static float RangeBy0(float _maxInclusive)
    {
        return Random.Range(0f, _maxInclusive);
    }
    public static int RangeBy0(int _maxExclusive)
    {
        return Random.Range(0, _maxExclusive);
    }
}

public static class StringExtend
{
    public static string StringColor(this string _string, Color _textColor)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(_textColor)}>{_string}</color>";
    }
}

[System.Serializable]
public struct Range
{
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    public float MinValue => minValue;
    public float MaxValue => maxValue;

    public float GetRandomValue()
    {
        return Random.Range(minValue, maxValue);
    }
}

[System.Serializable]
public struct IntRange
{
    [SerializeField] int minValue;
    [SerializeField] int maxValue;
    public int MinValue => minValue;
    public int MaxValue => maxValue;
    public int GetRandomValue(bool _maxInclusive = false)
    {
        return Random.Range(minValue, maxValue + (_maxInclusive ? 1 : 0));
    }
}