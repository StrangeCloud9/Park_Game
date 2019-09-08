using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car3dController : MonoBehaviour
{
    float speedForce = 40f;
    float torqueForce = -20f;
    // Start is called before the first frame update
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        speedForce = 40f;
        torqueForce = -20f;
    }
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {


    }

    void FixedUpdate() {
        if (Manager.Instance.control == "car")
        {

            rb.velocity = ForwardVelocity();



            if (Input.GetButton("Accelerate"))
            {
               // Debug.Log("car:accelerate!!!!");
                rb.AddForce(transform.forward * speedForce);
                //transform.Translate(Vector3.forward * Time.deltaTime);
            }

            if (Input.GetButton("Brakes"))
            {
                //Debug.Log("car:brakes!!");
                rb.AddForce(transform.forward * -speedForce / 2f);
            }

            float tf = Mathf.Lerp(0, torqueForce, rb.velocity.magnitude / 200);

            if (Vector3.Dot(transform.forward, rb.velocity) > 0)
                rb.angularVelocity = -Input.GetAxis("Horizontal") * transform.up * tf;
            else
                rb.angularVelocity = Input.GetAxis("Horizontal") * transform.up * tf;
        }
    }

    Vector3 ForwardVelocity()
    {
        return transform.forward * Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.forward);
    }

    Vector3 RightVelocity()
    {
        return transform.right * Vector3.Dot(GetComponent<Rigidbody>().velocity, transform.right);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name != "Player")
        {
            Debug.Log("collision happens!!!!:" + col.gameObject.name);
            Manager.Instance.money -= 10;
        }
    }
}
