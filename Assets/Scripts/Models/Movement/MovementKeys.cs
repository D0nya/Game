using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementKeys : IMovement
{
  private Vector2 targetPos;
  private Camera main;
  private GameObject player;

  public MovementKeys(GameObject player)
  {
    main = Camera.main;
    this.player = player;
    targetPos = player.transform.position;
  }

  public Vector2 Move()
  {
    if (Input.GetMouseButton(1))
    {
      targetPos = main.ScreenToWorldPoint(Input.mousePosition);
    }
    if (Input.GetMouseButtonUp(1))
    {
      targetPos = player.transform.position;
    }
    return targetPos;
  }
}
