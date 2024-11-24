using System.Collections.Generic;
using UnityEngine;

namespace UsefulHelpers.Index
{
    public static class IndexHelper
    {
        [Sirenix.OdinInspector.Button]
        public static void TurnIndices90DegreesCounterClockwise(ref Vector2Int centerIndex, ref Vector2Int gridSize, IList<Vector2Int> indicesToTurn)
        {
            var oldCenterIndex = centerIndex;
            centerIndex = new Vector2Int(gridSize.y - 1 - centerIndex.y, centerIndex.x);
            TurnIndicesAroundCenterIndices90DegreesCounterClockwise(gridSize, oldCenterIndex, centerIndex, indicesToTurn);
            gridSize = new Vector2Int(gridSize.y, gridSize.x);
        }

        [Sirenix.OdinInspector.Button]
        public static void TurnIndices90DegreesClockwise(ref Vector2Int centerIndex, ref Vector2Int gridSize, IList<Vector2Int> indicesToTurn)
        {
            var oldCenterIndex = centerIndex;
            centerIndex = new Vector2Int(centerIndex.y, gridSize.x - 1 - centerIndex.x);
            TurnIndicesAroundCenterIndices90DegreesClockwise(gridSize, oldCenterIndex, centerIndex, indicesToTurn);
            gridSize = new Vector2Int(gridSize.y, gridSize.x);
        }

        [Sirenix.OdinInspector.Button]
        public static void TurnIndices90DegreesCounterClockwise(ref Vector2Int centerIndex, ref Vector2Int gridSize, ICollection<Vector2Int> indicesToTurn)
        {
            var oldCenterIndex = centerIndex;
            centerIndex = new Vector2Int(gridSize.y - 1 - centerIndex.y, centerIndex.x);
            gridSize = new Vector2Int(gridSize.y, gridSize.x);
            SetOffsetIndicesBasedOnSizeAndCenterIndex(gridSize, centerIndex, indicesToTurn);
        }

        [Sirenix.OdinInspector.Button]
        public static void TurnIndices90DegreesClockwise(ref Vector2Int centerIndex, ref Vector2Int gridSize, ICollection<Vector2Int> indicesToTurn)
        {
            var oldCenterIndex = centerIndex;
            centerIndex = new Vector2Int(centerIndex.y, gridSize.x - 1 - centerIndex.x);
            gridSize = new Vector2Int(gridSize.y, gridSize.x);
            SetOffsetIndicesBasedOnSizeAndCenterIndex(gridSize, centerIndex, indicesToTurn);
        }

        static void TurnIndicesAroundCenterIndices90DegreesCounterClockwise(Vector2Int gridSize, Vector2Int oldCenterIndex, Vector2Int newCenterIndex, IList<Vector2Int> indices)
        {
            int collectionSize = indices.Count;

            for (int i = 0; i < collectionSize; i++)
            {
                Vector2Int index = indices[i];
                index += oldCenterIndex;
                index = new Vector2Int(gridSize.y - 1 - index.y, index.x);
                index -= newCenterIndex;
                indices[i] = index;
            }
        }

        static void TurnIndicesAroundCenterIndices90DegreesClockwise(Vector2Int gridSize, Vector2Int oldCenterIndex, Vector2Int newCenterIndex, IList<Vector2Int> indices)
        {
            int collectionSize = indices.Count;

            for (int i = 0; i < collectionSize; i++)
            {
                Vector2Int index = indices[i];
                index += oldCenterIndex;
                index = new Vector2Int(index.y, gridSize.x - 1 - index.x);
                index -= newCenterIndex;
                indices[i] = index;
            }
        }

        static void SetOffsetIndicesBasedOnSizeAndCenterIndex(Vector2Int gridSize, Vector2Int centerIndex, ICollection<Vector2Int> indices)
        {
            int offsetIndicesSize = gridSize.x * gridSize.y;
            indices.Clear();

            int arrayIndex = 0;
            for (int x = 0; x < gridSize.x; x++)
                for (int z = 0; z < gridSize.y; z++)
                {
                    Vector2Int index = new(x, z);
                    index = index - centerIndex;

                    indices.Add(index);
                    arrayIndex++;
                }
        }
    }
}
