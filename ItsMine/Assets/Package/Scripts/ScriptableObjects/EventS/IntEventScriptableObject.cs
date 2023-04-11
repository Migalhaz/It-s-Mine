using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Event", menuName = "Scriptable Objects/New Event/Event Int Overload", order = 0)]
    public class IntEventScriptableObject : ScriptableObject
    {
        Action<int> action = delegate { };

        [MenuItem("Scriptable Objects/New Event/Event Int Overload")]
        protected static void MenuItem()
        {
            Editor.ScriptableObjectMenu.NewScriptable<IntEventScriptableObject>("Scriptable Objects", "Int Overload Events", "New Int Event");
        }

        public void Invoke(int _value) => action?.Invoke(_value);

        public void ResetEvents() => action = delegate { };

        public static IntEventScriptableObject operator +(IntEventScriptableObject _event, Action<int> _action)
        {
            _event.action += _action;
            return _event;
        }

        public static IntEventScriptableObject operator -(IntEventScriptableObject _event, Action<int> _action)
        {
            _event.action -= _action;
            return _event;
        }

        public static IntEventScriptableObject operator --(IntEventScriptableObject _event)
        {
            _event.ResetEvents();
            return _event;
        }
    }
}