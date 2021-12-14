Audio Responsive Enviroment

Séamus Maher

C19316346

Game Engines 1

For my project I built an audio responsive enviroment using terrain, and instantiating prefabs

to use my project launch the game from file, use the left mouse button to move the camera around the terrain, and to speed up the movement of the terrain use the scroll wheel

For the Visualizer first we get data from the audio using GetSpectrumData

  void GetAudioData()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Hamming);// gets data from audio source, 512 samples, fft window seperates frequencies, blackman used to reduce leakage
    }
