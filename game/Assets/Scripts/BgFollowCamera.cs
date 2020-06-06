using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgFollowCamera : MonoBehaviour
{
    [Space]
    [Header("Values")]
    private Vector2 target = new Vector2(0, 0);
    private Dictionary<string, Vector2> modificationValues;

    // Start is called before the first frame update
    void Start()
    {
        modificationValues = new Dictionary<string, Vector2>();
        modificationValues.Add("L", new Vector3(-15f, 0f, 0f));
        modificationValues.Add("R", new Vector3(15f, 0f, 0f));
        modificationValues.Add("T", new Vector3(0f, 15f, 0f));
        modificationValues.Add("B", new Vector3(0f, -15f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraController cameraController = mainCamera.GetComponent<CameraController>();


        float step = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().movementSpeed * Time.deltaTime;
        target = new Vector2(cameraController.GetTarget().x, cameraController.GetTarget().y);

        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
