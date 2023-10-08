using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    public float scrollSpeed = 1.0f; // 배경 스크롤 속도
    public float loopOffset = 19.0f; // 배경 루프 간격

    private Vector3 initialPosition; // 초기 위치

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // 카메라의 현재 위치와 이동량을 계산
        float cameraX = Camera.main.transform.position.x;
        float deltaX = cameraX - initialPosition.x;

        // 배경을 가로로 스크롤
        transform.position = new Vector3(initialPosition.x + deltaX * scrollSpeed, transform.position.y, transform.position.z);

        // 배경 루프 처리
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
