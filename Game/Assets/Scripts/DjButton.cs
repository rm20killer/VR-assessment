using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class DjButton : MonoBehaviour
{
    
    private XRBaseInteractable interactable;
    private bool isFollowing = false;
    
    private Vector3 offset;
    private Transform lastPosition;

    public Vector3 localAxis;
    private Vector3 startLocalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        startLocalPos = transform.localPosition;
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
    }

    public void Follow(BaseInteractionEventArgs args)
    {
        if(args.interactor is XRPokeInteractor)
        {
            XRPokeInteractor pokeInteractor = (XRPokeInteractor)args.interactor;
            isFollowing = true;
            
            lastPosition = pokeInteractor.transform;
            offset = transform.position - lastPosition.position;
        }
    }

    public void Reset(BaseInteractionEventArgs args)
    {
        if(args.interactor is XRPokeInteractor)
        {
            isFollowing = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            Vector3 localPos = lastPosition.InverseTransformPoint(transform.position + offset);
            Vector3 constrainedLocalPos = Vector3.Project(localPos, localAxis);
            
            lastPosition.position = lastPosition.TransformPoint(constrainedLocalPos);
        }
        else
        {
            lastPosition.localPosition = startLocalPos;
        }
    }
}
