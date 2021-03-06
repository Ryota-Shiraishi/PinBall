﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour
{
    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;

    //ゲーム画面の横幅
    private float screenWidth;

    // Start is called before the first frame update
    void Start()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);

        //ゲーム画面の横幅を取得
        this.screenWidth　=Screen.width/2;
    }

    // Update is called once per frame
    void Update()
    {
        //タッチパッドでの操作に対応しているか判定
        if (Input.touchSupported)
        {
            //タッチしているか判定
            switch (Input.touchCount)
            {
                case 0:
                    SetAngle(this.defaultAngle);
                    break;
                case 1:
                    Touch touch = Input.GetTouch(0);
                    float touchPos = touch.position.x;
                    if (touchPos == this.screenWidth || (touchPos < this.screenWidth & tag == "LeftFripperTag")|| (touchPos > this.screenWidth & tag == "RightFripperTag"))
                    {
                        SetAngle(this.flickAngle);
                    }
                    break;
                default:
                    SetAngle(this.flickAngle);
                    break;
            }
        }
        
        //左矢印キーを押した時左フリッパーを動かす        
        if ((Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A)) && tag == "LeftFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //右矢印キーを押した時右フリッパーを動かす
        if ((Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D)) && tag == "RightFripperTag")
        {
            SetAngle(this.flickAngle);
        }
        //下矢印キーを押した時左右のフリッパーを動かす
        if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
        {
            SetAngle(this.flickAngle);
        }

        //矢印キー離された時フリッパーを元に戻す
        if ((Input.GetKeyUp(KeyCode.LeftArrow)|| Input.GetKeyUp(KeyCode.A)) && tag == "LeftFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if ((Input.GetKeyUp(KeyCode.RightArrow)|| Input.GetKeyUp(KeyCode.D)) && tag == "RightFripperTag")
        {
            SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)|| Input.GetKeyUp(KeyCode.S))
        {
            SetAngle(this.defaultAngle);
        }
        
    }

    //フリッパーの傾きを設定
    public void SetAngle(float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}
