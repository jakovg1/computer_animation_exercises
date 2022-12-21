using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSceneOnCollision : MonoBehaviour
{
    [SerializeField]
    GameObject collisionObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(transform.gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //Debug.Log("Collision with: object ");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
