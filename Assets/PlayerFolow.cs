using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerFolow : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    public float UpDownSpeed;
    public bool ChasingPlayer;
    public Animator a;
    private bool Dieing;
    public string[] FirstDevilWords;
    public string[] DevilWords;
    public string[] Congrats;
    public TMP_Text DevilTalks;
    public AudioSource audioS;
    public AudioClip GameOver;
    public void FixedUpdate()
    {
        if(Player == null)
        {
            if (Dieing == false)
            {
                StaticVariables.Deaths++;
                Dieing = true;
                a.SetTrigger("Die");
                StartCoroutine(Die());
            }
            return;
        }
        if (Vector2.Distance(Player.transform.position, this.transform.position) > 3)
        {
            ChasingPlayer = true;
        }
        else if (Vector2.Distance(Player.transform.position, this.transform.position) < 0.7f)
        {
            ChasingPlayer = false;
        }
        if (ChasingPlayer == true)
        {
            this.transform.position = new Vector3(Mathf.Lerp(transform.position.x, Player.transform.position.x, Time.fixedDeltaTime * speed), Mathf.Lerp(transform.position.y, Player.transform.position.y, Time.fixedDeltaTime * UpDownSpeed), -10);
        }
    }
    private bool InAction;
    public IEnumerator Die()
    {
        if (InAction == true)
        {
            yield break;
        }
        else
        {
            InAction = true;
        }
        for (float i =2;i >= 0; i -= Time.deltaTime)
        {
            audioS.volume = i / 2;
            yield return new WaitForEndOfFrame();
        }
        audioS.volume = 1;
        audioS.clip = GameOver;
        audioS.Play();
        //yield return new WaitForSeconds(2f);
        if (StaticVariables.Deaths == 1)
        {
            foreach (string s in FirstDevilWords)
            {
                string CurrentText = s.Replace("<Deaths>", StaticVariables.Deaths.ToString());
                DevilTalks.text = CurrentText;
                while (Input.anyKey)
                {
                    yield return new WaitForEndOfFrame();
                }
                while (!Input.anyKey)
                {
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            foreach (string s in DevilWords)
            {
                string CurrentText = s.Replace("<Deaths>", StaticVariables.Deaths.ToString());
                DevilTalks.text = CurrentText;
                while (Input.anyKey)
                {
                    yield return new WaitForEndOfFrame();
                }
                while (!Input.anyKey)
                {
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
        {
            audioS.volume = i * 2;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator Win()
    {
        a.SetTrigger("Die");
        Destroy(Player);
        if (InAction == true)
        {
            yield break;
        }
        else
        {
            InAction = true;
        }
        for (float i = 2; i >= 0; i -= Time.deltaTime)
        {
            audioS.volume = i / 2;
            yield return new WaitForEndOfFrame();
        }
        audioS.volume = 1;
        audioS.clip = GameOver;
        audioS.Play();
        foreach (string s in Congrats)
        {
            string CurrentText = s.Replace("<Deaths>", StaticVariables.Deaths.ToString());
            DevilTalks.text = CurrentText;
            while (Input.anyKey)
            {
                yield return new WaitForEndOfFrame();
            }
            while (!Input.anyKey)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
        {
            audioS.volume = i * 2;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void FW()
    {
        StartCoroutine(FinalWin());
    }
    public IEnumerator FinalWin()
    {
        if (InAction == true)
        {
            yield break;
        }
        else
        {
            InAction = true;
        }
        a.SetTrigger("Die");
        Destroy(Player);
        for (float i = 2; i >= 0; i -= Time.deltaTime)
        {
            audioS.volume = i / 2;
            yield return new WaitForEndOfFrame();
        }
        audioS.volume = 1;
        audioS.clip = GameOver;
        audioS.Play();
        foreach (string s in Congrats)
        {
            string CurrentText = s.Replace("<Deaths>", StaticVariables.Deaths.ToString());
            DevilTalks.text = CurrentText;
            while (Input.anyKey)
            {
                yield return new WaitForEndOfFrame();
            }
            while (!Input.anyKey)
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.1f);
        }
        for (float i = 0.5f; i >= 0; i -= Time.deltaTime)
        {
            audioS.volume = i * 2;
            yield return new WaitForEndOfFrame();
        }
    }
}
