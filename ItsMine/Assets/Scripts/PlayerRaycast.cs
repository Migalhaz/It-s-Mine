using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem.Player
{
    public class PlayerRaycast : MonoBehaviour
    {
        [SerializeField] Transform camPos;
        RaycastHit hit;
        [SerializeField] LayerMask hitLayerMask;
        [SerializeField] Texture2D cursorTexture;

        private void Awake()
        {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }

        void Update()
        {
            Debug.Log(hit.transform);
        }

        private void FixedUpdate()
        {
            Physics.Raycast(camPos.position, camPos.transform.forward, out hit, 200f, hitLayerMask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(camPos.position, camPos.transform.forward * 200f);
        }
    }
}