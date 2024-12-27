using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int Health;
    public Animator a;
    public GameObject Pop;
    public GameObject Spawner;
    public AudioClip BurstSFX;
    public PlayerFolow pf;
    public void TakeDamage()
    {
        Health--;
        if (Health <= 0)
        {
            a.SetTrigger("Die");
            StartCoroutine(Die());
        }
        else
        {
            a.SetTrigger("Hurt");
        }
    }
    public IEnumerator Die()
    {
        Destroy(Spawner);
        Destroy(this.GetComponent<Gaurdian>());
        yield return new WaitForSeconds(5f);
        Instantiate(Pop).transform.position = transform.position;
        Destroy(this.gameObject);
        FindObjectOfType<AudioSource>().PlayOneShot(BurstSFX);
        pf.FW();
    }
}
