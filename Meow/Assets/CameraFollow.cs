using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ��� (�÷��̾�)
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ� ����

    private Vector3 offset; // ī�޶�� �÷��̾� ������ �ʱ� �Ÿ�

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position =  new Vector3(smoothedPosition.x, 0, smoothedPosition.z);
    }
}
