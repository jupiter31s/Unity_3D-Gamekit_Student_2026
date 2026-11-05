using Gamekit3D;
using UnityEngine;

public class SurvivalMod : MonoBehaviour
{
    [SerializeField] private float thirstLevel = 100f;
    [SerializeField] private float thirstSpeed;
    [SerializeField] private float timer = 10f;
    
    [SerializeField] private bool thirsty;
    [SerializeField] private bool parched;

    public int waterBottles = 0;
    
    
    [SerializeField] private GameObject playerObject;
    
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    
    // Update is called once per frame
    void Update()
    {
        /* Subtracting the current value of thirstLevel with thirstSpeed.
         thistSpeed is normalised by using Time.deltaTime, so that it's frame rate independant. 
         MathClamp restricts the thirstLevel between its current max value and 0.
        */
        thirstLevel -= Mathf.Clamp(thirstSpeed*Time.deltaTime, 0, thirstLevel);

        //if statement checks if thirstLevel is equal to, or lower, than 50, to check for thirsty status
        if (thirstLevel <= 50f && thirsty == false)
        {
            Debug.Log("Getting thirsty..");
            thirsty = true;
        }
        
        //if statement checks if thirstLevel is equal to, or lower than 15, to check for parched status
        if (thirstLevel <= 15f && parched == false)
        {
            Debug.Log("Get some water now!!..");
            parched = true;
        }
        
        
        
        //if statement checks if thirst is fully depleted to take player damage
        if (thirstLevel <= 0 && thirsty)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Damageable.DamageMessage msg = new Damageable.DamageMessage()
                {
                    amount = 1,
                    damager = this,
                    damageSource = new Vector3(0, 0, 0),
                    direction = new Vector3(0, 0, 0),
                    throwing = false,
                    stopCamera = false
                };
                playerObject.GetComponent<Damageable>().ApplyDamage(msg);
                Debug.Log("Damage by thirst!");
                
                timer = 10f;
            }
            
        }
        
    }

    public void AddWater()
    {
        Debug.Log("added random water");
    }
}
