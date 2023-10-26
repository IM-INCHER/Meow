using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    public float resetPosition = 10.0f;
    public float startPosition = 10.0f;

    public CameraFollow CF;

    private float cameraMove = 0;

    void Update()
    {
        // 배경 이미지를 왼쪽으로 스크롤
        float deltaX = cameraMove - Camera.main.transform.position.x;

        //if (deltaX > 0) deltaX = 1;
        //else if (deltaX < 0) deltaX = -1;

        Vector3 pos = transform.position;

        transform.Translate(Vector2.left * scrollSpeed * deltaX * -1);

        // 이미지가 일정 위치로 돌아가면 다시 시작점으로 이동
        if (this.transform.localPosition.x <= resetPosition * -1 || this.transform.localPosition.x >= resetPosition)
        {
            Vector2 newPos = new Vector2(startPosition, transform.localPosition.y);
            transform.localPosition = newPos;
        }

        cameraMove = Camera.main.transform.position.x;
    }
}
