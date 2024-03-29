﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
    /// <summary>
    /// Provides methods for generating codes with a given number of positions
    /// and colors.
    /// </summary>
    public class CodeFactory
    {
        /// <summary>
        /// Gets the number of colors in codes generated by this factory.
        /// </summary>
        public int Colors { get; }

        /// <summary>
        /// Gets the number of positions in codes generated by this factory.
        /// </summary>
        public int Positions { get; }

        /// <summary>
        /// Gets the number of distinct codes that this factory can
        /// generate.
        /// </summary>
        public int Possibilities { get; }

        /// <summary>
        /// Initializes a new instance of the CodeFactory class.
        /// </summary>
        /// <param name="positions">
        /// The number of positions.
        /// </param>
        /// <param name="colors">
        /// The number of colors.
        /// </param>
        public CodeFactory(int positions, int colors)
        {
            if (positions < 1)
                throw new ArgumentException("A code must contain at least one position");

            if (colors < 1)
                throw new ArgumentException("A code must contain at least one color");

            if (colors > Game.Colors.List.Length)
                throw new ArgumentException($"A code can contain no more than {Game.Colors.List.Length} colors");

            Positions     = positions;
            Colors        = colors;
            Possibilities = (int)Math.Pow(colors, positions);
        }

        /// <summary>
        /// Creates a specified code.
        /// </summary>
        /// <param name="number">
        /// The number of the code to create from 0 to Possibilities - 1.
        /// </param>
        public Code Create(int number) =>
            EnumerateCodes().Skip(number).First();

        /// <summary>
        /// Creates a random code using the provided random number generator.
        /// </summary>
        /// <param name="random">
        /// The random number generator.
        /// </param>
        public Code Create(Random random) =>
            Create(random.Next(Possibilities));

        /// <summary>
        /// Generates a collection of codes containing every code that this
        /// factory can create exactly once.
        /// </summary>
        public IEnumerable<Code> EnumerateCodes()
        {
            var current = new int[Positions];
            var position = default(int);

            do
            {
                yield return new Code(current);

                position = 0;
                while (position < Positions && ++current[position] == Colors)
                    current[position++] = 0;
            }
            while (position < Positions);
        }
    }
}
