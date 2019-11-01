using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{

  [SerializeField]
  private ITail tail;
  [SerializeField]
  private IMovement movement;
  [SerializeField]
  private float speed;
  [SerializeField]
  private ITeam team;
  [SerializeField]
  private Mouse mouse = Mouse.left;

  [SerializeField]
  private int level = 0;
  [SerializeField]
  private int experience = 0;
  private Dictionary<int, int> levelExperience;
  private enum Mouse
  {
    left,
    right
  }

  private bool facingRight = true;
  private bool atMinSpeed = true;
  private float minSpeed;
  private Vector2 targetPos;
  [SerializeField]
  private List<Vector3> storedPositions;
  
  public event Action LevelUp;

  public ITail Tail
  {
    get => tail;
    set => tail = value;
  }
  public IMovement Movement
  {
    get => movement;
    set => movement = value;
  }
  public ITeam Team
  {
    get => team;
    set => team = value;
  }
  public float Speed
  {
    get => speed;
    set => speed = value;
  }
  public List<Vector3> StoredPositions
  {
    get => storedPositions;
    set => storedPositions = value;
  }
  public int Level
  {
    get => level;
    set => level = value;
  }

  private void Start()
  {
    levelExperience = new Dictionary<int, int>();
    levelExperience.Add(0, 100);
    levelExperience.Add(1, 200);
    levelExperience.Add(2, 300);
    levelExperience.Add(3, 400);
    levelExperience.Add(4, 500);
    levelExperience.Add(5, 600);
    LevelUp += IncreaseLevel;
    if (mouse == Mouse.left)
      movement = new MovementMouse(gameObject);
    else
      movement = new MovementKeys(gameObject);
    storedPositions = new List<Vector3>();

    Tail = GetComponent<Tail>();

    minSpeed = speed;
  }
  private void Update()
  {
    if(Tail.Amount > 0)
      StorePositions();
    if (!atMinSpeed)
    {
      Speed -= Speed * Time.deltaTime / 6f;
      if (Speed <= minSpeed)
      {
        Speed = minSpeed;
        atMinSpeed = !atMinSpeed;
      }
    }
  }
  private void FixedUpdate()
  {
    targetPos = movement.Move();
    transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);

    if (transform.position.x < targetPos.x && facingRight)
      Flip();
    if (transform.position.x > targetPos.x && !facingRight)
      Flip();
  }
  private void Flip()
  {
    facingRight = !facingRight;
    transform.Rotate(Vector3.up * 180);
  }
  private void StorePositions()
  {
    if (storedPositions.Count == 0)
    {
      storedPositions.Add(transform.position);
      return;
    }
    else if (storedPositions[storedPositions.Count - 1] != transform.position)
      storedPositions.Add(transform.position);

    // если количество позиций больше, чем максимальная дистанция, то удалить ласт позицию
    var maxFollowDistance = Tail.Items.Peek().GetComponent<Collectable>().CurrentFollowDistance;

    if(storedPositions.Count > maxFollowDistance)
      storedPositions.RemoveAt(0);
  }

  public void SpeedBoost(float amount)
  {
    float maxSpeed = minSpeed * 2;
    if (amount >= maxSpeed)
      amount = maxSpeed;
    speed += amount;
    atMinSpeed = false;
  }
  public void IncreaseExperience(int amount)
  {
    experience += amount;
    if (levelExperience[level] <= experience)
    {
      LevelUp.Invoke();
    }
  }
  public void ResetPositions()
  {
    storedPositions.Clear();
  }
  private void IncreaseLevel()
  {
    level++;
  }
}
