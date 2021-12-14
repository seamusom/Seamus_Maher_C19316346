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

For the Visualizer first we get data from the audio using GetSpectrumData in our audio data script

void GetAudioData()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Hamming);// gets data from audio source, our samples, fft window seperates frequencies, hamming is used to reduce leakage
    }

we then use this data in our Visualizer script to change to scale of each cube relative to each sample

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
            
   each cube is instantiated from a prefab
   
    Quaternion _rot = Quaternion.AngleAxis(i * -703125f, Vector3.forward);//rotates each cube
    GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab, transform.position, _rot);//instastiates each cube
    
   the prefab has a color randomizer script
    
    void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f); //sets random color of material on the cube
    }
  
  the terrain uses perlin noise to generate a random noise map 
  
  float CalHeight(int x, int y)
    {
    
        float xCoord = (float) x / width * scale + offsetx; 
        float yCoord = (float) y / height * scale + offsety;

        return Mathf.PerlinNoise(xCoord, yCoord);//generates a random value between 0 and 1 on every point on the terrain to give use our heights and depths
    }
    
    
  
