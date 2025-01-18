using System;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    // [SerializeField] [Range(0,1)] float movementFactor;  // Inspector 창에서 직접 확인 가능
    float movementFactor;
    [SerializeField] float period = 2f;   
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(period <=Mathf.Epsilon) {return;}
        float cycles = Time.time / period;  // 시간에 따라 계속 증가

        const float tau = Mathf.PI * 2;     // const : 일정한 값 -> 6.283...의 일정한 값
        float rawSinWave = Mathf.Sin(cycles * tau); // -1 에서 1로 움직임

        movementFactor = (rawSinWave + 1f) / 2f;    // 깔끔하게 0에서 1로 움직이게 계산함.

        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }
}
