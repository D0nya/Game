using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour, IGameManager
{
  private IPlayer[] players;
  private IBase[] bases;
  private int numberOfPlayers;
  private void Start()
  {
    GameObject[] basesGO = GameObject.FindGameObjectsWithTag("Base");
    bases = basesGO.Select(b => b.GetComponent<Base>()).ToArray();
    
    GameObject[] playersGO = GameObject.FindGameObjectsWithTag("Player");
    players = playersGO.Select(p => p.GetComponent<Player>()).ToArray();

    numberOfPlayers = players.Length;
    int sizeOfTeam = Mathf.CeilToInt((float)numberOfPlayers / 2);

    // Fill first team
    FillTeam(players.Take(sizeOfTeam), 0);

    // Fill second team
    FillTeam(players.Skip(sizeOfTeam).Take(sizeOfTeam), 1);
  }

  private void FillTeam(IEnumerable<IPlayer> players, int teamNumber)
  {
    foreach (IPlayer player in players)
    {
      player.Team = bases[teamNumber].Team;
      player.Team.Players.Add(player);
    }
  }
}
