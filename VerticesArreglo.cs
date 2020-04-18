using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Tarea1Grafica
{
    class VerticesArreglo
    {
        private int renderID;

        public VerticesArreglo()
        {
            renderID = GL.GenVertexArray();
        }

        /*~VerticesArreglo()
        {
            GL.DeleteVertexArray(renderID);
        }*/

        public void eliminarArreglo()
        {
            GL.DeleteVertexArray(renderID);
        }

        public void añadirBuffer(VerticesBuffer verticeBuffer, Shader shader)
        {
            enlazar();
            verticeBuffer.enlazar();

            var locacionVertice = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(locacionVertice);
            GL.VertexAttribPointer(locacionVertice, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            int locacionTexCoord = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(locacionTexCoord);
            GL.VertexAttribPointer(locacionTexCoord, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

        }

        public void enlazar()
        {
            GL.BindVertexArray(renderID);
        }

        public void desenlazar()
        {
            GL.BindVertexArray(0);
        }
    }
}
