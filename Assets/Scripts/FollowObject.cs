using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public bool followX = true;
    public bool followY = true;
    public bool followZ = false;
    public GameObject targetGameObj;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = targetGameObj.transform.position;
        Vector3 thisPos = this.gameObject.transform.position;
        float x = followX ? targetPos.x : thisPos.x;
        float y = followY ? targetPos.y : thisPos.y;
        float z = followZ ? targetPos.z : thisPos.z;
        this.gameObject.transform.position = new Vector3(x, y, z);
    }
}
