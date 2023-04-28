using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem.Player
{
    public class PlayerRaycast : MonoBehaviour
    {
        [Header("Raycast Settings")]
        [SerializeField] Transform camPos;
        [SerializeField] float raycastDistance = 10f;
        RaycastHit hit;
        [SerializeField] LayerMask hitLayerMask;
        [SerializeField] KeyCode interactKeycode = KeyCode.E;

#if UNITY_EDITOR
        [Header("Gizmos Settings")]
        [SerializeField] Color colorInHit = Color.green;
        [SerializeField] Color colorOutHit = Color.red;
#endif
        void Update()
        {
            if (!InteractLogic()) return;
            PlayerInteract();
        }

        bool InteractLogic()
        { 
            if (!Input.GetKeyDown(interactKeycode)) return false;
            if (hit.transform == null) return false;
            return true;
        }

        void PlayerInteract()
        {
            if (hit.transform.TryGetComponent(out Interact.IInteract _component))
            {
                _component.Interact();
            }
            else
            {
                Debug.Log($"{hit.transform.name.StringColor(Color.red)} game object doesn't have {"IInteract".StringColor(Color.white)} implemented");
            }
        }

        private void FixedUpdate()
        {
            Physics.Raycast(camPos.position, camPos.transform.forward, out hit, raycastDistance, hitLayerMask);
#if UNITY_EDITOR
            Debug.DrawRay(camPos.position, camPos.transform.forward * raycastDistance, hit.transform == null ? colorOutHit : colorInHit);
#endif
        }
    }
}

namespace Game.GameSystem.Interact
{
    public interface IInteract
    {
        void Interact();
    }
}