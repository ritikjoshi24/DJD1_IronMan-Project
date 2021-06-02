using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrateScore : MonoBehaviour
{
    public static int crateValue;
    Text crateScore;
    [SerializeField] TheSceneManager theSceneManager;

    void Start()
    {
        crateScore = GetComponent<Text>();
        crateValue = 0;
    }
    // Update is called once per frame
    void Update()
    {
        crateScore.text = crateValue +" /3 crates";
        if(crateValue == 3) 
        {
            theSceneManager.LoadWinScreen();
        }
    }
}
