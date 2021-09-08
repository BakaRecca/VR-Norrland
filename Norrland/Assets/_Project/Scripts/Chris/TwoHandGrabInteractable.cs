using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrabInteractable : XRGrabInteractable
{
    public List<XRSimpleInteractable> secondHandGrabpoints = new List<XRSimpleInteractable>();
    private XRBaseInteractor secondInteractor;
    private Quaternion attachIntialRoation;
    public enum TwoHandRotationType { None, First, Second }
    public TwoHandRotationType twoHandRotationType;
    public bool SnapToSecondHand = true;
    private Quaternion intialRotationOffset;


    void Start()
    {
        foreach (var item in secondHandGrabpoints)
        {
            item.selectEntered.AddListener(OnSecondHandGrab);
            item.selectExited.AddListener(OnSecondHandRelease);
        }
    }


    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (secondInteractor && selectingInteractor)
        {
            if (SnapToSecondHand)
                selectingInteractor.attachTransform.rotation = GetTwoHandRotation();

            else
                secondInteractor.attachTransform.rotation = GetTwoHandRotation() * intialRotationOffset;
        }
        base.ProcessInteractable(updatePhase);
    }

    private Quaternion GetTwoHandRotation()
    {
        Quaternion targetRotation;

        if (twoHandRotationType == TwoHandRotationType.None)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
        }

        else if (twoHandRotationType == TwoHandRotationType.First)
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, selectingInteractor.transform.up);
        }

        else
        {
            targetRotation = Quaternion.LookRotation(secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position, secondInteractor.transform.up);
        }

        return targetRotation;

    }



    public void OnSecondHandGrab(SelectEnterEventArgs args)
    {
        secondInteractor = args.interactor;
        intialRotationOffset = Quaternion.Inverse(GetTwoHandRotation()) * secondInteractor.attachTransform.rotation;
    }

    public void OnSecondHandRelease(SelectExitEventArgs args)
    {
        secondInteractor = null;
    }



    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        attachIntialRoation = args.interactor.attachTransform.localRotation;
        base.OnSelectEntered(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        secondInteractor = null;
        args.interactor.attachTransform.localRotation = attachIntialRoation;
        base.OnSelectExited(args);
    }


    public override bool IsSelectableBy(XRBaseInteractor interactor)
    {

        bool isalreadygrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
        return base.IsSelectableBy(interactor) && !isalreadygrabbed;
    }
}
