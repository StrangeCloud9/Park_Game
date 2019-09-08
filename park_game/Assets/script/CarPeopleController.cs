using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPeopleController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator ani;
    GameObject costumer;
    GameObject car;
    CostumerController costumerController;

    public string state;
    public float timer;
    public int parkNum;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        ani.enabled = false;
        car = transform.GetChild(0).gameObject;
        //car.transform.GetComponent<Collider>().enabled = false;

        costumer = transform.GetChild(1).gameObject;
        costumerController =
                        (CostumerController)costumer.GetComponent(typeof(CostumerController));
        car.transform.GetComponent<Collider>().enabled = false;
        car.GetComponent<Car3dController>().enabled = false;
        state = "Idle";
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(state == "EnterHotel")
        {
            timer -= Time.deltaTime;
            if(timer <= 0.0f && Manager.Instance.leaveWait < 3)
            {
                LeaveHotel();
            }
        }

        if (state == "Coming" &&
            timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                //be angry 
                costumerController.angry();
            }
        }

        if (state == "LeaveHotel" &&
            timer > 0.0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                //be angry 
                costumerController.angry();
            }
        }
        //if(leaveTimer)
    }

    public void Coming()
    {
        Debug.Log("car coming animation");
        state = "Coming";
        car.transform.GetComponent<Collider>().enabled = true;
        Rigidbody carRb = car.GetComponent<Rigidbody>();
        carRb.isKinematic = true;

        do
        {
            parkNum = Random.Range(1, 10);
            costumerController.setParkNum(parkNum);
        } while (Manager.Instance.occupy[parkNum] == true);

        Manager.Instance.occupy[parkNum] = true;
        Manager.Instance.isComingWait = true;
        timer = 18.0f;
        costumerController.mute();
        ani.enabled = true;
        ani.Play("carComing");

    }

    public void EnterHotel()
    {
        state = "EnterHotel";
        ani.enabled = false;
        timer = 15.0f;
        Manager.Instance.isComingWait = false;
        costumerController.EnterHotel();
    }
    public void LeaveHotel()
    {
        state = "LeaveHotel";
        timer = 20.0f;
        Manager.Instance.leaveWait++;
        Manager.Instance.emptyPos++;
        if (Manager.Instance.emptyPos == 4)
            Manager.Instance.emptyPos = 1;
        costumerController.LeaveHotel();
    }

    public void Leaving()
    {
        Debug.Log("cp controller: car leave");
        state = "Idle";
        car.transform.eulerAngles = new Vector3(0, 0, 0);
        car.transform.GetComponent<Collider>().enabled = false;
        Manager.Instance.leaveWait--;
        Manager.Instance.occupy[parkNum] = false;
        Manager.Instance.money += 10;

        ani.enabled = true;
        ani.Play("carLeaving");
    }

    public void driveCar()
    {
        Rigidbody carRb = car.GetComponent<Rigidbody>();
        car.GetComponent<Car3dController>().enabled = true;
        carRb.isKinematic = false;
    }

    public void stopCar()
    {
        Rigidbody carRb = car.GetComponent<Rigidbody>();
        car.GetComponent<Car3dController>().enabled = false;
        carRb.isKinematic = true;
    }

    public GameObject getCar()
    {
        return car;
    }

    public void speak(int p)
    {
        costumerController.speak(p);
    }

    public string getState()
    {
        return state;
    }

    public void carWaiting()
    {
        Debug.Log("car is waiting");
        costumerController.speak(parkNum);
    }

    public void carPicking()
    {
        Debug.Log("picking up the car");
        costumerController.speak(parkNum);
    }
    
}
