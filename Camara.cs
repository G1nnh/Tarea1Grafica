using System;
using OpenTK;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea1Grafica
{
    class Camara
    {
        private Vector3 frente = -Vector3.UnitZ;

        private Vector3 arriba = Vector3.UnitY;

        private Vector3 derecha = Vector3.UnitX;

        //Rotación en los ejes X e Y en RAD
        private float cabeceo;
        private float guiñada = -MathHelper.PiOver2;

        private float campoDeVision = MathHelper.PiOver2;

        public Camara(Vector3 posicion, float relacionDeAspecto)
        {
            Posicion = posicion;
            RelacionDeAspecto = relacionDeAspecto;
        }

        public Vector3 Posicion { get; set; }

        public float RelacionDeAspecto { private get; set; }

        public Vector3 Frente => frente;
        public Vector3 Arriba => arriba;
        public Vector3 Derecha => derecha;

        public float Cabeceo
        {
            get => MathHelper.RadiansToDegrees(cabeceo);
            set
            {
                var angulo = MathHelper.Clamp(value, -89f, 89f);
                cabeceo = MathHelper.DegreesToRadians(angulo);
                actualizarVectores();
            }
        }

        public float Guiñada
        {
            get => MathHelper.RadiansToDegrees(guiñada);
            set
            {
                guiñada = MathHelper.DegreesToRadians(value);
                actualizarVectores();
            }
        }

        public float CampoDeVision
        {
            get => MathHelper.RadiansToDegrees(campoDeVision);
            set
            {
                var angulo = MathHelper.Clamp(value, 1f, 45f);
                campoDeVision = MathHelper.DegreesToRadians(angulo);
            }
        }

        public Matrix4 getMatrizVista()
        {
            return Matrix4.LookAt(Posicion, Posicion + frente, arriba);
        }

        public Matrix4 getMatrizProyeccion()
        {
            return Matrix4.CreatePerspectiveFieldOfView(campoDeVision, RelacionDeAspecto, 0.01f, 100f);
        }

        private void actualizarVectores()
        {
            frente.X = (float)Math.Cos(cabeceo) * (float)Math.Cos(guiñada);
            frente.Y = (float)Math.Sin(cabeceo);
            frente.Z = (float)Math.Cos(cabeceo) * (float)Math.Sin(guiñada);

            frente = Vector3.Normalize(frente);

            derecha = Vector3.Normalize(Vector3.Cross(frente, Vector3.UnitY));
            arriba = Vector3.Normalize(Vector3.Cross(derecha, frente));
        }
    }
}
