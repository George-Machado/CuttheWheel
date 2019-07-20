using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class ConstructionScene : MonoBehaviour
{

  public GameObject StopSign;
  private BoxCollider2D signBoxCollider;

  // Start is called before the first frame update
  void Start()
  {
    LeanTouch.OnFingerSwipe += Swiped;

    Debug.Log(StopSign.GetInstanceID().ToString());
    signBoxCollider = StopSign.GetComponent<BoxCollider2D>();
    Debug.Log(signBoxCollider.GetInstanceID().ToString());
  }

  // Update is called once per frame
  void Update()
  {

  }

  void Swiped(LeanFinger finger)
  {
    Debug.Log("--------------------------");
    Debug.Log("SWIPED");

    var swipeStart = finger.GetStartWorldPosition(0);
    var swipeEnd = finger.GetLastWorldPosition(0);
    var heading = swipeEnd - swipeStart;
    var distance = heading.magnitude;
    var direction = heading / distance;
    RaycastHit2D[] hits = Physics2D.RaycastAll(swipeStart, direction);
    foreach (var hit in hits)
    {
      if (hit.collider == signBoxCollider)
      {
        StopSign.SetActive(!StopSign.activeSelf);
        Debug.Log("hit sign");
      }
      else
      {
        Debug.Log("No collisiion!");
      }
    }
  }
}
