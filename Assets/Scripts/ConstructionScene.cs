using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class ConstructionScene : MonoBehaviour
{

  public GameObject Car;
  public Vector3 carStartScale = new Vector3(0.3f, 0.3f, 1f);
  public Vector3 carEndScale = new Vector3(1f, 1f, 1f);
  public Vector3 carStartPos = new Vector3(0.3f, 0.3f, 1f);
  public Vector3 carEndPos = new Vector3(1f, 1f, 1f);
  public float carScaleDuration = 1f;
  public GameObject StopSign;
  private BoxCollider2D signBoxCollider;
  private SpriteRenderer signSpriteRender;
  public Sprite signFront;
  public Sprite signBack;
  private Boolean isSignBack = false;
  private DateTime signDoneChangingAfter = DateTime.Now;
  private DateTime sceneStartTime;

  // Start is called before the first frame update
  void Start()
  {
    sceneStartTime = DateTime.Now;

    LeanTouch.OnFingerSwipe += Swiped;

    Debug.Log(StopSign.GetInstanceID().ToString());
    signBoxCollider = StopSign.GetComponent<BoxCollider2D>();
    signSpriteRender = StopSign.GetComponent<SpriteRenderer>();
    Debug.Log(signBoxCollider.GetInstanceID().ToString());
  }

  // Update is called once per frame
  void Update()
  {
    float delta = Mathf.Clamp((float)(DateTime.Now - sceneStartTime).TotalMilliseconds, 0f, carScaleDuration * 1000) / (carScaleDuration * 1000);
    Debug.Log(delta);
    Car.transform.localScale = Vector3.Lerp(carStartScale, carEndScale, delta);
    Car.transform.position = Vector3.Lerp(carStartPos, carEndPos, delta);
  }

  void Swiped(LeanFinger finger)
  {
    if (DateTime.Now < signDoneChangingAfter)
    {
      Debug.Log("still changing");
      return;
    }
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
        signDoneChangingAfter = DateTime.Now.AddSeconds(1);

        isSignBack = !isSignBack;
        Debug.Log(isSignBack);
        if (isSignBack)
        {
          signSpriteRender.sprite = signBack;
        }
        else
        {
          signSpriteRender.sprite = signFront;
        }
        Debug.Log("hit sign");
      }
      else
      {
        Debug.Log("No collisiion!");
      }
    }
  }
}
