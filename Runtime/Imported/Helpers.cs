/*
 Copyright 2024 absencee_

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the “Software”), to deal in the 
Software without restriction, including without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to 
whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.Linq;
using UnityEngine;

namespace com.absence.attributes.imported
{
    /// <summary>
    /// Holds some handy functions.
    /// </summary>
    internal static class Helpers
    {
        public const float K_SPACING = 5f;
        public const float K_PADDING = 5f;

        /// <summary>
        /// Splits input rect by specified parameters.
        /// </summary>
        public static Rect[] SliceRectHorizontally(Rect rect, int pieceCount, params float[] pieceSizeCoefficients)
        {
            return SliceRectHorizontally(rect, pieceCount, K_SPACING, K_PADDING, pieceSizeCoefficients);
        }

        /// <summary>
        /// Splits input rect by specified parameters.
        /// </summary>
        public static Rect[] SliceRectHorizontally(Rect rect, int pieceCount, float overrideSpacing, float overridePadding, params float[] pieceSizeCoefficients)
        {
            if (pieceCount <= 0) return null;

            if (pieceSizeCoefficients == null)
            {
                pieceSizeCoefficients = new float[pieceCount];
                for (int i = 0; i < pieceCount; i++)
                {
                    pieceSizeCoefficients[i] = 1f;
                }
            }

            Rect[] result = new Rect[pieceCount];

            float totalWidth = rect.width - ((2 * overridePadding) + ((pieceCount - 1) * overrideSpacing));
            float generalCoefficient = totalWidth / pieceSizeCoefficients.Sum();

            float horizontalPointer = rect.x + overridePadding;
            for (int i = 0; i < pieceCount; i++)
            {
                float currentSize = generalCoefficient * pieceSizeCoefficients[i];

                Rect current = new Rect(horizontalPointer, rect.y, currentSize, rect.height);
                result[i] = current;

                horizontalPointer += currentSize;
                horizontalPointer += overrideSpacing;
            }

            return result;
        }
    }

}
