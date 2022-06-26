using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControler : MonoBehaviour

{
    Rigidbody rb;
    Vector2 lastMousePos = Vector2.zero;
    public float thrust = 100f;
    [SerializeField] float vitesse = 0.02f;
    [SerializeField] float wallDistance = 5f;
    [SerializeField] float minCamDistance = 4f;
    [SerializeField] Material[] materials;
    public float speed = 0f;
    public GameObject canvas;
    public GameObject winPanel;

    IEnumerator Win(float delayTime)
    {

        Debug.Log("You won");
        thrust = 0;
        speed = 0;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(delayTime);

        winPanel.SetActive(true);

    }

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaPos = Vector2.zero;
        if (Input.GetMouseButton(0))
        {
            Vector2 currentMousePos = Input.mousePosition;
            if (lastMousePos == Vector2.zero)
                lastMousePos = currentMousePos;

            deltaPos = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;
            Vector3 force = new Vector3(deltaPos.x, 0f, deltaPos.y) * thrust;
            rb.AddForce(force);
        }
        else
        {
            lastMousePos = Vector2.zero;
        }

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
        rb.MovePosition(rb.position + Vector3.forward * speed * Time.fixedDeltaTime);
        Camera.main.transform.position += Vector3.forward * speed * Time.fixedDeltaTime; 
    }
     IEnumerator Die(float delayTime)
    {
        Debug.Log("You're dead");
        speed = 0;
        thrust = 0;

        yield return new WaitForSeconds(delayTime);

        SceneManager.LoadScene(0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Evil")
            StartCoroutine(Die(2));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
            canvas.gameObject.SetActive(true);
            StartCoroutine(Win(2));
        }
            
    }

    public void setMaterial(int index)
    {
        if (index < materials.Length)
            GetComponent<Renderer>().material = materials[index];
    }


}
