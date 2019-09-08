using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("[State]")]
    public bool delay;
    public bool respawn;
    public int unitSize = 1;

    public int state = 0;   //0 = pause, 1 = inGame

    [Header("[Prefab]")]
    public GameObject red;
    public GameObject blue;

    [Header("[List]")]
    public List<Transform> redTeamRespawn = new List<Transform>();
    public List<Transform> blueTeamRespawn = new List<Transform>();

    public List<GameObject> redTeamCount = new List<GameObject>();
    public List<GameObject> blueTeamCount = new List<GameObject>();
    
    [Header("[Effect]")]
    public ParticleSystem exp;

    [Header("[UI]")]
    public GameObject panal;
    public GameObject reload;
    public Text titleTxt;
    public Text introTxt;
    public Text stateTxt;

    public Button leftBtn;
    public Button rightBtn;
    
    void Start()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        delay = true;
        respawn = true;
        panal.SetActive(true);
    }
    
    void Update()
    {
        if (state == 1)
        {
            if (redTeamCount.Count < unitSize && delay)
            {
                //Instantiate(red, transform.position, Quaternion.Euler(0, Random.Range(170, 190), 0));
                redTeamCount.Add(Instantiate(red, redTeamRespawn[Random.Range(0, redTeamRespawn.Count)].position, Quaternion.Euler(0, 180 + Random.Range(-10, 10), 0)));
                //print("add" + redTeamCount.Count);
            }

            if (blueTeamCount.Count < unitSize && respawn)
            {

                leftBtn.interactable = true;
                rightBtn.interactable = true;
            }
            else
            {
                leftBtn.interactable = false;
                rightBtn.interactable = false;
            }
        }
        
    }

    public void BlueRespawnBtn(int button)
    {
            if (button == 0)
            {
                blueTeamCount.Add(Instantiate(blue, blueTeamRespawn[0].position, Quaternion.Euler(0, Random.Range(-10, 10), 0)));
            }
            else if (button == 1)
            {
                blueTeamCount.Add(Instantiate(blue, blueTeamRespawn[1].position, Quaternion.Euler(0, Random.Range(-10, 10), 0)));
            }
        
    }

    public void GameStart()
    {
        state = 1;
        panal.SetActive(false);
    }

    public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
