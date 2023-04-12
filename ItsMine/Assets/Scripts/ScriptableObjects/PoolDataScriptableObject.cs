using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "New Pool Data", menuName = "Scriptable Objects/New Pool")]
    public class PoolDataScriptableObject : ScriptableObject
    {
        [SerializeField] string poolTag = "New Pool";
        [SerializeField] GameObject prefab;
        [SerializeField, Min(1)] int poolSize = 1;
        [SerializeField] bool expandablePool = false;

        public string PoolTag => poolTag;
        public int PoolSize => poolSize;
        public bool ExpandablePool => expandablePool;
        public GameObject Prefab => prefab;

        public bool CompareTag(string _tag)
        {
            return poolTag.Equals(_tag);
        }

        [MenuItem("Scriptable Objects/New Pool Data")]
        protected static void MenuItem()
        {
            Editor.ScriptableObjectMenu.NewScriptable<PoolDataScriptableObject>("Scriptable Objects", "Pool Data", "New Pool");
        }
    }
}