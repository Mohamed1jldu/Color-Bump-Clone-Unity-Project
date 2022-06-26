using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControlerSecondScene : MonoBehaviour
{
    [SerializeField] float vitesse = 0.02f;
    Rigidbody rb; 
    public float thrust = 100f;
   [SerializeField] float wallDistance = 5f;
    [SerializeField] float minCamDistance = 4f;
    public float speed = 5f;
    [SerializeField] Material[] materials;
    public GameObject winPanel;
    // Start is called before the first frame update


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 0f;
    }

    IEnumerator Win(float delayTime)
    {

        Debug.Log("You won");
        thrust = 0;
        speed = 0;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(delayTime);

        winPanel.SetActive(true);

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
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Vector3.forward * speed * Time.fixedDeltaTime);
        Camera.main.transform.position += speed * Time.fixedDeltaTime * Vector3.forward;
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

    IEnumerator Die(float delayTime)
    {
        Debug.Log("You're dead");
        speed = 0;
        thrust = 0;

        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Evil")
            StartCoroutine(Die(2));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
            StartCoroutine(Win(0.5f));
    }
    public void setMaterial(int index)
    {
        if (index < materials.Length)
            GetComponent<Renderer>().material = materials[index];
    }


}
