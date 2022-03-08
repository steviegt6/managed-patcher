using System;
using System.Collections.Generic;

namespace ManagedPatcher.Utilities
{
    /// <summary>
    ///     Parses a <see cref="string"/> into a collection of paths, using keys and values.
    /// </summary>
    public class PathOverriderCollection
    {
        public readonly Dictionary<string, string> Paths;

        public PathOverriderCollection(string input)
        {
            Paths = new Dictionary<string, string>();

            string[] pathInputs = input.Split(';');

            if (pathInputs.Length == 0)
                throw new ArgumentException(null, nameof(input));

            foreach (string pInput in pathInputs)
            {
                string[] split = pInput.Split('=');

                if (split.Length != 2)
                    throw new InvalidOperationException($"Input \"{pInput}\" could not be split.");
                
                Paths.Add(split[0], split[1]);
            }
        }
    }
}