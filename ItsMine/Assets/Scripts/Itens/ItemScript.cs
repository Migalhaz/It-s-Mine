using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Game.GameSystem.Itens
{
    using Core;
    using Interact;
    public class ItemScript : MonoBehaviour, IInteract
    {
        [ContextMenuItem("Generate Item", nameof(GenerateItem))]
        [ContextMenuItem("Deliver Item", nameof(Interact))]
        [Header("Dados do Item")]
        [SerializeField] Item item;
        public Item Item => item;
        GameObject model;

        [SerializeField] ScriptableObjects.EventScriptableObject onMouseButtonDown;
        ItemManager itemManager;
        Transform playerHand;
        bool inHand;

        private void Awake()
        {
            inHand = false;
            item = new();
            itemManager = ItemManager.Instance;
            playerHand = itemManager.PlayerHand;
            GenerateItem();

        }

        void LateUpdate()
        {
            if (!inHand) return;

            transform.position = playerHand.position;
            transform.rotation = playerHand.rotation;


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
            itemManager.GetModel(item.ItemType, gameObject);
        }

        public void DropObject()
        {
            inHand = false;
            itemManager.SetItemInHand(null);
            onMouseButtonDown -= DropObject;
        }

        public void Interact()
        {
            Debug.Log($"{"Interact function".Color(Color.red)} is not implemented! Script: {"ItemScript".Color(Color.yellow)}");

            Ui.UiManager.Instance.InspectItem(item.Description, this);
            //if (itemManager.ItemInHand != null)
            //{
            //    itemManager.ItemInHand.DropObject();
            //}
            //itemManager.SetItemInHand(this);
            //inHand = true;
            //onMouseButtonDown += DropObject;
            //This func must show itemDescription on UI
        }

        public void PickThisItem()
        {
            if (itemManager.ItemInHand != null)
            {
                itemManager.ItemInHand.DropObject();
            }
            itemManager.SetItemInHand(this);
            inHand = true;
            onMouseButtonDown += DropObject;
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
    static StringBuilder stringBuilder = new StringBuilder();
    public static string Color(this string _string, Color _textColor)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGBA(_textColor)}>{_string}</color>";
    }

    public static string Color(this string _string, string _textColorHex)
    {
        
        string _colorString = _textColorHex;
        if (_textColorHex.StartsWith('#'))
        {
            _colorString = _textColorHex.Split('#')[1];

        }
        return $"<color=#{_colorString}>{_string}</color>";
    }

    public static string Bold(this string _string)
    {
        return $"<b>{_string}</b>";
    }

    public static string Italic(this string _string)
    {
        return $"<i>{_string}</i>";
    }

    public static string Warning(this string _string)
    {
        return $"{"WARNING:".Bold().Color(UnityEngine.Color.yellow)} {_string.Italic()}";
    }

    public static string Error(this string _string)
    {
        return $"{"ERROR:".Bold().Color(UnityEngine.Color.red)} {_string.Italic()}";
    }
    public static string Append(this string _string, params string[] _textToAppend)
    {
        stringBuilder?.Clear();
        stringBuilder ??= new();
        stringBuilder.Append(_string);
        _textToAppend.ToList().ForEach(x => stringBuilder.Append($" {x}"));
        string _newText = stringBuilder.ToString();
        stringBuilder.Clear();
        return _newText;
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