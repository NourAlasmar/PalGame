using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    bool isPlayerAlive = true;
    
    
   
    

    //Referances 
    public Rigidbody2D rb;
    public GameObject playerVisual;
    public AudioSource dashSFX, dieSFX;
    public ParticleSystem dashVFX, dieVFX;


    //Dash
    public float dashPower = 5f;
    public float torquePower = 5f;
    int dashcount = 0;
    public float dashDotweenPower = 1f, dashDotweenDuration = 0.3f;

    //Base
    Vector2 baseScale;

    //Camera Shake Proprties
    [Header("Camera Shake Proprites")]
    [Range(0.05f, 50f)]
    public float dashShakePower = 0.5f;
    [Range(0.05f, 50f)]
    public float dashShakeDuration = 0.4f;

    [Header("Gravity")]
    public float sideGravityPower = 1f;
    public float sideGrabityPerAngleUnity = 0.07f;
    public Vector2 sideGeabityMinMax = new Vector2(1f, 2.5f);

    [Header("Debug Tools")]
    public bool d_PlayerNeverDie = true;


    // Start is called before the first frame update
    void Start()
    {
        baseScale= transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        DashUpdate();
        GravityUpdate();

    }


    void GravityUpdate()
    {
        var _angle = CameraManager._inst.CurrentCameraAngle;
        var _power = Mathf.Abs(_angle* sideGrabityPerAngleUnity);
        if (_power < sideGeabityMinMax.x) _power = 1f;
        if (_power > sideGeabityMinMax.y) _power = 2.5f;
        if (_angle > 8)
        {
            rb.velocity += new Vector2(1, 0) * Time.deltaTime *_power * sideGravityPower;
        }
        else if(_angle < -8)
        {
            rb.velocity += new Vector2(-1, 0) * Time.deltaTime *_power * sideGravityPower;
        }

    }


    void DashUpdate()
    {
        if (dashcount < 2 && Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }

    void Dash()
    {
        if (!isPlayerAlive) return;
        dashcount++;
        float direction = Input.GetAxisRaw("Horizontal");

        
        if (dashcount == 1)
        {
            Vector2 dashVector = new Vector2(direction, 1.25f);
            rb.AddForce(dashVector * dashPower, ForceMode2D.Impulse);
            rb.AddTorque(torquePower * direction, ForceMode2D.Impulse);
            transform.localScale = baseScale;
        }
        else
        {
            Vector2 dashVector = new Vector2(direction * 1.7f, 0.3f) ;
            rb.AddForce(dashVector * dashPower, ForceMode2D.Impulse);
            rb.AddTorque(torquePower * direction, ForceMode2D.Impulse);
        }

        dashSFX.Play();
        Instantiate(dashVFX,transform.position - new Vector3(0,0.5f,0f),Quaternion.identity);
        transform.DOShakeScale(dashDotweenDuration, dashDotweenPower).OnComplete(dashDotweeenFinish);
        CameraShake._inst.Shake(dashShakeDuration, dashShakePower);

    }

    void dashDotweeenFinish()
    {
        transform.localScale = baseScale;
    }
    void resetDashCount()
    {
        dashcount= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (d_PlayerNeverDie) return;
        if (!isPlayerAlive) return;
        
            isPlayerAlive = false;
            dieVFX.Play();
            dieSFX.Play();
            GamerManager._inst.GameOver();
            playerVisual.SetActive(false);
            CameraShake._inst.Shake(dashShakeDuration *10f , dashShakePower*10f);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        resetDashCount();
    }

}

