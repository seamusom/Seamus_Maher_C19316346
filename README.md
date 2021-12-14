 # Audio Responsive Enviroment

Name: Séamus Maher

Student Number: C19316346

Class Group: Game Engines 1, Dame Design

# Project Description

For my project I built an audio responsive enviroment using infinitly generated terrain, and instantiating prefabs to create an a visual representaion of the audio

![image](https://user-images.githubusercontent.com/55447839/146017361-fde3f49e-a70d-4370-be53-a38154978911.png)

# Instructions

* To use my project launch the game from file

* Use the left mouse button to move the camera around the terrain

* And to speed up the movement of the terrain use the scroll wheel

[here](https://youtu.be/SHKqKMl4WHg) is my project running

# How It Works

For the Visualizer first we get data from the audio using GetSpectrumData in our audio data script
```
[RequireComponent(typeof(AudioSource))] //to add audio componant to object this is connected to 

public class AudioData : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[8192]; //can be accessed from any script

        void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetAudioData(); 
    }

    void GetAudioData()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Hamming);// gets data from audio source, 512 samples, fft window seperates frequencies, Hamming used to reduce leakage
    }
}
```

we then use this data in our Visualizer script to change to scale of each cube relative to each sample, the cube is called from a prefab, each cubes scale is relative to an audio sample found in our audio data script

```
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
                _cube[i].transform.localScale = new Vector3(10, AudioData._samples [i] * _maxscale, 10);//changes the size of each cube relative to the samples frequency
            }
        }
    }
}
```
  
  the prefab has a color randomizer script
    
    void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); //sets random color of material on the cube
    }
  
  the terrain uses perlin noise to generate a random noise map 
  ```
  
  public class perlinNoise : MonoBehaviour
{
   
    public int height = 256;
    public int depth = 20;
    public int width = 256;

    public float scale = 20f;

    public float offsetx = 100f;
    public float offsety = 100f;
    public float Speed = 1f;

    

     void Start()
    {
        
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        if (Input.GetAxis("Mouse ScrollWheel")<0) //changes speed and direction of terrain
        {
            Speed += 1;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Speed -= 1;
        }


        offsety += Time.deltaTime * Speed; // moves the terrain in the y direction relative to the speed by time


    }

    TerrainData GenerateTerrain (TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalHeight(x, y);
            }
        }

        return heights;
    }

    

    float CalHeight(int x, int y)
    {
        float xCoord = (float) x / width * scale + offsetx; 
        float yCoord = (float) y / height * scale + offsety;

        return Mathf.PerlinNoise(xCoord, yCoord);//generates a random value between 0 and 1 on every point on the terrain to give use our heights and depths
    }

    
   
}
```

Camera Movement around the terrain

```
public class cameraMovement : MonoBehaviour
{
    public Camera _cam;
    public Transform _target;
    public float _distanceToTarget = 10;

    private Vector3 previousPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition); // this sets up the camera 
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            _cam.transform.position = _target.position;

            _cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            _cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);// rotates relative to the worlds y axis 

            _cam.transform.Translate(new Vector3(0, 0, -_distanceToTarget));

            previousPosition = newPosition;
        }
    }
}
```

# References
   
  | Class | Source |
  | ----- | ----- |
  | perlinNoise.cs | Modified from [source](https://www.youtube.com/watch?v=bG0uEXV6aHQ) |
  | color.cs | self written |
  | visualizer.cs | Modified from [reference](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html) and [reference](https://docs.unity3d.com/ScriptReference/Quaternion.html) |
  | AudioData.cs | modified from [reference](https://docs.unity3d.com/ScriptReference/AudioSource.GetSpectrumData.html) |
  | cameraMovment.cs | taken from [reference](https://www.emmaprats.com/p/how-to-rotate-the-camera-around-an-object-in-unity3d/) |
  
  | Asset | Source |
  | ----- | ----- |
  | music | taken from [here](https://www.youtube.com/watch?v=OgZ1doqlRpI) |
  | skybox | taken from [here](https://assetstore.unity.com/packages/2d/textures-materials/sky/free-skyboxes-space-178953) |
  
  # What I am most proud of in this assignment
  
  the thing I am most proud of in this assignment is the instatiation of the prefab around the terrain as trying to get it working took a lot of time with many mistakes along the way.
  
  # Proposal
  
  For this project I will make an audio responsive enviroment using perlin noise with a terrain with a custom controller to change the enviroment
  
  
