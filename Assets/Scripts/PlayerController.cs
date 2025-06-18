using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded = false;

    // 現在向いている方向（true:右, false:左）
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float move = 0f;

        // 横移動（A=左、D=右）
        if (Input.GetKey(KeyCode.A))
        {
            move = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1f;
        }
        else
        {
            move = 0f;
        }

        // Rigidbodyによる移動（Xのみ動かす）
        
        
        
        rb.velocity = new Vector3(move * moveSpeed, rb.velocity.y, 0);
        
        
        
        

        // アニメーター管理
        if (isGrounded)
        {
            animator.SetBool("isWalking", move != 0f);
        }
        

        // ★向きを変更する処理
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        // ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("jumpTrigger");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            isGrounded = false;
        }
    }

    // 向きを変える関数
    void Flip()
    {
        facingRight = !facingRight;
        float yRotation = facingRight ? 140f : -140f;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }


    // 接地判定（tag利用）
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void LateUpdate()
    {
        // Z軸位置を常に0にする
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}
