using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea1Grafica
{
    class Piramide : Forma
    {
        public Piramide()
        {
            cantidadDeVertices = 5;
            cantidadDeIndices = 18;

            bufferDeVertices = new VerticesBuffer(GetVertices(), GetVertices().Length * sizeof(float));
            bufferDeIndices = new IndicesBuffer(GetIndices(), GetIndices().Length);

            shader = new Shader("d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.vert", "d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.frag");
            shader.use();

            textura = new Textura("d:/ginno/Documents/Visual Studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/Recursos/cesped.jpg");
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
            modelo = Matrix4.CreateScale(escalacion) * Matrix4.CreateRotationX(rotacion.X) * 
                Matrix4.CreateRotationY(rotacion.Y) * Matrix4.CreateRotationZ(rotacion.Z) * 
                Matrix4.CreateTranslation(new Vector3(2f, -0.5f, 0f));
        }

        public override uint[] GetIndices()
        {
            uint[] indices =
            {
                0, 1, 2,
                0, 2, 3,
                0, 1, 4,
                1, 2, 4, 
                2, 3, 4, 
                3, 0, 4

            };
            return indices;
        }

        public override float[] GetVertices()
        {
            float[] vertices = {
                -0.5f,  0.0f,  0.5f, 0.0f, 1.0f,
                -0.5f,  0.0f, -0.5f, 0.0f, 0.0f,
                 0.5f,  0.0f, -0.5f, 1.0f, 0.0f,
                 0.5f,  0.0f,  0.5f, 1.0f, 1.0f,

                 0.0f,  1.0f,  0.0f, 0.5f, 0.5f
            };
            return vertices;
        }

        public override void SetShader()
        {
            shader = new Shader("d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.vert", "d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.frag");
        }
    }
}