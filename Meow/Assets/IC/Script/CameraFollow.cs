using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상 (플레이어)
    public float smoothSpeed = 0.125f; // 카메라 이동 속도 조절
    public float limitX;
    public float limitY;

    public bool ismove = false;

    private Vector3 offset; // 카메라와 플레이어 사이의 초기 거리

    void Start()
    {
        offset = transform.position - new Vector3(0, target.position.y, target.position.z);
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = new Vector3(
            Mathf.Clamp(smoothedPosition.x, 0, limitX), // X좌표 제한
            Mathf.Clamp(smoothedPosition.y, 0, limitY), // Y좌표 제한
            transform.position.z
        );
        ismove = true;

        if (transform.position.x == 0 || transform.position.x == limitX || transform.position.y == 0 || transform.position.y == limitY)
        {
            ismove = false;
        }
    }
}
