// Copyright 2017 Nathan Sizemore <nathanrsizemore@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is furnished
// to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.


using UnityEngine;


public class Blob : MonoBehaviour
{
    private const int NUM_SAMPLES = 1024;
    private const float VOLUME = 1.0f;


    private AudioSource audioSource;
    private Transform fbx;

    private BlobChunk[] chunks;

    private float[] samples = new float[NUM_SAMPLES];

    private int numLoops = 0;
    private float sumRms = 0.0f;


#region MONOBEHAVIOR
    private void Awake()
    {
        SetupObjectRefs();
        // SetupAudioData();
    }

    private void Update()
    {
        float rms, averageRms;
        rms = GetRms();
        sumRms += rms;
        numLoops += 1;
        averageRms = sumRms / numLoops;

        if (rms < averageRms)
            rms = 0.0f;

        for (int x = 0; x < chunks.Length; x++)
            chunks[x].sample = rms;
    }
#endregion

#region PRIVATE
    private float GetRms()
    {
        float sum = 0.0f;
        audioSource.GetOutputData(samples, 0);
        for (int x = 0; x < NUM_SAMPLES; x++)
            sum += samples[x] * samples[x];

        float rmsValue = Mathf.Sqrt(sum / NUM_SAMPLES);
        float dbValue = 20 * Mathf.Log10(rmsValue / 0.1f);
        if (dbValue < -160)
            dbValue = -160;

        return rmsValue * VOLUME;
    }

    private void SetupAudioData()
    {
        int numSamples;
        float min, mean, max;

        min = 0.0f;
        mean = 0.0f;
        max = 0.0f;
        numSamples = audioSource.clip.samples;
        samples = new float[numSamples];

        audioSource.clip.GetData(samples, 1);
        for (int x = 0; x < numSamples; x++)
        {
            float sample = samples[x];
            if (sample < min)
                min = sample;
            else if (sample > max)
                max = sample;

            mean += sample;
        }

        mean /= (float)numSamples;

        for (int x = 0; x < chunks.Length; x++)
        {
            chunks[x].sampleMin = Random.Range(min, mean);
            chunks[x].sampleMax = max;
        }
    }

    private void SetupBlobChunkRefs()
    {
        int numChildren = fbx.childCount;
        chunks = new BlobChunk[numChildren];
        for (int x = 0; x < numChildren; x++)
        {
            chunks[x] = fbx.GetChild(x)
                .gameObject
                .GetComponent<BlobChunk>();
        }
    }

    private void SetupObjectRefs()
    {
        audioSource = GetComponent<AudioSource>();
        fbx = transform.Find("Model").Find("blob");
        SetupBlobChunkRefs();
    }
#endregion
}
