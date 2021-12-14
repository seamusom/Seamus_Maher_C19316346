using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualizer : MonoBehaviour
{
    public GameObject _cubePrefab;
    GameObject[] _cube = new GameObject[512];
    public float _maxscale;


    void Start()
    {

        for (int i = 0; i < 512; i++)
        {
            Quaternion _rot = Quaternion.AngleAxis(i * -703125f, Vector3.forward);//rotates each cube
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab, transform.position, _rot);//instastiates each cube
            _instanceCube.transform.position = transform.position;
            _instanceCube.transform.parent = transform;
            _instanceCube.name = "cube" + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instanceCube.transform.position = Vector3.forward * 100;

            _cube[i] = _instanceCube;
        }
     
        
    }

    
    void Update()
    {
        for (int i = 0; i < 512 ; i++)
        {
            if (_cube != null)
            {
                _cube[i].transform.localScale = new Vector3(10, AudioData._samples [i] * _maxscale, 10);//changes the size of each cube relative to the samples frequency
            }
        }
    }
}
