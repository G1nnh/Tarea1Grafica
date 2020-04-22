using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Tarea1Grafica
{
    class Render
    {
        public Render()
        {

        }

        public void clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void draw(VerticesArreglo verticesArreglo, IndicesBuffer indicesBuffer, Shader shader)
        {
            shader.use();
            verticesArreglo.enlazar();
            indicesBuffer.enlazar();
            GL.DrawElements(PrimitiveType.Triangles, indicesBuffer.getCantidad(), DrawElementsType.UnsignedInt, 0);
        }
    }
}
