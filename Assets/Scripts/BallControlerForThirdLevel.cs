using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControlerForThirdLevel : MonoBehaviour
{
    Rigidbody rb;
    public float thrust = 100f;
    [SerializeField] float vitesse = 0.02f;
    [SerializeField] float wallDistance = 5f;
    [SerializeField] float minCamDistance = 4f;
    [SerializeField] Material[] materials;
    public float speed = 5f;
    public GameObject winPanel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("right"))
        {
            transform.Translate(vitesse, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            transform.Translate(-vitesse, 0, 0);
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        if (pos.z < Camera.main.transform.position.z + minCamDistance)
        {
            pos.z = Camera.main.transform.position.z + minCamDistance;
        }



        if (pos.x < -wallDistance)
        {
            pos.x = -wallDistance;
        }

        else if (pos.x > wallDistance)
        {
            pos.x = wallDistance;
        }
        transform.position = pos;
    }

   void FixedUpdate()
    {
        //Move the ball forward
        rb.MovePosition(rb.position + Vector3.forward * speed * Time.fixedDeltaTime);

        // Move the Camera forward
        Camera.main.transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
    }

    IEnumerator Win(float delayTime)
    {
        // Do stuff before waiting 
        thrust = 0;
        speed = 0;
        rb.velocity = Vector3.zero;

        //Wait some time 
        yield return new WaitForSeconds(delayTime);

        // Do other stuff after waiting

        // Activate the pannel
        winPanel.SetActive(true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
            StartCoroutine(Win(0.5f));
    }

    IEnumerator Die(float delayTime)
    {
        Debug.Log("You're dead");
        speed = 0;
        thrust = 0;

        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Evil")
            StartCoroutine(Die(2));
    }

    public void setMaterial(int index)
    {
        if (index < materials.Length)
            GetComponent<Renderer>().material = materials[index];
    }
}
