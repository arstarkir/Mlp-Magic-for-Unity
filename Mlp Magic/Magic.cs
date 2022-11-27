using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Magic : MonoBehaviour
{
    Rigidbody rb; // Rigidbody of the object on wich you are using magic
    public float Dist = 5, mana = 100; // starting valuse of Distanse to controled object
    GameObject go = null;
    //public GameObject sparks; //an empty gameObject with sparcs efect
    Vector3 Target;
    Coroutine moveAction;
    Rigidbody interactObj;
    bool cM = false, tr = false;

    public float UseOfMagic(float m) // mana - mass * (distanse to controled object/3)
    {
        mana = mana - m * (Dist / 3) * Time.fixedDeltaTime;
        if (mana < 100)
        {
            if (cM == false)
            {
                cM = true;
                StartCoroutine(ManaRegen());
                return (mana);
            }
            else
            {
                return (0);
            }
        }
        else
            return (mana);
    }
    public void ManaUI() // warks on ui of the mana 
    {
        GameObject magic_count_UI = GameObject.Find("Magic Count");
        magic_count_UI.gameObject.transform.localScale = new Vector3((float)(mana * 0.01/* if you would want to change the max amount of mana from 100 to something else * mana by something to get 100*/), 1, 1); 
    }

    void Start()
    {
        ManaUI();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            RaycastHit hitM;
            if (Physics.Raycast(transform.position, transform.forward, out hitM)
                && mana > 0
                && hitM.transform.gameObject.tag == "Levitatable") // casts a ray and puts it in "hitM" then chekes if mana > 0 and if the object you are trying to controle has a tag "Levitatable"
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0 && Dist < 10) //controlse a distns from player to controled obj
                {
                    Dist = Dist + 0.2f;
                    Debug.Log(Dist);
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0 && Dist > 2) //controlse a distns from player to controled obj
                {
                    Dist = Dist - 0.2f;
                    Debug.Log(Dist);
                }
                hitM.transform.gameObject.GetComponent<Renderer>().material.SetOverrideTag("Magic", "true"); // adds the magic tag to material. material now has aura 
                interactObj = hitM.transform.gameObject.GetComponent<Rigidbody>(); //used to get mass of the obj
                Target = transform.position + transform.forward * Dist; // calculates a target location
                go = hitM.transform.gameObject;
                rb = go.GetComponent<Rigidbody>(); // used to turn on or off Gravity
                rb.useGravity = false;
                if (tr == false) // starts the proces of movement to a target
                    moveAction = StartCoroutine(MoveToPosition(go.transform.position));
            }
        }

        if (go != null) // turns off all
        {
            Target = transform.position + transform.forward * Dist;
            if (Input.GetKeyUp(KeyCode.Q))
            {
                rb.useGravity = true;
                go.transform.gameObject.GetComponent<Renderer>().material.SetOverrideTag("Magic", "false");
                go = null;
                tr = false;
                StopAllCoroutines();
                StartCoroutine(ManaRegen());
            }
        }
    }
    public IEnumerator ManaRegen() // restores mana 
    {
        while (mana < 100)
        {
            yield return new WaitForSeconds(0.15f);
            mana = mana + 0.5f;
            ManaUI();
        }
        cM = false;
    }
    IEnumerator MoveToPosition(Vector3 start)
    {
        while (true)
        {
            tr = true;
            if (mana > 0)
            {
                UseOfMagic(interactObj.mass);
                ManaUI();
                rb.AddForce(5 * (Target - go.transform.position) - rb.velocity); // moves to a targeted location
            }
            else // turns off all
            {
                go.transform.gameObject.GetComponent<Renderer>().material.SetOverrideTag("Magic", "false");
                Target = transform.position + transform.forward * Dist;
                rb.useGravity = true;
                go = null;
                tr = false;
                Dist = 5;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}