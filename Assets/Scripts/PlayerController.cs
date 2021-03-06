using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public int playerSpeed;
    public int amountToDecrease = 0;

    public GameObject TempText;
    public int PlayerMaxHp;
     int playerHp;
    public HpBar healthBar;

    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
                if (instance ==null)
                {
                    instance = new GameObject("PlayerController spawned", typeof(PlayerController)).GetComponent<PlayerController>();

                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    
    private Gyroscope gyro;
    private bool gyroEnabled;
    private Vector3 gyroRot;
    private bool gyroActive;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(PlayerMaxHp);
        playerHp = PlayerMaxHp;
        EnableGyro();
    }
    public bool EnableGyro()
    {
   

        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroActive = true;
            return true;
            
        }
        return false;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        TempText.GetComponent<TextMesh>().text = gyroRot.ToString();
        // MovePlayer();
        GyroMove();
        PlayerMove();
    }
    void PlayerMove()
    {
        float tiltMultiplier = 1;
        if (gyroRot.x < 0.3 && gyroRot.x > -0.3)
        {
            if (gyroRot.x > 0)
            {
                tiltMultiplier = 0.8f + (gyroRot.x * 1.5f);
            }
            else if (gyroRot.x < 0)
            {
                tiltMultiplier = 0.8f + (-gyroRot.x * 1.5f);
            }
          
        }


        if (gyroRot.x > 0.1)
        {
            rb.AddForce(new Vector3(playerSpeed * tiltMultiplier, 0, 0), ForceMode.Impulse);
        }
        else if (gyroRot.x < -0.1)
        {
            rb.AddForce(new Vector3(-playerSpeed * tiltMultiplier, 0, 0), ForceMode.Impulse);
        }
        else if (gyroRot.x < 0.1 && gyroRot.x > -0.1)
        {

            if (rb.velocity.x > 0)
            {
                amountToDecrease--;
            }
            else if (rb.velocity.x < 0)
            {
                amountToDecrease++;
            }
            rb.AddForce(new Vector3(amountToDecrease, 0, 0), ForceMode.Force);

          
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemyBullet"))
        {
            GameObject vihunLuoti = other.gameObject;
            EnemyBulletScript enemyBulletScript = vihunLuoti.GetComponent<EnemyBulletScript>();
            playerHp -= enemyBulletScript.damage;
            healthBar.SetHealth(playerHp);
            Destroy(vihunLuoti);

            CheckPlayerHp();
        }
    }
    void CheckPlayerHp()
    {
        if (playerHp == 0 || playerHp < 0)
        {
            Destroy(this.gameObject);
        }
    }
    public Vector3 GetGyroRotation()
    {
        return gyroRot;
    }

    void GyroMove()
    {
       if (gyroActive)
        {
            gyroRot = gyro.gravity;
        
        }
    }
    void MovePlayer()
    {
        gyroRot.z = Input.GetAxis("Horizontal");


        if (gyroRot.z < 0)
        {
            rb.AddForce(new Vector3(-playerSpeed, 0, 0), ForceMode.Impulse);
        }
        else if (gyroRot.z > 0)
        {
            rb.AddForce(new Vector3(playerSpeed, 0, 0), ForceMode.Impulse);
        }
        else if (gyroRot.z == 0)
        {

            if (rb.velocity.x > 0)
            {
                amountToDecrease--;
            }
            else if (rb.velocity.x < 0)
            {
                amountToDecrease++;
            }
            rb.AddForce(new Vector3(amountToDecrease, 0, 0), ForceMode.Force);
        }
    }
}
