using UnityEngine;
// 新しいInput Systemを使うための宣言
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private CharacterController controller;

    void Start()
    {
        // 骸骨にくっついているCharacter Controllerを取得
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 新しいInput Systemのやり方でキーボードの入力を取得（矢印キー・WASD共通）
        Vector2 inputMovement = Vector2.zero;

        if (Keyboard.current != null)
        {
            // WASDキー または 矢印キー（upArrowKeyなどに修正）の入力を検知
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) inputMovement.y = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) inputMovement.y = -1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) inputMovement.x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) inputMovement.x = 1f;
        }

        // 入力方向を計算
        Vector3 moveDirection = new Vector3(inputMovement.x, 0, inputMovement.y).normalized;

        // キャラクターを移動させる
        if (controller != null)
        {
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        // 動いている方向にスムーズに見た目を向ける
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}