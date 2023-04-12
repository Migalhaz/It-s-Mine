using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameSystem.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] float moveSpeed = 10f;

        [Header("Input Settings")]
        [SerializeField, Min(1)] float mouseSmoothness = 100;
        [SerializeField] Vector2 mouseSense = new(2, 2);
        [SerializeField] Vector2 maxYView = new(-30, 30);
        float rY;
        float rX;

        [Header("Transforms")]
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform camPivot;
        [SerializeField] Transform cam;
        Vector3 dir;
        Vector3 inputs;
        Rigidbody rig;


        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            InputListener();
            Move();
        }

        private void FixedUpdate()
        {
            FixedMove();
        }

        void FixedMove()
        {
            rig.AddForce(dir * moveSpeed, ForceMode.Force);
        }

        void Move()
        {
            playerTransform.Rotate(0, rX, 0, Space.World);
            cam.rotation = Quaternion.Lerp(cam.rotation, Quaternion.Euler(rY * mouseSense.y, playerTransform.eulerAngles.y, 0f), mouseSmoothness * Time.deltaTime);
            camPivot.position = Vector3.Lerp(camPivot.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }

        void InputListener()
        {
            inputs.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            dir = playerTransform.TransformVector(inputs.normalized);
            rX = Mathf.Lerp(rX, Input.GetAxisRaw("Mouse X") * mouseSense.x, mouseSmoothness * Time.deltaTime);
            rY = Mathf.Clamp(rY - Input.GetAxisRaw("Mouse Y") * mouseSense.y * mouseSmoothness * Time.deltaTime, maxYView.x, maxYView.y);
        }
    }
}