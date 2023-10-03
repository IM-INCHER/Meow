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
        // 배경 이미지를 왼쪽으로 스크롤
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // 이미지가 일정 위치로 돌아가면 다시 시작점으로 이동
        if (transform.position.x <= resetPosition)
        {
            Vector2 newPos = new Vector2(startPosition, transform.position.y);
            transform.position = newPos;
        }
    }
}
