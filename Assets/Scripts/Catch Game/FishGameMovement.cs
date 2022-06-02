using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGameMovement : MonoBehaviour
{
    public Vector2 moveSpeedRange;
    public float changeInterval;
    public float floatSpeed;

    private float moveSpeed;
    private float yPos;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = moveSpeedRange.x;
        yPos = transform.position.y;
        StartCoroutine(ChangeSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * moveSpeed * Time.deltaTime);
        yPos += Mathf.Sin(Time.timeSinceLevelLoad * 4f) * floatSpeed;
        transform.localPosition = Vector3.up * yPos;
    }

    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeInterval);
            moveSpeed = Random.Range(moveSpeedRange.x, moveSpeedRange.y);
        }
    }
}
