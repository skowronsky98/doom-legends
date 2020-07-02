using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private List<Transform> transformSpace = new List<Transform>();

    private void Awake()
    {
        //Fill the list of board positions 
        gameObject.GetComponentsInChildren<Transform>(transformSpace);

        //foreach (Transform i in transformSpace)
        //    Debug.Log(i.position.x + " -> " + i.position.y);

    }
    public List<Transform> TransformSpace { get { return transformSpace; } }
}
