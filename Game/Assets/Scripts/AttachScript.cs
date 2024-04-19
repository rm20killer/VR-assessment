using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AttachScript : MonoBehaviour
{
        IXRSelectInteractable SelectInteractable;

        /// <summary>
        /// Handles the event when the script is enabled.
        /// </summary>
        protected void OnEnable()
        {
            SelectInteractable = GetComponent<IXRSelectInteractable>();
            if (SelectInteractable as Object == null)
            { 
                return;
            }
            // Add the OnSelectEntered listener to the selectEntered event on the SelectInteractable object
            SelectInteractable.selectEntered.AddListener(OnSelectEntered);
        }

        protected void OnDisable()
        {
            if (SelectInteractable as Object != null)
            {
                // Remove the OnSelectEntered listener from the selectEntered event on the Select 
                // helps with memory management
                SelectInteractable.selectEntered.RemoveListener(OnSelectEntered);
            }
        }

        /// <summary>
        /// Handles the event when an object is selected.
        /// </summary>
        /// <param name="args">The arguments related to the select entered event.</param>
        void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (args.interactorObject is not XRRayInteractor)
            {
                return;
            }

            var attachTransform = args.interactorObject.GetAttachTransform(SelectInteractable);
            var originalAttachPose = args.interactorObject.GetLocalAttachPoseOnSelect(SelectInteractable);
            if (attachTransform != null) attachTransform.SetLocalPose(originalAttachPose);
        }
}
