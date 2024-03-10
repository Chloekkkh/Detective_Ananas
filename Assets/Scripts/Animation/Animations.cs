using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [Header("Float Settings")]
    public float floatSpeed = 1.0f; // 浮动速度
    public float floatHeight = 0.5f; // 浮动高度
    public float offsetFactor = 0.0f; // 浮动偏移

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        FloatObject();
    }

    void FloatObject()
    {
        // 使用Sin函数创建一个周期性的浮动效果
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed * offsetFactor) * floatHeight;

        // 将物体位置设置为新的位置
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }  
}
