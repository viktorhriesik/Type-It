using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bltSc : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public GameObject blt;
    public GameObject hitParticles;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        
   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
         collision.gameObject.GetComponent<asteroidSc>().takeDamage();
         var particles = Instantiate(hitParticles, transform.position, Quaternion.identity);

         Destroy(particles.gameObject,.5f);
         Destroy(this.gameObject);

    }


}
