using Devcorner.NIdenticon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PF.Common.Identicon
{
    public static class NIdenticon
    {
        private static IdenticonGenerator Generator;

        public static void Init()
        {
            Generator = new IdenticonGenerator()
                .WithSize(128, 128)
                .WithBlocks(5, 5)
                .WithBlockGenerators(IdenticonGenerator.ExtendedBlockGeneratorsConfig);
        }

        public static Bitmap Generate(long seed)
        {
            return Generator.Create(seed.ToString());
        }
    }
}
