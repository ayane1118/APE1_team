using UnityEngine;

/// <summary>
/// プレイヤーの移動・カメラ制御。
/// ライフとタイマーは第6週に PlayerStatus.cs として別コンポーネントで追加予定。
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("移動")]
    [SerializeField] float moveSpeed = 5f;

    [Header("カメラ（マウス視点）")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity = 2f;
    [SerializeField] float verticalClampAngle = 80f;

    CharacterController cc;
    float verticalAngle = 0f;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    void Update()
    {
        HandleLook();
        HandleMove();
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 横回転：キャラクター本体を回す
        transform.Rotate(Vector3.up, mouseX);

        // 縦回転：カメラのみ（上下クランプ付き）
        verticalAngle -= mouseY;
        verticalAngle  = Mathf.Clamp(verticalAngle, -verticalClampAngle, verticalClampAngle);
        if (cameraTransform != null)
            cameraTransform.localEulerAngles = new Vector3(verticalAngle, 0f, 0f);
    }

    void HandleMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;
        move = Vector3.ClampMagnitude(move, 1f) * moveSpeed;

        // 重力
        if (!cc.isGrounded) move.y -= 9.81f * Time.deltaTime;

        cc.Move(move * Time.deltaTime);
    }
}
