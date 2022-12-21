using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetSpawner : MonoBehaviour
{
    [SerializeField]
    List<GameObject> inputObjects;

    [SerializeField]
    public float forwardMovementCoefficient = 5f;

    [SerializeField]
    public float objectGenerationRate = 4f;

    private List<GameObject> objects = new List<GameObject>();

    private float timeElapsedCoefficient = 0f;

    private float defaultXPosition = 0f;

    void Start()
    {
        //for (float i = transform.position.x - 50f; i >= 70; i -= 50f)
        //{
        //    Debug.Log(i);
        //    defaultXPosition = i;
        //    CreateNewObject();
        //}


        //defaultXPosition = 0f;
        InvokeRepeating("CreateNewObject", 0f, 1 / objectGenerationRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsedCoefficient <= 70)
        {
            timeElapsedCoefficient += Time.deltaTime;
        }
    }

    void CreateNewObject()
    {
        //Debug.Log(timeElapsedCoefficient);
        forwardMovementCoefficient = 40f + timeElapsedCoefficient * 1.05f;
        var positionFactor = Random.Range(200f, 1400f);
        var rotationFactor = 20f;

        var scaleFactor = Random.Range(0f, 10f);

        //var randPosition = new Vector3(-defaultXPosition, Random.Range(-positionFactor, positionFactor), 100 + Random.Range(-positionFactor, positionFactor));
        var randPosition = positionFactor * Random.onUnitSphere;

        var randRotation = new Vector3(Random.Range(-rotationFactor, rotationFactor) * 1f, 0f, 0f);
        var randScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);


        var randInputObject = inputObjects[(int)Random.Range(0, inputObjects.Count)];
        //var randInputObject = inputObjects[1];

        var objectClone = Instantiate(randInputObject, transform.position + randPosition, Quaternion.identity); //
        objectClone.transform.localScale = objectClone.transform.localScale + randScale;
        //objectClone.transform.localScale =  randScale;

        objectClone.GetComponent<Rigidbody>().AddRelativeTorque(randRotation, ForceMode.Impulse);

        objectClone.GetComponent<Rigidbody>().velocity = new Vector3(-forwardMovementCoefficient, 0, 0);

        StartCoroutine(DestroyObject(objectClone));

    }

    IEnumerator DestroyObject(GameObject objectClone)
    {
        yield return new WaitForSeconds(40);

        //objects.Remove(objectClone);
        Destroy(objectClone);

    }
}
