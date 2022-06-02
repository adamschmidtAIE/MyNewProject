using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishWorldMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
