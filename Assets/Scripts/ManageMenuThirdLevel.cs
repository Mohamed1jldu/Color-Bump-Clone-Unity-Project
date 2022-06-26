using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMenuThirdLevel : MonoBehaviour
{
    public float chosenSpeed = 5f;
    // Start is called before the first frame update
    public void StartGame()
    {
        GameObject.Find("Ball").GetComponent<BallControlerForThirdLevel>().speed = chosenSpeed;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
