using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Google.Protobuf;

namespace Basic_Pathfinder.PreFab
{
    public static class PreFabUser
    {
        public static void Load(PreFab prefab)
        {
            WorldGeneration.WorldGenerationType = WorldGenType.PreFab;
            WorldGeneration.WorldPreFab = prefab;
            if (!WorldGeneration.RepeatPreFab)
                WorldGeneration.WorldGenerationType = WorldGenType.Random;
        }
        public static PreFab Read(string file)
        {
            PreFab pf;
            using (var input = File.OpenRead(file))
                pf = PreFab.Parser.ParseFrom(input);
            return pf;
        }
        public static PreFab[] ReadAll(string[] files)
        {
            List<PreFab> prefabs = new List<PreFab>();
            foreach (var file in files)
                prefabs.Add(Read(file));
            return prefabs.ToArray();
        }
        public static void Write(string fileName)
        {
            PreFab pf = new PreFab {
                GoalPoint = WorldGeneration.Goal,
                StartingPoint = WorldGeneration.Start,
                NodeSize = WorldGeneration.NodeSize,
                Prefab = { WorldGeneration.ObstaclesToObs() }
            };
            using (var output = File.Create(fileName))
                pf.WriteTo(output);
        }

    }
}
