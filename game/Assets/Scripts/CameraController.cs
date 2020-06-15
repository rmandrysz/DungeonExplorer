using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [Space]
    [Header("Values")]
    public float movementSpeed = 150f;
    private Vector3 target = new Vector3(0, 0, -15f);
    private Dictionary<string, Vector3> modificationValues;
    private float roomDistance = 20f;

    private void Start()
    {
        modificationValues = new Dictionary<string, Vector3>();
        modificationValues.Add("L", new Vector3(-roomDistance, 0f, 0f));
        modificationValues.Add("R", new Vector3(roomDistance, 0f, 0f));
        modificationValues.Add("T", new Vector3(0f, roomDistance, 0f));
        modificationValues.Add("B", new Vector3(0f, -roomDistance, 0f));
    }

    private void Update()
    {
        float step = movementSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    public void UpdateTarget(string direction)
    {
        target += modificationValues[direction];
    }

    public Vector3 GetTarget()
    {
        return target;
    }

}
