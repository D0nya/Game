using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour, ITail
{
  private Stack<GameObject> tail;

  public int Amount
  {
    get => tail.Count;
  }
  public Stack<GameObject> Items
  {
    get => tail;
    set => tail = value;
  }

  private void Start()
  {
    tail = new Stack<GameObject>();
  }
  public void AddObject(GameObject obj)
  {
    tail.Push(obj);
  }
  public void ResetTail()
  {
    foreach (var item in tail)
      Destroy(item);
    tail.Clear();
  }

  public void ReduceStolenCollectables(GameObject until, IPlayer stealer)
  {
    while (Items.Peek() != until)
    {
      var currentCollectable = Items.Pop().GetComponent<Collectable>();
      currentCollectable.ChangeOwner(stealer);
    }
    if (until == Items.Peek())
    {
      Items.Pop().GetComponent<Collectable>().ChangeOwner(stealer);
      return;
    }
  }
}
