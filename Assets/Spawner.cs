using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Spawner : MonoBehaviour
{
    public GameObject[] Birds;
    private float TimeTillSpawn;
    public float SpawnInterval;
    public GameObject[] HolyCreatures;
    private float EvilTimeTillSpawn;
    public float Interval;
    private GameObject Player;
    public void Start()
    {
        Player = FindObjectOfType<PlayerController>().gameObject;
        EvilTimeTillSpawn = Interval;
    }
    public void Update()
    {
        if (Player == null)
        {
            return;
        }
        TimeTillSpawn -= Time.deltaTime;
        Tilemap t = Player.GetComponent<PlayerController>().t;
        if(TimeTillSpawn <= 0)
        {
            TimeTillSpawn += SpawnInterval;
            Vector3 Pos = new Vector3(Player.transform.position.x + UnityEngine.Random.Range(-10, 10), Player.transform.position.y + 10, 0);
            Vector3Int tPos = new Vector3Int(Mathf.RoundToInt(Pos.x), Mathf.RoundToInt(Pos.y), 0);
            while (t.GetTile(tPos) != null || t.GetTile(tPos + new Vector3Int(0, 1, 0) ) != null
                || t.GetTile(tPos + new Vector3Int(0, -1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(1, 0, 0)) != null
                || t.GetTile(tPos + new Vector3Int(-1, 0, 0)) != null
                || t.GetTile(tPos + new Vector3Int(1, 1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(-1, 1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(1, -1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(-1, -1, 0)) != null)
            {
                Pos.y += 1;
                tPos.y += 1;
            }
            GameObject GoodThing = Instantiate(Birds[UnityEngine.Random.Range(0,Birds.Length)]);
            GoodThing.transform.position = Pos;
        }
        if (StaticVariables.Deaths < 1)
        {
            return;
        }
        EvilTimeTillSpawn -= Time.deltaTime;
        if (EvilTimeTillSpawn <= 0)
        {
            EvilTimeTillSpawn += Interval;
            Vector3 Pos = new Vector3(Player.transform.position.x + UnityEngine.Random.Range(-10, 10), Player.transform.position.y + 10, 0);
            Vector3Int tPos = new Vector3Int(Mathf.RoundToInt(Pos.x), Mathf.RoundToInt(Pos.y), 0);
            while (t.GetTile(tPos) != null || t.GetTile(tPos + new Vector3Int(0, 1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(0, -1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(1, 0, 0)) != null
                || t.GetTile(tPos + new Vector3Int(-1, 0, 0)) != null
                || t.GetTile(tPos + new Vector3Int(1, 1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(-1, 1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(1, -1, 0)) != null
                || t.GetTile(tPos + new Vector3Int(-1, -1, 0)) != null)
            {
                Pos.y += 1;
                tPos.y += 1;
            }
            GameObject BadThing = Instantiate(HolyCreatures[UnityEngine.Random.Range(0, HolyCreatures.Length)]);
            BadThing.transform.position = Pos;
        }
    }
}