using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Texture blackTexture;
    public float speed;
    public Text countText;
    public Text winText;
    private Rigidbody rb;
    private int count;
    public GameObject player;
    private AudioSource audio;
    private bool death = false;
    private bool fadingIn;
    private bool fadingOut;
    private float fadeLength;
    float alphaFadeValue = 1;

    void Start()
    {
        fadingIn = false;
        fadingOut = false;
        fadeLength = 20;
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        
    }

    void FixedUpdate()
    {
        if (audio.isPlaying == false && death == true) {
            player.SetActive(false);
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
        
    }
    private void OnGUI()
    {
        if (death == true)
        {
            if (fadingIn)
            {
                alphaFadeValue -= Time.deltaTime / fadeLength;
                if (alphaFadeValue < 0)
                {
                    fadingIn = false;
                    alphaFadeValue = 0;
                }
            }
            if (fadingOut)
            {
                alphaFadeValue += Time.deltaTime / fadeLength;
                if (alphaFadeValue > 1)
                {
                    fadingOut = false;
                    alphaFadeValue = 1;
                }
            }
            GUI.color = new Color(0, 0, 0, alphaFadeValue);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blackTexture);
        
    }
    }

        void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count > 0)
        {
            winText.text = "You have sacrificed yourself!";
            death = true;
            audio.Play();
            fadingOut = true;

        }
    }
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.CompareTag ("wall"))
		{
			Vector3 dir = col.contacts [0].point - transform.position;
			dir = -dir.normalized;
			GetComponent <Rigidbody> ().AddForce (dir * 300);
		}
	}
}
