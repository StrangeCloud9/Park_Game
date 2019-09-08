using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheck : MonoBehaviour
{
    private bool occupied;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        occupied = false;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger: exit");
        occupied = false;
    }

    private void OnTriggerStay(Collider other)
    {

        if (occupied == false)
        {
            occupied = true;
            Debug.Log("Trigger: Stay");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger: Enter");

    }

}
