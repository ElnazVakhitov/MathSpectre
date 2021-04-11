using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Компьютерная_математика
{
    class Algoritm
    {
        public void Solve(String f)
        {
            var cube = new n_Cube(f);
            bool[][][] xorNodes = new bool[cube.countLight][][];
            for (var i =0; i<cube.n; i++)
            {
                xorNodes[i] = cube.startNodes
                    .Select(x => x.vector)
                    .Select(x => Xor(cube.startNodes[i].vector, x))
                    .ToArray();
            }
            int[][] spectres = new int[xorNodes.Length][];
            for (var i =0; i<xorNodes.Length;i++)
            {
                step3(xorNodes[i], cube);
                spectres[i]= step4(cube);
            }
            Console.WriteLine(String.Join(",", step5(spectres, cube.n_cube.Length - 1)));
        }

        bool[] Xor(bool[] xorVector, bool[] vector)
        {
            return xorVector.Zip(vector, (x, y) => x ^ y).ToArray();
        }

        void step3(bool[][] vectors, n_Cube cube)
        {
            cube.repoint(vectors);
        }

        int[] step4(n_Cube cube)
        {
            int[] spectre = new int[cube.n_cube.Length-1];
            for (var i = 1; i < cube.n_cube.Length; i++) {
                for (var j = 0; j < cube.n_cube[i].Length; j++)
                {
                    var node = cube.n_cube[i][j];
                    var pmin = node.linked.Min(n => n.m);
                    node.p = pmin;
                    if (node.p < i - i)
                        node.m = node.p;
                    else if (node.l)
                        node.m = i - 1;
                    else
                        node.m = i;
                    if (node.m < pmin)
                        pmin = node.m;
                    if (spectre[i] < node.m)
                        spectre[i] = node.m;
                }
            }
            return spectre;
        }

        int[] step5(int[][] spectres, int n)
        {
            return Enumerable.Range(0, n).Select(x => spectres.Max(y => y[x])).ToArray();
        }
    }
}
