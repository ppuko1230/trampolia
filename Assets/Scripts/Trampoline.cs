using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーのみ反応させたい場合（Tagなどで判定）
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 上方向に加速度を与える（ジャンプ力上書きもOK）
                rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            }
        }
    }

}
