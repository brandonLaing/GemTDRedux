using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClicker : MonoBehaviour
{
  public GameObject towerOne;
  public TileGenerator tileGen;
  public LayerMask tileLayermask;
  public LayerMask towerMask;

  public RotateAroundObject rotatorCamera;

  private void Update()
  {
    if (Input.GetMouseButtonDown(1) && !Input.GetKey(KeyCode.LeftShift))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out hit, 200F, tileLayermask))
      {
        TileInfo parentsInfo = hit.transform.GetComponentInParent<TileInfo>();

        parentsInfo.InteractWithTile();
      }
    }

    if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out hit, 100F, tileLayermask))
      {
        TileInfo parentsInfo = hit.transform.GetComponentInParent<TileInfo>();

        parentsInfo.DestroyTower();
      }
    }

    if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      if (Physics.Raycast(ray, out hit, 100F, towerMask))
      {
        rotatorCamera.RotateAround(hit.transform);
      }
    }
  }
}
