using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem.Interact.Object 
{
    public class DoorScript : MonoBehaviour, IInteract
    {

        [SerializeField, Range(0f, 10f)] float smooth;
        [SerializeField] Vector3 openRotation = new(0f, -90f, 0f);
        bool opened;
        void Start()
        {
            opened = false;
        }

        void Update()
        {
            if (opened)
            {
                AnimRotation(Quaternion.Euler(openRotation));
            }
            else
            {
                AnimRotation(Quaternion.identity);
            }

            void AnimRotation(Quaternion _newQuaternion)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _newQuaternion, smooth * Time.deltaTime);
            }
        }
              
        public void Interact()
        {
            opened = !opened;
        }
    }
}