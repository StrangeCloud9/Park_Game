﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeScene()
    {
        SceneManager.LoadScene("MainGame");
    }


}
