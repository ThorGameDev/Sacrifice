using UnityEngine;
using UnityEngine.Tilemaps;
public class BirdBullet : MonoBehaviour
{
    public Vector2 Direction;
    public Rigidbody2D rb2d;
    public float speed;
    private Vector3 InitialPos;
    public GameObject Burst;
    public AudioClip BurstSFX;
    public int Range;
    public Tilemap t;
    public Tile[] GoodTiles;
    public Tile[] EvilTiles;
    public void Start()
    {
        if (FindObjectOfType<PlayerController>() == null)
        {
            return;
        }
        PlayerController pc = FindObjectOfType<PlayerController>();
        t = pc.t;
        GoodTiles = pc.GoodTiles;
        EvilTiles = pc.EvilTiles;
        InitialPos = this.transform.position;
    }
    private float TimeAlive;
    void Update()
    {
        transform.right = transform.position - InitialPos;
        //this.transform.rotation = Quaternion.Euler(this.transform.position - PreviousPosition);
        rb2d.velocity = Direction * speed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(enabled == false)
        {
            return;
        }
        if (FindObjectOfType<PlayerFolow>() == null)
        {
            return;
        }
        Vector3Int PlayerPos = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
        if (GoodTiles.Length > 0 && EvilTiles.Length > 0)
        {
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
                            if (t.GetTile(TileCheck) == GoodTiles[i])
                            {
                                t.SetTile(TileCheck, EvilTiles[i]);
                            }
                            i++;
                        }
                    }
                }
            }
        }
        if (collision.gameObject.tag == "Foe")
        {
            if(collision.gameObject.GetComponent<Boss>() == null)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Boss>().TakeDamage();
            }
        }
        FindObjectOfType<PlayerFolow>().a.SetTrigger("Shake");
        Destroy(this.gameObject);
        FindObjectOfType<AudioSource>().PlayOneShot(BurstSFX);
        Instantiate(Burst).transform.position = this.transform.position;
    }
}
