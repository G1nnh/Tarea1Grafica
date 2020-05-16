using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace Tarea1Grafica
{
    class Terreno : Forma
    {            
        public Terreno()
        {
            cantidadDeVertices = GetVertices().Length / 5;
            cantidadDeIndices = GetIndices().Length;

            bufferDeVertices = new VerticesBuffer(GetVertices(), GetVertices().Length * sizeof(float));
            bufferDeIndices = new IndicesBuffer(GetIndices(), GetIndices().Length);

            shader = new Shader("d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.vert", "d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.frag");
            shader.use();

            textura = new Textura("d:/ginno/Documents/Visual Studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/Recursos/cesped.jpg");
            textura.Use();

            //Creamos el VertexArrayObject (VAO)
            arregloDeVertices = new VerticesArreglo();
            //GL.BindVertexArray(arregloDeVertices);
            arregloDeVertices.enlazar();

            //Enlazamos de nuevo los buffers
            //GL.BindBuffer(BufferTarget.ArrayBuffer, bufferDeVertices.);
            bufferDeVertices.enlazar();
            //
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferDeElementosIndices);
            bufferDeIndices.enlazar();

            arregloDeVertices.añadirBuffer(bufferDeVertices, shader);

            //Al inicializarse, se calcula aqui la matriz modelo
            CalcularMatrizModelo();
        }

        public override float[] GetVertices()
        {
            float[] vertices =
            {
                -3.0f, 0.0f, -6.0f, 0.0f, 1.0f, //0
                 0.0f, 0.0f, -6.0f, 1.0f, 1.0f, //1
                 3.0f, 0.0f, -6.0f, 0.0f, 1.0f, //2
                 6.0f, 0.0f, -6.0f, 1.0f, 1.0f, //3 
                                  
                -3.0f, 0.0f,  0.0f, 0.0f, 0.0f, //4
                 0.0f, 0.0f,  0.0f, 1.0f, 0.0f, //5
                 3.0f, 0.0f,  0.0f, 0.0f, 0.0f, //6
                 6.0f, 0.0f,  0.0f, 1.0f, 0.0f, //7 

                -3.0f, 0.0f,  6.0f, 0.0f, 1.0f, //8
                 0.0f, 0.0f,  6.0f, 1.0f, 1.0f, //9
                 3.0f, 0.0f,  6.0f, 0.0f, 1.0f, //10
                 6.0f, 0.0f,  6.0f, 1.0f, 1.0f  //11                                                                                                           
            };
            return vertices;
        }

        public override uint[] GetIndices()
        {
            uint[] indices = { 
                0, 1, 4,
                4, 5, 1,
                1, 2, 5,
                5, 6, 2, 
                2, 3, 6, 
                3, 6, 7,
                4, 5, 8, 
                8, 9, 5,
                5, 6, 9,
                9, 10, 6,
                6, 7, 10,
                10, 11, 7
            };
            return indices;
        }

        public override void CalcularMatrizModelo()
        {
            modelo *= Matrix4.CreateTranslation(new Vector3(0f, -0.5f, 0f)); ;
        }

        public override void SetShader()
        {
            shader = new Shader("d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.vert", 
                "d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.frag");
        }
    }
}
