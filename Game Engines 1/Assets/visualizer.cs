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
            Quaternion _rot = Quaternion.AngleAxis(i * -703125f, Vector3.forward);
            GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab, transform.position, _rot);
            _instanceCube.transform.position = this.transform.position;
            _instanceCube.transform.parent = this.transform;
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
                _cube[i].transform.localScale = new Vector3(10, AudioData._samples [i] * _maxscale, 10);
            }
        }
    }
}
