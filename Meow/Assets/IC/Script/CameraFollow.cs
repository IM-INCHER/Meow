using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ��� (�÷��̾�)
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ� ����
    public float limitX;
    public float limitY;

    public bool ismove = false;

    private Vector3 offset; // ī�޶�� �÷��̾� ������ �ʱ� �Ÿ�

    void Start()
    {
        offset = transform.position - new Vector3(0, target.position.y, target.position.z);
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = new Vector3(
            Mathf.Clamp(smoothedPosition.x, 0, limitX), // X��ǥ ����
            Mathf.Clamp(smoothedPosition.y, 0, limitY), // Y��ǥ ����
            transform.position.z
        );
        ismove = true;

        if (transform.position.x == 0 || transform.position.x == limitX || transform.position.y == 0 || transform.position.y == limitY)
        {
            ismove = false;
        }
    }
}
