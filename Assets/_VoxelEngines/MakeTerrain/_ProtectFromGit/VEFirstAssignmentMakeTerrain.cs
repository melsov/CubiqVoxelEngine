using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cubiquity;
using UnityEngine;
using UnityEditor;


namespace VE.VoxelGen
{
    public class VEFirstAssignmentMakeTerrain : VEGenerateVolumeBase
    {



        protected override void MakeChunk(ColoredCubesVolumeData data)
        {

            float invNoiseScale = 1f / noiseScale;

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

                        ///
                        /// FIRST ASSIGNMENT
                        /// ADD YOUR CODE HERE
                        ///

                        // Delete this line
                        data.SetVoxel(x, y, z, empty); // <<---Delet me!!!

                    }
                }
            }

            Debug.Log("generated " + debugCount + " solid voxels out of area: " + (size.x * size.y * size.z) +
                ". solid / area ratio: " + (debugCount / (float)(size.x * size.y * size.z)));
        }
    }


}
