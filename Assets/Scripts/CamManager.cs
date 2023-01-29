using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
   public GameObject firstPerson;
    public GameObject thirdPerson;
    public bool camMode = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c")){
            firstPerson.SetActive(!firstPerson.activeInHierarchy);
            thirdPerson.SetActive(!thirdPerson.activeInHierarchy);
        }
    }
}
