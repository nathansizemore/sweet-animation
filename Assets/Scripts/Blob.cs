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
    private Transform fbx;
    private BlobChunk[] chunks;


#region MONOBEHAVIOR
    private void Awake() { SetupObjectRefs(); }
#endregion

#region PRIVATE
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
        fbx = transform.Find("Model").Find("blob");
        SetupBlobChunkRefs();
    }
#endregion
}
