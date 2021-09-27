using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour
{
    public Camera camera;
    Transform cam;
    // Start is called before the first frame update

    private void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam = camera.transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
