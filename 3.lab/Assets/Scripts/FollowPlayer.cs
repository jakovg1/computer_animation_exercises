using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField]
    GameObject targetObject;

    // Update is called once per frame
    void Update()
    {
        transform.position = targetObject.transform.position;
    }
}
