using System;
using UnityEngine;
using UnityEditor;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Event", menuName = "Scriptable Objects/New Event/New Default Event", order = 0)]
    public class EventScriptableObject : ScriptableObject
    {
        Action action = delegate { };

        [MenuItem("Scriptable Objects/New Event/New Default Event")]
        protected static void MenuItem()
        {
            Editor.ScriptableObjectMenu.NewScriptable<EventScriptableObject>("Scriptable Objects", "Default Events", "New Event");
        }

        public void Invoke() => action?.Invoke();

        public void ResetEvents() => action = delegate { };

        public static EventScriptableObject operator +(EventScriptableObject _event, Action _action)
        {
            _event.action += _action;
            return _event;
        }

        public static EventScriptableObject operator -(EventScriptableObject _event, Action _action)
        {
            _event.action -= _action;
            return _event;
        }

        public static EventScriptableObject operator --(EventScriptableObject _event)
        {
            _event.ResetEvents();
            return _event;
        }
    }

    public abstract class EventMonoBehaviourManager : MonoBehaviour
    {
        protected abstract void OnEnable();
        protected abstract void OnDisable();
    }
}