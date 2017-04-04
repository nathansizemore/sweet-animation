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


public class BlobChunk : MonoBehaviour
{
    private const float UPDATE_INTERVAL = 0.05f;


    public float sampleMin = 0.0f;
    public float sampleMax = 0.0f;

    public float sample = 0.0f;


    private float waited = 0.0f;

    private Vector3 origin = Vector3.zero;
    private Vector3 vector = Vector3.zero;


#region MONOBEHAVIOR
    private void Awake() { CalculateVector(); }

    private void Update()
    {
        UpdatePosition();

        // waited += Time.deltaTime;
        // if (waited > UPDATE_INTERVAL)
        // {
        //     UpdatePosition();
        //     waited = 0.0f;
        // }
    }
#endregion

#region PRIVATE
    private void CalculateVector()
    {
        origin = transform.position;
        vector = origin - transform.parent.position;
    }

    private void UpdatePosition()
    {
        float mappedSample = BlobChunk.Map(sample,
                                           sampleMin,
                                           sampleMax,
                                           0.0f,
                                           1.1f);

        //Vector3 newPosition = origin + (vector * mappedSample);
        Vector3 newPosition = origin + (vector * sample);
        transform.position = newPosition;
    }

    private static float Map(float value,
                             float fromMin,
                             float fromMax,
                             float toMin,
                             float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }
#endregion
}
