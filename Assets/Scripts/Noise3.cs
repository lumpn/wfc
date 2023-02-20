//----------------------------------------
// MIT License
// Copyright(c) 2023 Jonas Boetel
//----------------------------------------
using UnityEngine;

namespace Lumpn.WFC
{
    public sealed class Noise3 : IRandom
    {
        private const uint SQ5_BIT_NOISE1 = 0xd2a80a3f; // 11010010101010000000101000111111
        private const uint SQ5_BIT_NOISE2 = 0xa884f197; // 10101000100001001111000110010111
        private const uint SQ5_BIT_NOISE3 = 0x6C736F4B; // 01101100011100110110111101001011
        private const uint SQ5_BIT_NOISE4 = 0xB79F3ABB; // 10110111100111110011101010111011
        private const uint SQ5_BIT_NOISE5 = 0x1b56c4f5; // 00011011010101101100010011110101

        private const int PRIME1 = 198491317; // Large prime number with non-boring bits
        private const int PRIME2 = 6542989; // Large prime number with distinct and non-boring bits
        private const int PRIME3 = 357239; // Large prime number with distinct and non-boring bits

        private readonly uint seed;

        public Noise3(int seed)
        {
            unchecked
            {
                this.seed = (uint)seed;
            }
        }

        public int Range(Vector3Int position, int minInclusive, int maxExclusive)
        {
            var noise = Get3dNoiseUint(position.x, position.y, position.z, seed);
            var range = maxExclusive - minInclusive;

            unchecked
            {
                ulong longNoise = (ulong)noise;
                longNoise *= (uint)range;
                longNoise /= uint.MaxValue;

                return minInclusive + (int)longNoise;
            }
        }

        private static uint SquirrelNoise5(int value, uint seed)
        {
            unchecked
            {
                uint mangledBits = (uint)value;
                mangledBits *= SQ5_BIT_NOISE1;
                mangledBits += seed;
                mangledBits ^= (mangledBits >> 9);
                mangledBits += SQ5_BIT_NOISE2;
                mangledBits ^= (mangledBits >> 11);
                mangledBits *= SQ5_BIT_NOISE3;
                mangledBits ^= (mangledBits >> 13);
                mangledBits += SQ5_BIT_NOISE4;
                mangledBits ^= (mangledBits >> 15);
                mangledBits *= SQ5_BIT_NOISE5;
                mangledBits ^= (mangledBits >> 17);
                return mangledBits;
            }
        }

        private static uint Get1dNoiseUint(int positionX, uint seed)
        {
            return SquirrelNoise5(positionX, seed);
        }

        private static uint Get2dNoiseUint(int indexX, int indexY, uint seed)
        {
            return SquirrelNoise5(indexX + (PRIME1 * indexY), seed);
        }

        private static uint Get3dNoiseUint(int indexX, int indexY, int indexZ, uint seed)
        {
            return SquirrelNoise5(indexX + (PRIME1 * indexY) + (PRIME2 * indexZ), seed);
        }

        private static uint Get4dNoiseUint(int indexX, int indexY, int indexZ, int indexT, uint seed)
        {
            return SquirrelNoise5(indexX + (PRIME1 * indexY) + (PRIME2 * indexZ) + (PRIME3 * indexT), seed);
        }
    }
}
