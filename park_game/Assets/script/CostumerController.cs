using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumerController : MonoBehaviour
{
    // Start is called before the first frame update
    int moveFlag;
    GameObject bubble;
    int speed = 5;
    int moveTime = 0;
    public int parkNum = -1;
    private void Awake()
    {
        bubble = transform.GetChild(0).gameObject;
        mute();
    }
    void Start()
    {
        moveFlag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveFlag == 1)
        {
            //enter the hotel
            //Vector3 direc = Vector3.Normalize(new Vector3(1, 0, 6));
            //transform.Translate(direc * Time.deltaTime * speed);
            //moveTime--;
            //if (moveTime == 0) moveFlag = 0;

            Vector3 target = new Vector3(80.9f, 0f, -13.6f);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            float dis = Vector3.Distance(target, transform.position);
            if (dis <= 1)
            {
                moveFlag = 0;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        if (moveFlag == 2)
        {
            //get out of the hotel
            Vector3 target = new Vector3(67.6f, 0f, 4.9f - 5 * Manager.Instance.emptyPos);
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            float dis = Vector3.Distance(target, transform.position);
            if (dis <= 1)
            {
                moveFlag = 0;
                speak(parkNum);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    public void EnterHotel()
    {
        Debug.Log("costumer: enter hotel");
        transform.eulerAngles = new Vector3(0, 15, 0);
        moveFlag = 1;
        mute();
        moveTime = 150;
    }

    public void LeaveHotel()
    {
        Debug.Log("costumer: leave hotel");
        int waitCnt = Manager.Instance.leaveWait;

        transform.eulerAngles = new Vector3(0, 330, 0);
        moveTime = 160 - waitCnt * 5;
        moveFlag = 2;
        bubble.SetActive(false);

    }

    public void speak(int p)
    {
        Debug.Log("speak:" + p);
        if(p >= 0 && p <= 12)
        {
            if (p >= 10)
            {
                if (p == 10)
                {
                    bubble.SetActive(true);
                    bubble.transform.GetChild(10).gameObject.SetActive(true);
                    bubble.transform.GetChild(11).gameObject.SetActive(true);
                }
            }
            else
            {
                bubble.SetActive(true);
                bubble.transform.GetChild(p).gameObject.SetActive(true);
            }
        }


    }

    public void mute()
    {
        for (int i = 0; i <= 11; i++)
        {
            bubble.transform.GetChild(i).gameObject.SetActive(false);
        }
        bubble.SetActive(false);
    }

    public void setParkNum(int p)
    {
        parkNum = p;
    }

    public void angry()
    {
        Manager.Instance.money -= 10;
        speak(0);
    }
}
