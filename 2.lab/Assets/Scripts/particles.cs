using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particles : MonoBehaviour
{
    public GameObject particleObject;

    [SerializeField]
    public GameObject camera;

    [SerializeField]
    public float particleGenerationRate = 2f;

    [SerializeField]
    public float particleLifetime = 10f;

    [SerializeField]
    private float locationSpread = 2;
    private List<GameObject> particleList = new List<GameObject>();

    private float t = 0f;

    private void OnDrawGizmos()
    {
        for (var theta = 0f; theta <= 2*3.14f; theta += 0.05f)
        {
            var position = transform.position + new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta)) * locationSpread;
            Gizmos.DrawSphere(position, 0.1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateNewSnowflake", 0f, 1 / particleGenerationRate);
    }

    // Update is called once per frame
    void Update()
    {
        //t += Time.deltaTime * particleGenerationRate;
        foreach (GameObject particle in particleList)
        {
            var finalRotation = Quaternion.Euler(90, 0, 0);
            particle.transform.rotation = Quaternion.LookRotation(camera.transform.position - particle.transform.position) * finalRotation;

            var previousColor = particle.GetComponent<Renderer>().material.color;
            var newColorVariable = previousColor.r <= 0f ? previousColor.r : previousColor.r - 0.001f;
            var newColor = new Color(newColorVariable, newColorVariable, previousColor.b);
            particle.GetComponent<Renderer>().material.color = newColor;
        }

    }

    void CreateNewSnowflake()
    {

        var randPos = new Vector3(Random.Range(-locationSpread, locationSpread), 0, Random.Range(-locationSpread, locationSpread));
        var snowflakeClone = Instantiate(particleObject, transform.position + randPos, Quaternion.identity); //
        snowflakeClone.GetComponent<Renderer>().material.color = Color.white;
        particleList.Add(snowflakeClone);

        StartCoroutine(DestroySnowFlake(snowflakeClone));


    }

    IEnumerator DestroySnowFlake(GameObject snowflake)
    {
        yield return new WaitForSeconds(particleLifetime);

        particleList.Remove(snowflake);
        Destroy(snowflake);
        
    }
}
