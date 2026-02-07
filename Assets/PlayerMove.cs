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
    [SerializeField]
    private Arm arm_;
    private bool isPushFire_;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 取得確認
        bool isGet = Camera.main.TryGetComponent<Cursor>(out cursor);

        //取得できていなければ処理を停止
        Assert.IsTrue(isGet, "componentの取得失敗");

        isPushFire_ = false;
    }

    private void UpdateGunTrigger()
    {
        if (!arm_.IsGrabGun()) { return; }
        if (isPushFire_)
        {
            arm_.OnTrigger();
        }
        else
        {
            arm_.OffTrigger();
        }
    }

    private void Update()
    {
        UpdateGunTrigger();
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
    private void TryGetGun(Collider item)
    {
        GunBase gun;
        if(!item.TryGetComponent(out gun)) { return; }
        if(!gun.GetIsAlone()) { return; }
        arm_.Grab(gun);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) {  return; }
        TryGetGun(other);
    }
    public void OnFire(InputValue inputValue)
    {
        isPushFire_ = inputValue.isPressed;
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