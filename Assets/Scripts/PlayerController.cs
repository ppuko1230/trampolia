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

    // ���݌����Ă�������itrue:�E, false:���j
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float move = 0f;

        // ���ړ��iA=���AD=�E�j
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

        // Rigidbody�ɂ��ړ��iX�̂ݓ������j
        
        
        
        rb.velocity = new Vector3(move * moveSpeed, rb.velocity.y, 0);
        
        
        
        

        // �A�j���[�^�[�Ǘ�
        if (isGrounded)
        {
            animator.SetBool("isWalking", move != 0f);
        }
        

        // ��������ύX���鏈��
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        // �W�����v
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("jumpTrigger");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0);
            isGrounded = false;
        }
    }

    // ������ς���֐�
    void Flip()
    {
        facingRight = !facingRight;
        float yRotation = facingRight ? 140f : -140f;
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }


    // �ڒn����itag���p�j
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
        // Z���ʒu�����0�ɂ���
        Vector3 pos = transform.position;
        pos.z = 0;
        transform.position = pos;
    }
}
