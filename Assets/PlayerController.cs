using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public Tilemap t;
    public Tile[] GoodTiles;
    public Tile[] EvilTiles;
    public int Range;
    private Vector3Int PriorPos;
    public GameObject DeathEffect;
    public GameObject Particles;
    public GameObject Held;
    // Update is called once per frame
    void Update()
    {
        if(Held != null)
        {
            Held.transform.position = this.transform.position + new Vector3(0, 1, 0);
        }
        rb2d.velocity = new Vector2(Input.GetAxis("Horizontal") * speed,rb2d.velocity.y);
        if (StaticVariables.Deaths >= 1)
        {
            Particles.SetActive(true);
            Vector3Int PlayerPos = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
            if (PlayerPos != PriorPos)
            {
                PriorPos = PlayerPos;
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
        }
        if(transform.position.y <= -15)
        {
            Die();
        }
    }
    public void Die()
    {
        Vector2 Position = this.transform.position;
        Instantiate(DeathEffect).transform.position = Position;
        Destroy(this.gameObject);
    }
}
