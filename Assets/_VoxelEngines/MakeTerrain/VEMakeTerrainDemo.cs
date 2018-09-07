using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cubiquity;
using UnityEngine;
using UnityEditor;


namespace VE.VoxelGen
{
    public class VEMakeTerrainDemo : VEGenerateVolumeBase
    {

        float Gradient(float input, float range)
        {
            return input / range;
        }

        protected override void MakeChunk(ColoredCubesVolumeData data)
        {

            

            QuantizedColor grass = voxelTypeToColor.getQuantizedColor(VoxelType.Grass);
            QuantizedColor empty = new QuantizedColor(0, 0, 0, 0);

            int debugCount = 0;

            //
            // Iterate over every voxel in the volume
            //
            for (int z = 0; z < size.x; z++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    for (int x = 0; x < size.z; x++)
                    {

                        float invNoiseScale = 1f / noiseScale;
                        // Simplex noise is quite high frequency. We scale the sample position to reduce this.
                        float sampleX = x * invNoiseScale;
                        float sampleY = y * invNoiseScale;
                        float sampleZ = z * invNoiseScale;
                        // ranges from -1 to +1
                        float noise2DValue = SimplexNoise.Noise.Generate(sampleX, sampleZ);
                        // adjust the range to -.5 to +.5
                        noise2DValue = noise2DValue * .5f;


                        float heightOfWorld = size.y;

                        //
                        // if y = 0, testDepth = heightOfWorld
                        // if y = heightOfWorld - 1, testDepth = close to zero, small number
                        //
                        float testDepth = heightOfWorld - y;



                        // adjust the range by 'snoise2DScale'
                        //noiseValue = noiseValue * snoise2DScale;

                        //
                        // 'Perturb' the testDepth using the noiseValue.
                        // In other words, push down testDepth if noiseValue 
                        // is negative.
                        // This gives higher voxels a chance to be solid (creates hills).
                        // Push up testDepth if noiseValue is positive.
                        // These values now have a greater chance of being air (creates valleys).
                        //
                        testDepth = (testDepth) / heightOfWorld + noise2DValue; // + (heightOfWorld * noise2DValue);

                        float testIsAVoxel = testDepth; // Gradient(testDepth, heightOfWorld); // testDepth, heightOfWorld);

                        

                        if (testIsAVoxel > isSolidThreshhold)
                        {
                            data.SetVoxel(x, y, z, grass);
                            debugCount++;
                        }
                        else
                        {
                            data.SetVoxel(x, y, z, empty);
                        }

                    }
                }
            }

            Debug.Log("generated " + debugCount + " solid voxels out of area: " + (size.x * size.y * size.z) +
                ". solid / area ratio: " + (debugCount / (float)(size.x * size.y * size.z)));
        }
    }


}
