using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shipSc : MonoBehaviour
{
    public GameObject gun;
    public GameObject bullet;
    public GameObject targetWord;
    public string firstWord = "/";
    public GameObject[] asteroids = new GameObject[100];
    public List<string> words = new List<string>();

    public Transform camTransform;
    public static float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    Vector3 originalPos;
    public GameObject aimTarget;

    public float shakeFrequency;
    void Start()
    {
        originalPos = Camera.main.transform.position;
        camTransform = Camera.main.transform;
        findAllWords();
        //targetWord = asteroids[0];
    }
    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
        if (targetWord != null)
        {
            if (targetWord.GetComponent<asteroidSc>().word.Length == 0)
            {
                targetWord = null;
            }
        }


    }

    public void findAllWords()
    {
        asteroids = new GameObject[100];
        words.Clear();
        asteroids = GameObject.FindGameObjectsWithTag("asteroid");
        foreach (var item in asteroids)
        {
            words.Add(item.GetComponent<asteroidSc>().word);
        }
    }
    public void shoot()
    {
        GameObject currentBlt= Instantiate(bullet, gun.transform.position, Quaternion.identity);

        var bltSc = currentBlt.GetComponent<bltSc>();
        float angle = Mathf.Atan2(targetWord.transform.position.y - transform.position.y, targetWord.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
        currentBlt.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100);
        bltSc.target = targetWord;

        targetWord.GetComponent<asteroidSc>().ShiftString();
    }
    public void clickCharacter(string character)
    {
        if (targetWord == null)
        {
            findAllWords();
            for (int i = 0; i < words.Count; i++)
            {
                string s = words[i];
                if (s.Length != 0)
                {
                    if (s[0].ToString().Equals(character))
                    {
                        //Debug.Log("Prvo slovo: " + s[0].ToString() + "  Character: " + character);
                        GameObject aim = Instantiate(aimTarget, asteroids[i].transform.position, Quaternion.identity);
                        aim.transform.SetParent(asteroids[i].transform);
                        Destroy(aim.gameObject, .6f);
                        targetWord = asteroids[i];
                    }
                }
            }

        }
        if(targetWord!=null)
        {
           
            string s = targetWord.GetComponent<asteroidSc>().word;
            if (s[0].ToString().Equals(character))
            {
                Debug.Log("Prvo slovo: " + s[0].ToString() + "  Character: " + character);
                shoot();
            }
        }


    }
    public static void CameraShake()
    {
        shakeDuration = 0.2f;
    }
}
