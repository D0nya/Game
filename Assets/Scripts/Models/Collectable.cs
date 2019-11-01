using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
  private int defaultFollowDistance = 7;
  private int currentFollowDistance;
  private bool isCollected = false;
  private IPlayer owner;
  public bool IsCollected
  {
    get => isCollected;
    set => isCollected = value;
  }
  public IPlayer Owner
  {
    get => owner;
    set => owner = value;
  }
  public int CurrentFollowDistance { get => currentFollowDistance; set => currentFollowDistance = value; }

  private void Update()
  {
    if (isCollected)
      Follow();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.CompareTag("Player"))
    {
      transform.position = collision.transform.position;
      if (!isCollected)
      {
        var spawner = transform.parent.GetComponent<SpawnObjects>();
        spawner.collected = true;
        owner = collision.gameObject.GetComponent<Player>();
        owner.Tail.AddObject(gameObject);
        CurrentFollowDistance = defaultFollowDistance * owner.Tail.Amount;
        IsCollected = true;
      }
      else
      {
        IPlayer stealer = collision.gameObject.GetComponent<Player>();
        if ((object)owner != stealer)
        {
          owner.ResetPositions();
          //Remove from old owner
          owner.Tail.ReduceStolenCollectables(gameObject, stealer);
        }
      }
    }
  }

  public void ChangeOwner(IPlayer newOwner)
  {
    owner = newOwner;
    owner.Tail.AddObject(gameObject);
    CurrentFollowDistance = defaultFollowDistance * owner.Tail.Amount;
  }

  public void Follow()
  {
    if(owner.StoredPositions.Count >= currentFollowDistance)
      transform.position = owner.StoredPositions[owner.StoredPositions.Count - currentFollowDistance];
  }
}
