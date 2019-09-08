using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Manager.Instance.control == "player")
       {
            Rigidbody rb = GetComponent<Rigidbody>();

            int vel = 20;
            Vector3 up = new Vector3(0, 0, 1);
            Vector3 down = new Vector3(0, 0, -1);
            Vector3 left = new Vector3(-1, 0, 0);
            Vector3 right = new Vector3(1, 0, 0);
            if (Input.GetButton("W") && Input.GetButton("A"))
            {
                rb.velocity = (up + left) * 15;
                transform.eulerAngles = new Vector3(0, 315, 0);
            } 
            else if (Input.GetButton("W") && Input.GetButton("D"))
            {
                rb.velocity = (up + right) * 15;
                transform.eulerAngles = new Vector3(0, 45, 0);
            }
            else if (Input.GetButton("S") && Input.GetButton("A"))
            {
                rb.velocity = (down + left) * 15;
                transform.eulerAngles = new Vector3(0, 225, 0);
            }
            else if (Input.GetButton("S") && Input.GetButton("D"))
            {
                rb.velocity = (down + right) * 15;
                transform.eulerAngles = new Vector3(0, 135, 0);
            }
            else if (Input.GetButton("W"))
            {
                rb.velocity = up * vel;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Input.GetButton("S"))
            {
                rb.velocity = down * vel;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Input.GetButton("A"))
            {
                rb.velocity = left * vel;
                transform.eulerAngles = new Vector3(0, 270, 0);

            }
            else if (Input.GetButton("D"))
            {
                rb.velocity = right * vel;
                transform.eulerAngles = new Vector3(0, 90, 0);
            }


        }
    }

}
