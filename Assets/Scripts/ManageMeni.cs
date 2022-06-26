using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMeni : MonoBehaviour
{
    public float chosenSpeed = 0f;

    // Start is called before the first frame update
    public void StartGame()
    {
        GameObject.Find("Ball").GetComponent<BallControler>().speed = chosenSpeed;
        gameObject.SetActive(false);
    }
}
