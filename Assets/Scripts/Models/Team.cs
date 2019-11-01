using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour, ITeam
{
  [SerializeField]
  private List<IPlayer> players;
  [SerializeField]
  private int score;
  [SerializeField]
  private string teamName;
  public List<IPlayer> Players
  {
    get => players;
    set => players = value;
  }
  public int Score
  {
    get => score;
    set => score = value;
  }
  public string TeamName
  {
    get => teamName;
    set => teamName = value;
  }

  private void Start()
  {
    Players = new List<IPlayer>();
  }
}
