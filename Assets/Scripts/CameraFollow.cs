using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // 跟随的目标对象
    public Vector3 velocity; // 速度
    public Vector3 offset;   // 偏移量
    [Range(0,1)]
    public float smoothTime = 0.3f; // 平滑时间

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void LateUpdate()
    {
        // 相机的位置
        Vector3 targetPos = target.position+offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
