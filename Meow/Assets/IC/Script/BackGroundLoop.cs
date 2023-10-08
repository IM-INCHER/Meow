using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    public float scrollSpeed = 1.0f; // ��� ��ũ�� �ӵ�
    public float loopOffset = 19.0f; // ��� ���� ����

    private Vector3 initialPosition; // �ʱ� ��ġ

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // ī�޶��� ���� ��ġ�� �̵����� ���
        float cameraX = Camera.main.transform.position.x;
        float deltaX = cameraX - initialPosition.x;

        // ����� ���η� ��ũ��
        transform.position = new Vector3(initialPosition.x + deltaX * scrollSpeed, transform.position.y, transform.position.z);

        // ��� ���� ó��
        //if (transform.position.x < cameraX - loopOffset)
        //{
        //    transform.position = new Vector3(initialPosition.x + loopOffset, transform.position.y, transform.position.z);
        //}

        //if (transform.position.x <= loopOffset * -1 || transform.position.x >= loopOffset)
        //{
        //    Vector2 newPos = new Vector2(0, transform.position.y);
        //    transform.position = newPos;
        //    initialPosition = Camera.main.transform.position;

        //}
    }
}
