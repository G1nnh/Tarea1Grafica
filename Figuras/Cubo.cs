using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea1Grafica.Figuras
{
    class Cubo : Forma
    {
        public Cubo()
        {
            cantidadDeVertices = GetVertices().Length / 5;
            cantidadDeIndices = GetIndices().Length;

            bufferDeVertices = new VerticesBuffer(GetVertices(), GetVertices().Length * sizeof(float));
            bufferDeIndices = new IndicesBuffer(GetIndices(), GetIndices().Length);

            SetShader();

            textura = new Textura("d:/ginno/Documents/Visual Studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/Recursos/wall.png");
            textura.Use();

            arregloDeVertices = new VerticesArreglo();
            arregloDeVertices.enlazar();

            bufferDeVertices.enlazar();
            bufferDeIndices.enlazar();

            arregloDeVertices.añadirBuffer(bufferDeVertices, shader);

            CalcularMatrizModelo();
        }
        public override void CalcularMatrizModelo()
        {
            modelo = Matrix4.Identity;
        }

        public override uint[] GetIndices()
        {
            uint[] indices =
            {
                1, 2, 0,
                3, 6, 2,
                7, 4, 6,
                5, 0, 4,
                6, 0, 2,
                3, 5, 7,
                1, 3, 2,
                3, 7, 6,
                7, 5, 4,
                5, 1, 0,
                6, 4, 0,
                3, 1, 5

            };
            return indices;
        }

        public override float[] GetVertices()
        {
            float[] vertices =
            {
                -0.500000f, -0.500000f,  0.500000f, 0.0f, 0.0f, //0
                -0.500000f,  0.500000f,  0.500000f, 0.0f, 1.0f, //1
                -0.500000f, -0.500000f, -0.500000f, 1.0f, 0.0f, //2
                -0.500000f,  0.500000f, -0.500000f, 1.0f, 1.0f, //3

                 0.500000f, -0.500000f,  0.500000f, 1.0f, 1.0f, //4
                 0.500000f,  0.500000f,  0.500000f, 1.0f, 0.0f, //5
                 0.500000f, -0.500000f, -0.500000f, 0.0f, 1.0f, //6
                 0.500000f,  0.500000f, -0.500000f, 0.0f, 0.0f  //7
            };
            return vertices;
        }

        public override void SetShader()
        {
            shader = new Shader("d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.vert", "d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.frag");
            shader.use();
        }
    }
}
