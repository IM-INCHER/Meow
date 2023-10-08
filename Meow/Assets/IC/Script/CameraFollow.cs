using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상 (플레이어)
    public float smoothSpeed = 0.125f; // 카메라 이동 속도 조절
    public float limitX;

    public bool ismove = false;

    private Vector3 offset; // 카메라와 플레이어 사이의 초기 거리

    void Start()
    {
        offset = transform.position - new Vector3(0, target.position.y, target.position.z);
    }

    void Update()
    {

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, 0, smoothedPosition.z);
        ismove = true;

        if (this.transform.position.x < 0)
        {
            this.transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
            ismove = false;
        }
        if (this.transform.position.x > limitX)
        {
            this.transform.position = new Vector3(limitX, this.transform.position.y, this.transform.position.z);
            ismove = false;
        }
    }
}
