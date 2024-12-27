using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Bullet : MonoBehaviour
{
    public Vector2 Direction;
    public Rigidbody2D rb2d;
    public float speed;
    private Vector3 InitialPos;
    public GameObject Burst;
    public AudioClip Shoot;
    public AudioClip BlastSFX;
    public int Range;
    private Tilemap t;
    private Tile[] GoodTiles;
    private Tile[] EvilTiles;
    public void Start()
    {
        if(FindObjectOfType<PlayerController>() == null)
        {
            return;
        }
        FindObjectOfType<AudioSource>().PlayOneShot(Shoot);
        PlayerController pc = FindObjectOfType<PlayerController>();
        t = pc.t;
        GoodTiles = pc.GoodTiles;
        EvilTiles = pc.EvilTiles;
        InitialPos = this.transform.position;
    }
    private float TimeAlive;
    void Update()
    {
        transform.right = transform.position- InitialPos;
        //this.transform.rotation = Quaternion.Euler(this.transform.position - PreviousPosition);
        rb2d.velocity = Direction * speed;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<PlayerFolow>() == null)
        {
            return;
        }
        Vector3Int PlayerPos = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        for (int Y = Range; Y >= -Range; Y--)
        {
            for (int X = Range; X >= -Range; X--)
            {
                if (Mathf.Pow(X, 2) + Mathf.Pow(Y, 2) < Mathf.Pow(Range, 2))
                {
                    Vector3Int TileCheck = new Vector3Int(X, Y, 0) + PlayerPos;
                    int i = 0;
                    foreach (Tile tile in GoodTiles)
                    {
                        if (t.GetTile(TileCheck) == EvilTiles[i])
                        {
                            t.SetTile(TileCheck, GoodTiles[i]);
                        }
                        i++;
                    }
                }
            }
        }
        if (collision.gameObject.tag == "Player")
        {
            PlayerController pc = FindObjectOfType<PlayerController>();
            pc.Die();
        }
        FindObjectOfType<PlayerFolow>().a.SetTrigger("Shake");
        Destroy(this.gameObject);
        FindObjectOfType<AudioSource>().PlayOneShot(BlastSFX);
        Instantiate(Burst).transform.position = this.transform.position;
    }
}
