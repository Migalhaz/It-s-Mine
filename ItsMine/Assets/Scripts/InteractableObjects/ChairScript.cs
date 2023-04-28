using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Game.GameSystem.Interact.Object
{
    public class ChairScript : MonoBehaviour, IInteract
    {
        [SerializeField] float finishedZPosition;
        float startedZPosition;
        bool active;
        [SerializeField, Min(0)] float animSpeed;
        void Start()
        {
            startedZPosition = transform.position.z;
            active = false;
        }
        public void Interact()
        {
            active = !active;

            if (active)
            {
                transform.DOMoveZ(finishedZPosition, animSpeed);
            }
            else
            {
                transform.DOMoveZ(startedZPosition, animSpeed);
            }
        }

    }
}