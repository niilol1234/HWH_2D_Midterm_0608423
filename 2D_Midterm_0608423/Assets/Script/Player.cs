﻿using UnityEngine;
using UnityEngine.UI;  // 引用 介面 API

public class Player : MonoBehaviour
{
    // 註解

    // 欄位語法 Field - 儲存資料
    // 修飾詞 類型 名稱 （指定 值）；
    // 私人 private 不顯示 （預設值）
    // 公開 public  顯示

    // 類型    四大類型
    // 整數    int
    // 浮點數  float
    // 布林值  bool true 是、false 否
    // 字串    string
    [Header("等級")]
    public int lv = 1;
    [Header("移動速度"), Range(0, 300)]
    public float speed = 10.5f;
    [Header("角色是否死亡")]
    public bool isDead = false;
    [Header("角色名稱"), Tooltip("這是角色的名稱")]
    public string pName = "玩家";
    [Header("虛擬搖桿")]
    public FixedJoystick joystick;
    [Header("變形元件")]
    public Transform tra;
    [Header("動畫元件")]
    public Animator ani;
    [Header("偵測範圍")]
    public float rangeAttack = 2.5f;

    // 事件：繪製圖示
    private void OnDrawGizmos()
    {
        // 指定顏色 (紅，綠，藍，透明度)
        Gizmos.color = new Color(0, 0, 1, 0.2f);
        // 繪製圖案 球體(中心點，半徑)
        Gizmos.DrawSphere(transform.position, rangeAttack);
    }

    // 方法語法 Method - 儲存複雜的程式區塊或演算法
    // 修飾詞 類型 名稱 () { 程式區塊或演算法 }
    // void 無類型

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        float h = joystick.Horizontal;
        //float v = joystick.Vertical;

        // 變形元件.位移(水平 * 速度 * 一幀的時間，垂直 * 速度 * 一幀的時間，0)
        tra.Translate (h * speed * Time.deltaTime, 0, 0);

        ani.SetFloat("水平", h);
        //ani.SetFloat("垂直", v);
    }

    public void Attack()
    {
        print("攻擊");

        // 2D 物理 圓形碰撞(中心點，半徑，方向，距離，圖層)
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, rangeAttack, -transform.up, 0, 1 << 8);


        // 如果 碰到的物件存在 並且 碰到的物件 標籤 為 道具 就 取得道具腳本並呼叫掉落道具
        if (hit && hit.collider.tag == "道具") hit.collider.GetComponent<Item>().DropProp();
    }

    private void Hit()
    {

    }

    private void Dead()
    {

    }
    // 事件 - 特定時間會執行的方法
    // 開始事件：播放後執行一次
    private void Start()
    {
        

    }

    // 更新事件：大約一秒執行六十次 60FPS
    private void Update()
    {   
        // 呼叫方式
        // 方法名稱();
        Move();
    }

    [Header("牧草數量")]
    public Text textGrass;

    private int grass;

    // 觸發事件 - 進入：兩個物件必須有一個勾選 Is Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.tag == "牧草")
        {
            grass++;
            Destroy(collision.gameObject);
            textGrass.text = "牧草：" + grass;
        }
    }
}
