using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.GameSystem.Npc
{
    public class NpcManager : MonoBehaviour
    {
        [Header("Positions")]
        [SerializeField] Points points;

        [Header("NPC")]
        [SerializeField] Transform npc;
        [SerializeField, Min(0)] float moveSpeed;
        [SerializeField, Min(0)] float rotationSpeed;
        void Start()
        {
                        
        }
    }

    [System.Serializable]
    public class Points
    {
        [SerializeField] Transform startPosition;
        [SerializeField] Transform tablePosition;
        [SerializeField] Transform endPosition;

        public Transform StartPosition => startPosition;
        public Transform TablePosition => tablePosition;
        public Transform EndPosition => endPosition;
    }
}