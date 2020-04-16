using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Tarea1Grafica
{
    class IndicesBuffer
    {
        private int renderID;
        private int cantidad;

        public IndicesBuffer(uint[] indices, int cantidad)
        {
            this.cantidad = cantidad;

            renderID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, renderID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, cantidad * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        }

        /*~IndicesBuffer()
        {
            GL.DeleteBuffer(renderID);
        }*/

        public void eliminarBuffer()
        {
            GL.DeleteBuffer(renderID);
        }

        public void enlazar()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, renderID);
        }
        public void desenlazar()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public int getCantidad()
        {
            return cantidad;
        }
    }
}
