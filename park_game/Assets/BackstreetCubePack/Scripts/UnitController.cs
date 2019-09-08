using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stage_Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class UnitController : MonoBehaviour
{
    public GameManager gm;
    public GameObject unit;
    public Rigidbody rb;
    public float power = 1f;
    public int id;
    public bool team;   //false = blue, true = red
    public bool isBoss;
    public bool isMove;

    public Stage_Boundary boundary;

    void Start()
    {
        gm = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
        rb = transform.GetComponent<Rigidbody>();
        if (!isBoss)
        {
            if (team)
            {
                id = gm.redTeamCount.IndexOf(gameObject);
            }
            else
            {
                id = gm.blueTeamCount.IndexOf(gameObject);
            }
        }

        isMove = false;
    }
    
    void Update()
    {
        if (isMove)
        {
            if (rb.velocity.z <= 0)
            {
                rb.velocity = transform.forward * power;
            }

            transform.position = new Vector3
            (
            Mathf.Clamp(transform.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(transform.position.z, boundary.zMin, boundary.zMax)
            );
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isBoss)
        {
            if (team)
            {
                if (collision.gameObject.tag == "BlueBox")
                {
                    Instantiate(gm.exp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                }else if (collision.gameObject.tag == "RedBox")
                {
                    transform.position = new Vector3(0,0,0);
                }
            }
            else
            {
                if (collision.gameObject.tag == "RedBox")
                {
                    Instantiate(gm.exp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                }
                else if (collision.gameObject.tag == "BlueBox")
                {
                    transform.position = new Vector3(0, 0, 0);
                }
            }
        }

        if (isBoss)
        {
            if (collision.gameObject.tag == "Floor")
            {
                if (team)
                {
                    //red fail
                    Instantiate(gm.exp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    print("*****************Blue Win*****************");

                    //UI
                    gm.titleTxt.text = "You Win!!";
                    gm.introTxt.enabled = false;
                    gm.stateTxt.text = "Reload";
                    gm.reload.SetActive(true);
                    gm.panal.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    //blue fail
                    Instantiate(gm.exp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                    print("*****************Red Win*****************");
                    gm.titleTxt.text = "You fail!!";
                    gm.introTxt.enabled = false;
                    gm.stateTxt.text = "Reload";
                    gm.reload.SetActive(true);
                    gm.panal.SetActive(true);
                    Time.timeScale = 0;
                }
                
            }
        }

        if (!isBoss)
        {
            if (collision.gameObject.tag == "Floor")
            {
                isMove = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            Instantiate(gm.exp, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            transform.forward = -transform.forward;
            transform.Rotate(0, Random.Range(-10, 10), 0);
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            gm.respawn = false;
        }

        if (other.gameObject.tag == "Delay")
        {
            gm.delay = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            gm.respawn = true;
        }

        if (other.gameObject.tag == "Delay")
        {
            gm.delay = true;
        }
    }

    public void CharacterTestBtn()
    {
        //Rigidbody rb = unit.GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * power;
        //rb.AddForce(Vector3.forward * power, ForceMode.Acceleration);
        gm.redTeamCount.RemoveAt(1);
    }
}
