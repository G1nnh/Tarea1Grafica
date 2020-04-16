using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Tarea1Grafica
{
    class VerticesBuffer
    {
        private int renderID;

        public VerticesBuffer(float[] vertices, int tamaño)
        {
            renderID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, renderID);
            GL.BufferData(BufferTarget.ArrayBuffer, tamaño, vertices, BufferUsageHint.StaticDraw);
        }

        /*~VerticesBuffer()
        {
            GL.DeleteBuffer(renderID);
        }*/

        public void eliminarBuffer()
        {
            GL.DeleteBuffer(renderID);
        }

        public void enlazar()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, renderID);
        }
        public void desenlazar()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
