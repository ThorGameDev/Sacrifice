using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Gaurdian : MonoBehaviour
{
    public float DistanceAway;
    public Rigidbody2D rb2d;
    public GameObject Player;
    public float LeftRightSpeed;
    public float UpSpeed;
    public float DownSpeed;
    public bool CanFarAttack;
    public GameObject BulletPrefab;
    public float bulletCooldown;
    public float bulletCooodownDistant;
    private float TimeInCountdown = 1;
    public float ShootDistance = 20;
    public void Start()
    {
        Player = FindObjectOfType<PlayerController>().gameObject;
    }
    void Update()
    {
        if(Player == null)
        {
            return;
        }
        Vector2 Velocity = new Vector2();
        Velocity.x += (Player.transform.position.x > this.transform.position.x) ? LeftRightSpeed :-LeftRightSpeed ;
        if(Player.transform.position.y + DistanceAway >this.transform.position.y)
        {
            Velocity.y = UpSpeed;
        }
        else
        {
            Velocity.y = -DownSpeed;
        }
        rb2d.velocity = Velocity;
        if (Vector2.Distance(Player.transform.position, transform.position) >= ShootDistance && CanFarAttack)
        {
            TimeInCountdown -= Time.deltaTime;
            if (TimeInCountdown <= 0)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                Vector3 ShootFrom = new Vector3(Player.transform.position.x + UnityEngine.Random.Range(-10,11), Player.transform.position.y + 10, 0);
                bullet.transform.position = ShootFrom;
                bullet.GetComponent<Bullet>().Direction = (Player.transform.position - ShootFrom + new Vector3(UnityEngine.Random.Range(-3, 4), 0, 0)).normalized;
                TimeInCountdown = bulletCooodownDistant;
            }
        }
        else if(Vector2.Distance(Player.transform.position, transform.position) <= ShootDistance)
        {
            TimeInCountdown -= Time.deltaTime;
            if (TimeInCountdown <= 0)
            {
                if (UnityEngine.Random.Range(0, 8) == 0)
                {
                    GameObject bullet1 = Instantiate(BulletPrefab);
                    bullet1.transform.position = this.transform.position;
                    bullet1.GetComponent<Bullet>().Direction = (Player.transform.position - this.transform.position + new Vector3(0, 2, 0)).normalized;
                    GameObject bullet2 = Instantiate(BulletPrefab);
                    bullet2.transform.position = this.transform.position;
                    bullet2.GetComponent<Bullet>().Direction = (Player.transform.position - this.transform.position + new Vector3(0, -2, 0)).normalized;
                }
                else if (UnityEngine.Random.Range(0, 8) == 0)
                {
                    GameObject bullet1 = Instantiate(BulletPrefab);
                    bullet1.transform.position = this.transform.position;
                    bullet1.GetComponent<Bullet>().Direction = (Player.transform.position - this.transform.position + new Vector3(3, 0, 0)).normalized;
                    GameObject bullet2 = Instantiate(BulletPrefab);
                    bullet2.transform.position = this.transform.position;
                    bullet2.GetComponent<Bullet>().Direction = (Player.transform.position - this.transform.position + new Vector3(-3, 0, 0)).normalized;
                }
                else
                {
                    GameObject bullet = Instantiate(BulletPrefab);
                    bullet.transform.position = this.transform.position;
                    bullet.GetComponent<Bullet>().Direction = (Player.transform.position - this.transform.position).normalized;
                }
                TimeInCountdown = bulletCooldown;
            }
        }
    }
}
