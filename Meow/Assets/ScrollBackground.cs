using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    public float resetPosition = -10.0f;
    public float startPosition = 10.0f;

    void Update()
    {
        // ��� �̹����� �������� ��ũ��
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // �̹����� ���� ��ġ�� ���ư��� �ٽ� ���������� �̵�
        if (transform.position.x <= resetPosition)
        {
            Vector2 newPos = new Vector2(startPosition, transform.position.y);
            transform.position = newPos;
        }
    }
}
