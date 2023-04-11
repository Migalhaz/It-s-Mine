using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Event", menuName = "Scriptable Objects/New Event/Event String Overload", order = 0)]
    public class StringEventScriptableObject : ScriptableObject
    {
        Action<string> action = delegate { };

        [MenuItem("Scriptable Objects/New Event/Event String Overload")]
        protected static void MenuItem()
        {
            Editor.ScriptableObjectMenu.NewScriptable<StringEventScriptableObject>("Scriptable Objects", "String Overload Events", "New String Event");
        }

        public void Invoke(string _value) => action?.Invoke(_value);

        public void ResetEvents() => action = delegate { };

        public static StringEventScriptableObject operator +(StringEventScriptableObject _event, Action<string> _action)
        {
            _event.action += _action;
            return _event;
        }

        public static StringEventScriptableObject operator -(StringEventScriptableObject _event, Action<string> _action)
        {
            _event.action -= _action;
            return _event;
        }

        public static StringEventScriptableObject operator --(StringEventScriptableObject _event)
        {
            _event.ResetEvents();
            return _event;
        }
    }
}