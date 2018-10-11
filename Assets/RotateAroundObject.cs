using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RotateAroundObject : MonoBehaviour {

  public Transform targetTransform;
  public float rotationSpeed;
  public Vector3 positionOffset = new Vector3(0, 1, -5);
  public Vector3 rotationOffSet = new Vector3(31, 0, 0);
  private Camera myCamera;

  private void Start()
  {
    myCamera = GetComponent<Camera>();
  }
  void Update ()
  {
    if (targetTransform != null)
    {
      myCamera.enabled = true;
      transform.RotateAround(targetTransform.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
    else
    {
      myCamera.enabled = false;
      targetTransform = null;
    }
  }

  public void RotateAround(Transform newTarget)
  {
    if (newTarget != targetTransform)
    {
      targetTransform = newTarget;
      transform.position = newTarget.position + positionOffset;
      transform.eulerAngles = rotationOffSet;
    }
  }
}
