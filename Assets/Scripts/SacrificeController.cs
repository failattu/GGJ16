using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SacrificeController : MonoBehaviour {
    public float force = 3;
    public GameObject sacrifes;
    public Text bobSacrifice;
    public string bobName;

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 dir = collision.contacts[0].point - transform.position;
            dir = -dir.normalized;
            GetComponent<Rigidbody>().AddForce(dir * force);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            SetCountText();

        }
    }
    void SetCountText()
    {
            bobSacrifice.text = "You have sacrificed "+ bobName + "!";

            sacrifes.SetActive(false);
            
        }
    }

