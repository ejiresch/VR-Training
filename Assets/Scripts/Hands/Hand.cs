using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand : MonoBehaviour
{
    //Stores handPrefab to be Instantiated
    public GameObject handPrefab;
    public GameObject spawnedHand;

    public Renderer left, right;
    public Renderer left2, right2;

    //Stores what kind of characteristics we're looking for with our Input Device when we search for it later
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    //Stores the InputDevice that we're Targeting once we find it in InitializeHand()
    private InputDevice _targetDevice;
    private Animator _handAnimator;
    private SkinnedMeshRenderer _mesh;


    private bool shouldShow = true;
    private void Start()
    {
        InitializeHand();
        
    }

    private void InitializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //Call InputDevices to see if it can find any devices with the characteristics we're looking for
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        //Our hands might not be active and so they will not be generated from the search.
        //We check if any devices are found here to avoid errors.
        if (devices.Count > 0)
        {

            _targetDevice = devices[0];

           this.spawnedHand = Instantiate(handPrefab, transform);
            _handAnimator = this.spawnedHand.GetComponent<Animator>();
            _mesh = this.spawnedHand.GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }


    // Update is called once per frame
    // Update is called once per frame
    public void Update()
    {

        if (ProcessHandler.Instance.GetHandActive() == true)
        {
            shouldShow = false;
        }   
        else
        {
            shouldShow= true;
        }

       

        if (shouldShow==false) {
                 enableVisibility();
                }
      


        

            
        //Since our target device might not register at the start of the scene, we continously check until one is found.
        if (!_targetDevice.isValid)
        {
            InitializeHand();
        }
        else
        {
            UpdateHand();
        }

    }


    private void UpdateHand()
    {
            //This will get the value for our trigger from the target device and output a flaot into triggerValue
            if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                _handAnimator.SetFloat("Trigger", triggerValue);
               
            }
            else
            {
                _handAnimator.SetFloat("Trigger", 0);
                

            }
       
            //This will get the value for our grip from the target device and output a flaot into gripValuel
            if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                _handAnimator.SetFloat("Grip", gripValue);
                

            }
            else
            {
                _handAnimator.SetFloat("Grip", 0);
               

            }
        
    }

    /**
     * Deaktiviert das Hand Mesh
     */
    public void disableVisibility()
    {
        if (spawnedHand != null)
        {
            _mesh.enabled = false;
           }
    }

    /**
     * Aktiviert das Hand Mesh
     */
    public void enableVisibility()
    {
        if(spawnedHand!= null)
        {
            _mesh.enabled = true;

        }
    }
}