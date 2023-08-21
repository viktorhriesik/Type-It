using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class asteroidSc : MonoBehaviour
{
    public float moveSpeed;
    public GameObject ship;
    public string word;
    public Text text;
    public Color color;
    public int hp;
    public GameObject behindTextObj;
    
    void Start()
    {
        text.text =word;
        hp = word.Length;
        ship = GameObject.FindWithTag("ship");

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, ship.transform.position, moveSpeed * Time.deltaTime);
        if (word.Length == 0)
        {
            behindTextObj.gameObject.SetActive(false);
        }
    }

    public void takeDamage()
    {
        text.color = color;
        hp--;
        text.text = word;
        if (hp == 0)
        {
            shipSc.CameraShake();
            Destroy(this.gameObject);
        }

    }
    public void ShiftString()
    {
        word = word.Substring(1, word.Length - 1);
        text.text = word;
    }
}
