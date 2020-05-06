using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK;

namespace Tarea1Grafica
{
    class Terreno
    {
        private float[] vertices = { 
            -3.0f, 0.0f,  0.0f, 0.0f, 0.0f, //0
             0.0f, 0.0f,  0.0f, 0.0f, 1.0f, //1
             3,0f, 0.0f,  0.0f, 0.0f, 0.0f, //2
             6.0f, 0.0f,  0.0f, 0.0f, 1.0f, //3

            -3.0f, 0.0f, -6.0f, 1.0f, 0.0f, //4
             0.0f, 0.0f, -6.0f, 1.0f, 1.0f, //5
             3.0f, 0.0f, -6.0f, 1.0f, 0.0f, //6
             6.0f, 0.0f, -6.0f, 1.0f, 1.0f, //7

            -3.0f, 0.0f,  6.0f, 1.0f, 0.0f, //8
             0.0f, 0.0f,  6.0f, 1.0f, 1.0f, //9
             3.0f, 0.0f,  6.0f, 1.0f, 0.0f, //10
             6.0f, 0.0f,  6.0f, 1.0f, 1.0f, //11
        };
        private uint[] indices = { 
            0, 1, 4,
            4, 5, 1,
            1, 2, 5,
            5, 6, 2,
            2, 3, 6,
            6, 7, 3,
            0, 1, 8,
            8, 9, 1,
            1, 2, 9,
            9, 10, 2,
            2, 3, 10,
            10, 11, 3
        };

        public Shader shader;
        public Textura textura;
        public VerticesBuffer bufferDeVertices;
        public IndicesBuffer bufferDeIndices;
        public VerticesArreglo arregloDeVertices;

        public Matrix4 modelo = Matrix4.Identity;
        public Matrix4 vista = Matrix4.Identity;
        public Matrix4 proyeccion = Matrix4.Identity;

        public Terreno()
        {
            bufferDeVertices = new VerticesBuffer(vertices, vertices.Length * sizeof(float));

            bufferDeIndices = new IndicesBuffer(indices, indices.Length);

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
        }

        public void dibujar( Render render)
        {
            shader.SetMatrix4("model", modelo);
            render.draw(arregloDeVertices, bufferDeIndices, shader);
        }

    }
}
