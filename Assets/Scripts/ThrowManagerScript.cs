using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class ThrowManagerScript : MonoBehaviour
{
    public float MaxPower = 1;
    public float MinPower = 0;
    public float Scaler = 10.5f;
    public float cooldownSecond = 1;
    private Timer_ timer = new Timer_();
    private Timer_ clearCooldownTimer = new Timer_();
    public GameObject indicator;
    public GameObject objectToThrow;
    public GameObject povManager;
    ChangeSceneScript changeSceneScript;
    private Vector3 throwCenter;
    public Image UIdeadZone;
    public Button clearButton;
    
    void Start()
    {
        throwCenter = new Vector3(Screen.width/2f,0.1f,0);
        changeSceneScript = povManager.GetComponent<ChangeSceneScript>();
    }

    public void reset()
    {
        timer.start(0);
        clearCooldownTimer.start(0);
    }
    
    float scalePower(float power)
    {
        power /= Screen.height;
        float scaledPower = Math.Max(Math.Min(MaxPower,power),MinPower) * Scaler;
        return scaledPower ;
    }

    private bool touchIsValid(Vector3 touchPosition)
    {
        return touchPosition.y > UIdeadZone.transform.position.y*2;
    }
    
    void Update()
    {
        timer.update(Time.deltaTime);
        clearCooldownTimer.update(Time.deltaTime);
        
        if (clearCooldownTimer.Ended)
        {
            clearButton.enabled = true;
            clearButton.GetComponentInChildren<TextMeshProUGUI >().text = "Clear";
        }
        else
        {
            clearButton.GetComponentInChildren<TextMeshProUGUI >().text = (clearCooldownTimer.Percentage*100).ToString("0") + "%";
        }
        
        if (! ChangeSceneScript.InUI && Input.touchCount > 0)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            if(!touchIsValid(touchPosition)) return;
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Began)
            {

                Ray ray = Camera.main.ScreenPointToRay(touchPosition);
                
                Plane plane = new Plane(Vector3.up, transform.position);
                    
                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 worldPosition = ray.GetPoint(distance);
                    Vector3 direction =  worldPosition - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction);

                    float ThrowPower = scalePower(touchPosition.y - throwCenter.y );
                    indicator.transform.rotation = rotation;
                    indicator.transform.localScale = new Vector3(1,1, ThrowPower*0.7f);
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended && timer.Ended)
            {
                timer.start(cooldownSecond);
                Ray ray = Camera.main.ScreenPointToRay(touchPosition);

                Plane plane = new Plane(Vector3.up, transform.position);

                if (plane.Raycast(ray, out float distance))
                {
                    Vector3 worldPosition = ray.GetPoint(distance);
                    Vector3 direction =  worldPosition - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    float ThrowPower = scalePower(touchPosition.y - throwCenter.y);
                    indicator.transform.rotation = rotation;
                    indicator.transform.localScale = new Vector3(1,1, ThrowPower*0.7f);
                    GameObject obj = Instantiate(objectToThrow, transform.position, rotation);
                    obj.GetComponent<ObjectToThrowScript>().setSpeed(ThrowPower);
                }
            }
        }
    }

    public void clearTable()
    {
        if (clearCooldownTimer.Ended)
        {
            clearButton.enabled = false;
            clearCooldownTimer.start(30);
            foreach (ObjectToThrowScript obj in ObjectToThrowScript.objectThrown)
            {
                obj.destroyObject();
            }
        }
    }
    
}