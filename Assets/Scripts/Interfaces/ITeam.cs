using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
  public interface ITeam
  {
    List<IPlayer> Players { get; set; }
    string TeamName { get; set; }
    int Score { get; set; }
  }
}
