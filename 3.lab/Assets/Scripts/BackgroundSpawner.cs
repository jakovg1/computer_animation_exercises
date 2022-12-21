using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject inputObject;

    [SerializeField]
    public float forwardMovementCoefficient = 5f;

    [SerializeField]
    public float objectGenerationRate = 0.05f;

    private List<GameObject> objects = new List<GameObject>();

    private float timeElapsedCoefficient = 0f;

    private float defaultXPosition = 0f;

    void Start()
    {
        for (float i = transform.position.x - 50f; i >= 70; i -= 50f)
        {
            Debug.Log(i);
            defaultXPosition = i;
            CreateNewObject();
        }


        defaultXPosition = 0f;
        InvokeRepeating("CreateNewObject", 0f, 1 / objectGenerationRate);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeElapsedCoefficient <= 70)
        {
            timeElapsedCoefficient += Time.deltaTime;
        }
    }

    void CreateNewObject()
    {
        //Debug.Log(timeElapsedCoefficient);
        forwardMovementCoefficient = 40f + timeElapsedCoefficient * 1.05f;
        var positionFactor = 3f + timeElapsedCoefficient * 0.05f;
        var rotationFactor = 6000 + timeElapsedCoefficient * 0.5f;
        //var rotationFactor = 10000 + timeElapsedCoefficient * 0.5f;


        var scaleFactor = 0.9f - timeElapsedCoefficient * 0.01f;

        var randPosition = new Vector3(-defaultXPosition, Random.Range(-positionFactor, positionFactor), Random.Range(-positionFactor, positionFactor));
        var randRotation = new Vector3(Random.Range(-rotationFactor, rotationFactor) * 1f, 0f, 0f);
        var randScale = new Vector3(0f, Random.Range(0, scaleFactor) * 1f, Random.Range(0, scaleFactor) * 1f);


        var objectClone = Instantiate(inputObject, transform.position + randPosition , Quaternion.identity); //
        objectClone.transform.localScale = objectClone.transform.localScale + randScale;
        //objectClone.transform.localScale =  randScale;

        objectClone.GetComponent<Rigidbody>().AddRelativeTorque(randRotation, ForceMode.Impulse);

        objectClone.GetComponent<Rigidbody>().velocity = new Vector3(-forwardMovementCoefficient, 0, 0);

        var colorValue = 0.7f - 0.01f * timeElapsedCoefficient >= 0f ? 0.7f - 0.01f * timeElapsedCoefficient : 0f;
        //var colorValue = 0f;
        var newColor = new Color(0.7f, colorValue, colorValue);
        for (int i = 0; i < objectClone.transform.childCount; i++)
        {
            objectClone.transform.GetChild(i).GetComponent<Renderer>().material.color = newColor;
        }
        objectClone.GetComponent<Renderer>().material.color = newColor;


        StartCoroutine(DestroyObject(objectClone));

    }

    IEnumerator DestroyObject(GameObject objectClone)
    {
        yield return new WaitForSeconds(20);

        Destroy(objectClone);

    }
}
