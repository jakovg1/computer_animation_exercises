using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    [SerializeField]
    public float unitMovementCoefficient = 5f;



    private Vector3 leftDirectionVelocity;
    private Vector3 rightDirectionVelocity;

    private Vector3 upDirectionVelocity;
    private Vector3 downDirectionVelocity;

    private float timeElapsedCoefficient = 0f;
    private bool isDestroyed = false;




    // Start is called before the first frame update
    void Start()
    {
        leftDirectionVelocity = new Vector3(0, 0, unitMovementCoefficient);
        rightDirectionVelocity = new Vector3(0, 0, -unitMovementCoefficient);

        upDirectionVelocity = new Vector3(0, unitMovementCoefficient, 0);
        downDirectionVelocity = new Vector3(0, -unitMovementCoefficient, 0);

        GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);


    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsedCoefficient <= 70)
        {
            timeElapsedCoefficient += Time.deltaTime;
        }

        if (isDestroyed) return;


        //var decayVelocity = - GetComponent<Rigidbody>().velocity * 0.99f;
        var decayVelocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().velocity = decayVelocity;

        float rotationAngle = 30;
        float slerpTParam = 0.1f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, slerpTParam * 1.5f);

        if (Input.GetKey(leftKey))
        {
            GetComponent<Rigidbody>().velocity += leftDirectionVelocity;
            //GetComponent<Rigidbody>().AddForce(leftDirectionVelocity, ForceMode.VelocityChange);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -rotationAngle, 0), slerpTParam);

        }

        if (Input.GetKey(rightKey))
        {
            GetComponent<Rigidbody>().velocity += rightDirectionVelocity;
            //GetComponent<Rigidbody>().AddForce(rightDirectionVelocity, ForceMode.VelocityChange);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, rotationAngle, 0), slerpTParam);
        }

        if (Input.GetKey(upKey))
        {
            GetComponent<Rigidbody>().velocity += upDirectionVelocity;
            //GetComponent<Rigidbody>().AddForce(upDirectionVelocity, ForceMode.VelocityChange);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotationAngle), slerpTParam);
        }

        if (Input.GetKey(downKey))
        {
            GetComponent<Rigidbody>().velocity += downDirectionVelocity;
            //GetComponent<Rigidbody>().AddForce(downDirectionVelocity, ForceMode.VelocityChange);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -rotationAngle), slerpTParam);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isDestroyed = true;
        GetComponent<Rigidbody>().AddRelativeTorque(Random.onUnitSphere * 5f, ForceMode.Impulse);

        //transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(0.1f, 0.1f, 0.1f);
        transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        DestroyObject(transform.GetChild(1).gameObject);
        DestroyObject(transform.GetChild(2).gameObject);
        DestroyObject(transform.GetChild(3).gameObject);


        //Instantiate(inputObject, transform.position + randPosition, Quaternion.identity);
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
