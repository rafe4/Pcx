using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardY : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;

        Vector3 targetPosition = new Vector3(camPos.x, transform.position.y, camPos.z);

        transform.LookAt(targetPosition);


    }
}
