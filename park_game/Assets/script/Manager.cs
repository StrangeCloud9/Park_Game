using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager Instance 
    {
        get;
        private set;
    }

    public string control = "player";
    public float closeEnoughThr = 10.0f;
    public GameObject player;
    public GameObject carPeople;
    public int drivingIdx = -1;
    GameObject[] cps = new GameObject[8];
    public bool[] occupy = new bool[11];
    public CarPeopleController[] cpControllers = new CarPeopleController[8];

    public bool isComingWait;
    public int leaveWait = 0;
    public int emptyPos = 1;
    public float timer = 5.0f;

    public float gameTime = 120f;
    public int money = 200;
    public Text timeText;
    public Text moneyText;

    private void Awake()
    {
        control = "player";
        if (Instance == null)
        {
            Debug.Log("Instance = this");
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.Find("Player");

        for (int i = 1; i <= 8; i++)
        {
            string CpName = "CarPeople" + i;
            cps[i - 1] = GameObject.Find(CpName);
            cpControllers[i - 1] = 
                (CarPeopleController)cps[i - 1].GetComponent(typeof(CarPeopleController));
        }

        for (int i = 0; i < 11; i++)
        {
            occupy[i] = false;
        }
        carPeople = cps[0];
        timer = 5.0f;


        gameTime = 120f;

    }

    private void FixedUpdate()
    {

        //scheduler
        timer -= Time.deltaTime;
        Debug.Log("timer:" + timer);
        if(timer <= 0.0f)
        {
            timer = 15.0f;
            for(int i = 0; i < 8; i++)
            {
                string state = cpControllers[i].getState();
                if(state == "Idle" && isComingWait == false)
                {
                    cpControllers[i].Coming();
                    break;
                }
            }
        }

        gameTime -= Time.deltaTime;
        timeText.text = ((int)(gameTime)).ToString();

        moneyText.text = ((int)(money)).ToString();

        if(gameTime < 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public int pickNearestCar()
    {
        int ret = -1;
        float dis = 0;
        for(int i = 0; i < 8; i++)
        {
            GameObject car = cpControllers[i].getCar();
            float curDis = Vector3.Distance(car.transform.position, player.transform.position);
            Debug.Log("i:" + i + " dis:" + curDis);
            if (curDis <= 10)
            {
                Debug.Log("find less");
            } else
            {
                Debug.Log("thr:" + 10 + " bigger than curDis:" + curDis);
            }
            if (curDis <= 10
                && (ret == -1 || curDis < dis) )
            {
                dis = curDis;
                ret = i;
            }
        }
        Debug.Log("ret:" + ret + " dis:" + dis);
        Debug.Log(player.transform.position);
        return ret;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {


            if (control == "player")
            {//player gets up the car;
                int cpIdx = pickNearestCar();
                if(cpIdx != -1)
                {
                    //have cars to get on

                    Debug.Log("manager: close enough:" + cpIdx);

                    if (player.transform.position[0] >= 50.0)
                    {
                        Debug.Log("manager: pick up a car");
                        Debug.Log(player.transform.position);
                        cpControllers[cpIdx].EnterHotel();
                    }
                    cpControllers[cpIdx].driveCar();
                    player.SetActive(false);
                    control = "car";
                    drivingIdx = cpIdx;
                }
            }
            else
            {//player gets off the car;
                GameObject car = cpControllers[drivingIdx].getCar();
                player.SetActive(true);
                player.transform.position = car.transform.position;
                Debug.Log("player: get off the car");
                control = "player";

                if (car.transform.position[0] >= 50.0 &&
                    cpControllers[drivingIdx].getState() == "LeaveHotel")
                {
                    //give the car to the costumer.
                    Debug.Log("costumer pick up the car");
                    cpControllers[drivingIdx].Leaving();
                }
                else
                {
                    //normal park
                    cpControllers[drivingIdx].stopCar();
                }
                drivingIdx = -1;
            }

        }

        if (Input.GetKeyDown("1"))
        {
            carPeople = (GameObject)cps[0];
        }

        if (Input.GetKeyDown("2"))
        {
            carPeople = (GameObject)cps[1];
        }

        if (Input.GetKeyDown("3"))
        {
            carPeople = (GameObject)cps[2];
        }

        if (Input.GetKeyDown("4"))
        {
            carPeople = (GameObject)cps[3];
        }

        if (Input.GetKeyDown("q"))
        {
            CarPeopleController cpController =
                    (CarPeopleController)carPeople.GetComponent(typeof(CarPeopleController));
            cpController.Coming();
        }

        if (Input.GetKeyDown("e"))
        {
            CarPeopleController cpController =
                    (CarPeopleController)carPeople.GetComponent(typeof(CarPeopleController));
            cpController.LeaveHotel();
        }

    }

}
