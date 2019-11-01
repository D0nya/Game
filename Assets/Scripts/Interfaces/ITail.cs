using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
  public interface ITail
  {
    int Amount { get; }
    Stack<GameObject> Items { get; set; }
    void AddObject(GameObject collectable);
    void ResetTail();
    void ReduceStolenCollectables(GameObject until, IPlayer stealer);
  }
}
