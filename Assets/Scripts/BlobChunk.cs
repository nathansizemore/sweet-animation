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
    private const int PATH_LEN = 50;
    private const float TRAVEL_MIN = 0.25f;
    private const float TRAVEL_MAX = 0.75f;
    private const float UPDATE_INTERVAL = 0.25f;


    private Vector3 origin = Vector3.zero;
    private Vector3 vector = Vector3.zero;

    private int pathOffset = 0;
    private int offsetIncrementor = 1;
    private readonly Vector3[] path = new Vector3[PATH_LEN];

    private float waited = 0.0f;


#region MONOBEHAVIOR
    private void Awake()
    {
        CalculateVector();
        GeneratePath();
    }

    private void Update()
    {
        waited += Time.deltaTime;
        if (waited > UPDATE_INTERVAL)
        {
            UpdatePosition();
            UpdateRotation();
        }
    }
#endregion

#region PRIVATE
    private void CalculateVector()
    {
        origin = transform.position;
        vector = origin - transform.parent.position;
    }

    private void GeneratePath()
    {
        float t, incrementor, distance;
        distance = RandomDistance();
        incrementor = distance / PATH_LEN;
        t = 0.0f;

        Vector3 end = origin + (vector * distance);
        for (int x = 0; x < PATH_LEN; x++)
        {
            Vector3 point = Vector3.Lerp(origin, end, t);
            // Vector3 point = Vector3.Slerp(origin, end, t);
            path[x] = point;

            t += incrementor;
        }
    }

    private float RandomDistance()
    {
        return Random.Range(TRAVEL_MIN, TRAVEL_MAX);
    }

    private void UpdatePosition()
    {
        const int OFFSET_MIN = 0;
        const int OFFSET_MAX = PATH_LEN - 1;

        if (pathOffset == OFFSET_MIN)
            offsetIncrementor = 1;
        else if (pathOffset == OFFSET_MAX)
            offsetIncrementor = -1;

        transform.position = path[pathOffset];
        pathOffset += offsetIncrementor;
    }

    private void UpdateRotation()
    {

    }
#endregion
}
