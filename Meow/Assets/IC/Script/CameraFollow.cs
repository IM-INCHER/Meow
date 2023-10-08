using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ī�޶� ���� ��� (�÷��̾�)
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ� ����
    public float limitX;

    public bool ismove = false;

    private Vector3 offset; // ī�޶�� �÷��̾� ������ �ʱ� �Ÿ�

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
