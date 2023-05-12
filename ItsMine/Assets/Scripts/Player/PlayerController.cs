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
        [SerializeField] PlayerInputs playerInputManager;
        [SerializeField, Min(1)] float mouseSmoothness = 100;
        [SerializeField] Vector2 mouseSense = new(2, 2);
        [SerializeField] Vector2 maxYView = new(-30, 30);
        float rY;
        float rX;

        [Header("Transforms")]
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform camPivot;
        [SerializeField] Transform mainCam;
        [SerializeField] Transform itemRendererCam;
        Vector3 dir;
        Vector3 inputs;
        Rigidbody rig;

        [Header("Editor Only")]
        bool followMouse = true;


        private void Awake()
        {
            playerInputManager ??= gameObject.TryGetComponent(out PlayerInputs _newPlayerInput) ? _newPlayerInput : gameObject.AddComponent<PlayerInputs>();
            rig = GetComponent<Rigidbody>();
            
        }

        private void Start()
        {
            SetFollowMouse(true);
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
            if (playerInputManager.PauseCamPressed()) SetFollowMouse(!followMouse);
            if (!followMouse) return;
            playerTransform.Rotate(0, rX, 0, Space.World);

            

            mainCam.rotation = Quaternion.Lerp(mainCam.rotation, Quaternion.Euler(rY * mouseSense.y, playerTransform.eulerAngles.y, 0f), mouseSmoothness * Time.deltaTime);
            //itemRendererCam.rotation = mainCam.rotation;
            camPivot.position = Vector3.Lerp(camPivot.position, playerTransform.position, moveSpeed * Time.deltaTime);

            
        }

        public void SetFollowMouse(bool _newValue)
        {
            
            followMouse = _newValue;

            Cursor.lockState = followMouse ? CursorLockMode.Locked : CursorLockMode.Confined;
            Ui.UiManager.Instance.SetCrosshairEnable(followMouse);
        }

        void InputListener()
        {
            //inputs.Set(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            inputs.Set(playerInputManager.MoveInput().x, 0f, playerInputManager.MoveInput().y);
            dir = playerTransform.TransformVector(inputs.normalized);
            rX = Mathf.Lerp(rX, playerInputManager.MousePositionInput().x * mouseSense.x, mouseSmoothness * Time.deltaTime);
            rY = Mathf.Clamp(rY - playerInputManager.MousePositionInput().y * mouseSense.y * mouseSmoothness * Time.deltaTime, maxYView.x, maxYView.y);
            
        //    rX = Mathf.Lerp(rX, Input.GetAxisRaw("Mouse X") * mouseSense.x, mouseSmoothness * Time.deltaTime);
        //    rY = Mathf.Clamp(rY - Input.GetAxisRaw("Mouse Y") * mouseSense.y * mouseSmoothness * Time.deltaTime, maxYView.x, maxYView.y);
        }
    }
}