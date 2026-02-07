using System;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed_ = 1f;
    private Rigidbody rb;
    private Vector2 moveInput;
    //カーソル
    [SerializeField]
    private Cursor cursor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 取得確認
        bool isGet = Camera.main.TryGetComponent<Cursor>(out cursor);

        //取得できていなければ処理を停止
        Assert.IsTrue(isGet, "componentの取得失敗");
    }

    private void Update()
    {
        //もしCursorのレイがヒットしてなければ早期リターン
        if (!cursor.GetIsHit()) { return; }

        // レイの衝突情報を取得
        RaycastHit raycasthit = cursor.GetRaycastHit();

        //pointが衝突の座標
        Vector3 lookAt = raycasthit.point;

        // 術突位置は床なので、Playerと同じ目線の高さまで補正
        lookAt.y = transform.position.y;

        //LookAtメソッドは、引数で指定した座標へ向くメソッドだ
        transform.LookAt(lookAt);
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 input;
        input = new Vector3(moveInput.x, 0, moveInput.y);

        // 入力が無かったら早期リターン
        if (input.sqrMagnitude == 0) { return; }

        //Rigidbodyの移動機能を用いて移動
        rb.MovePosition(transform.position + input * moveSpeed_ * Time.deltaTime);
    }
}