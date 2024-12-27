using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouceCircle : MonoBehaviour
{
    public List<GameObject> ThingsInRange;
    public PlayerController pc;
    public Camera MainCamera;
    public void Update()
    {
        if(StaticVariables.Deaths < 1 || pc == null)
        {
            return;
        }
        Vector3 MousePos = Input.mousePosition;
        MousePos.z = 10;
        Vector3 Spot = new Vector3(MainCamera.ScreenToWorldPoint(MousePos).x, MainCamera.ScreenToWorldPoint(MousePos).y, 0);
        this.transform.position = Spot;
        if (pc.Held == null)
        {
            if (ThingsInRange.Count >= 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject CurrentClosestThing = ThingsInRange[0];
                    float ThingDistance = Vector2.Distance(ThingsInRange[0].transform.position, this.transform.position);
                    foreach (GameObject g in ThingsInRange)
                    {
                        float Dist = Vector2.Distance(g.transform.position, this.transform.position);
                        if (Dist < ThingDistance)
                        {
                            CurrentClosestThing = g;
                            ThingDistance = Dist;
                        }
                    }
                    pc.Held = CurrentClosestThing;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                BirdBullet b = pc.Held.GetComponent<BirdBullet>();
                pc.Held = null;
                b.enabled = true;
                b.Direction = (Spot - b.transform.position).normalized;
            }
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Peacefull")
        {
            if (ThingsInRange.Contains(collision.gameObject) == false)
            {
                ThingsInRange.Add(collision.gameObject);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Peacefull")
        {
            if (ThingsInRange.Contains(collision.gameObject) == true)
            {
                ThingsInRange.Remove(collision.gameObject);
            }
        }
    }
}
