using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IBase
{
  private ITeam team;
  public ITeam Team
  {
    get => team;
    set => team = value;
  }

  private void Start()
  {
    Team = GetComponent<Team>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      IPlayer player = collision.GetComponent<Player>();
      ITail tail = collision.gameObject.GetComponent<Tail>();
      if (player.Team.TeamName == team.TeamName && tail.Amount > 0)
      {
        Team.Score += tail.Amount;
        player.SpeedBoost(tail.Amount);
        player.IncreaseExperience(tail.Amount * 2);
        player.ResetPositions();
        tail.ResetTail();
      }
    }
  }
}