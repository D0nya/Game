using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
  public GameObject collectablePrefab;
  public float timeToRespawn;
  public bool collected = false;
  private float countDown;

  private void Start()
  {
    countDown = timeToRespawn;
    Instantiate(collectablePrefab, transform.position, Quaternion.identity, transform);
  }

  void Update()
  {
    if (collected == true)
      countDown -= Time.deltaTime;
    if(countDown   <= 0)
    {
      Instantiate(collectablePrefab, transform.position, Quaternion.identity, transform);
      collected = false;
      countDown = timeToRespawn;
    }
  }
}