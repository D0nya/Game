using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
  public interface IPlayer
  {
    ITail Tail { get; set; }
    IMovement Movement { get; set; }
    ITeam Team { get; set; }
    List<Vector3> StoredPositions { get; set; }
    int Level { get; set; }

    void SpeedBoost(float amount);
    void ResetPositions();
    void IncreaseExperience(int amount);
  }
}
