
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Player : MonoBehaviour
{
   
    //Rigidbodyを変数に入れる
    Rigidbody rb;
    //移動スピード
    public float speed = 3f;
    //ジャンプ力
    public float thrust = 100;
    //Animatorを入れる変数
    public Animator animator;
    public VRIK vrik;
  
    //ユニティちゃんの位置を入れる
    Vector3 playerPos;
    Vector3 pos;
      
    //地面に接触しているか否か
    bool ground;
    int a;
   
   
   public VRTest vRTest;
    public ControllerInputManager ControllerInput;
    void Start()
    {
        //Rigidbodyを取得
        rb = GetComponent<Rigidbody>();
        //ユニティちゃんのAnimatorにアクセスする
        animator = GetComponent<Animator>();
       
        vrik = GetComponent<VRIK>();

        //ユニティちゃんの現在より少し前の位置を保存
        playerPos = transform.position;
    }

    void Update()
    {
        if (ground && ControllerInput.l == false)
        {
            //ユニティちゃんの現在より少し前の位置を保存
            playerPos = transform.position;

            if (vRTest.v > 0.05f && (vRTest.x>0.05f||vRTest.y>0.05f))
            {
                vrik.enabled = false;

                transform.rotation =vRTest.head.transform.rotation;
                transform.position =new Vector3( vRTest.head.transform.position.x,0, vRTest.head.transform.position.z);

                //走るアニメーションを再生
                animator.SetBool("Running", true);
            }
            else
            {
                //ベクトルの長さがない＝移動していない時は走るアニメーションはオフ
                animator.SetBool("Running", false);
                vrik.enabled = true; 
            }                          

            //スペースキーやゲームパッドの3ボタンでジャンプ
            if (Input.GetButton("Jump"))
            {
                transform.rotation = vRTest.head.transform.rotation;
                transform.position = new Vector3(vRTest.head.transform.position.x, 0, vRTest.head.transform.position.z);
                vrik.enabled = false;
                //thrustの分だけ上方に力がかかる
                rb.AddForce(transform.up * thrust);

                //速度が出ていたら前方と上方に力がかかる
                if (rb.velocity.magnitude > 0)
                    rb.AddForce(transform.forward * thrust + transform.up * thrust);
            }
            else if (vRTest.w>0.05f && (vRTest.x > 0.05f || vRTest.y > 0.05f))
            {
                transform.rotation = vRTest.head.transform.rotation;
                transform.position = new Vector3(vRTest.head.transform.position.x, 0, vRTest.head.transform.position.z);
                vrik.enabled = false;
                //thrustの分だけ上方に力がかかる
                rb.AddForce(transform.up * thrust);
                //速度が出ていたら前方と上方に力がかかる
                if (rb.velocity.magnitude > 0)
                    rb.AddForce((transform.forward * thrust) + (transform.up * thrust));
            }          
        }
    }

    //Planに触れている間作動
    void OnCollisionStay(Collision col)
    {
        ground = true;
        //ジャンプのアニメーションをオフにする
        animator.SetBool("Jumping", false);
        if (a == 1)
        {
            vrik.enabled = true;
            a = 0;
                   
        }
    }

    //Planから離れると作動
    void OnCollisionExit(Collision col)
    {
        ground = false;
        //ジャンプのアニメーションをオンにする
        animator.SetBool("Jumping", true);
        a = 1;
    }
}