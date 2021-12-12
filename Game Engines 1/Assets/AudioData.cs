using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] //to get audio 
public class AudioData : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[4096]; //can be accessed from any script

        void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource(); 
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Hamming);// gets data from audio source, 512 samples, fft window seperates frequencies, blackman used to reduce leakage
    }
}
