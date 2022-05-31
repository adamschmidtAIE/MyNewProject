using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public Transform rodTip;
    public Transform lake;
    public float reelSpeed;
    public FishingControls fishingControls;
    public LayerMask notWater;

    private Rigidbody rb;
    private LineRenderer line;
    private bool cast;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        line.SetPositions(new Vector3[] { transform.position, rodTip.position });

        if (cast)
        {
            Vector3 position = transform.position;
            position.y = lake.position.y;
            transform.position = position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Water")
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            cast = true;
            if (Physics.CheckSphere(transform.position, 2, notWater))
            {
                fishingControls.ReturnBobber();
                cast = false;
            }
        }
        else
        {
            fishingControls.ReturnBobber();
        }
    }

    public void ReelIn()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, rodTip.position, reelSpeed * Time.deltaTime);

        if (Physics.CheckSphere(transform.position, 2, notWater))
        {
            fishingControls.ReturnBobber();
            cast = false;
        }
    }
}
