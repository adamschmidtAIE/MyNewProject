using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiStuff : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }









    public void Jump(float height)
    {
        rb.AddForce(Vector3.up * height, ForceMode.Impulse);
    }







    public void ChangeSize(float size)
    {
        rb.transform.localScale = Vector3.one * size;
    }
}
