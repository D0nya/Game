using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMouse : IMovement
{
  private Vector2 targetPos;
  private Camera main;
  private GameObject player;

  public MovementMouse(GameObject player)
  {
    main = Camera.main;
    this.player = player;
    targetPos = player.transform.position;
  }

  public Vector2 Move()
  {
    if (Input.GetMouseButton(0))
    {
      targetPos = main.ScreenToWorldPoint(Input.mousePosition);
    }
    if (Input.GetMouseButtonUp(0))
    {
      targetPos = player.transform.position;
    }
    return targetPos;
  }
}
