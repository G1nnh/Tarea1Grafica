using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Tarea1Grafica
{
    abstract class Forma
    {
        public Vector3 posicion = Vector3.Zero;
        public Vector3 rotacion = Vector3.Zero;
        public Vector3 escalacion = Vector3.One;

        public int cantidadDeVertices;
        public int cantidadDeIndices;
        public Matrix4 modelo = Matrix4.Identity;
        public Matrix4 vista = Matrix4.Identity;
        public Matrix4 proyeccion = Matrix4.Identity;

        public Shader shader;
        public Textura textura;
        public VerticesBuffer bufferDeVertices;
        public IndicesBuffer bufferDeIndices;
        public VerticesArreglo arregloDeVertices;

        public abstract float[] GetVertices();
        public abstract uint[] GetIndices();
        public abstract void CalcularMatrizModelo();
        public abstract void SetShader();
    }
}   
