using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace Tarea1Grafica
{
    class Shader
    {
        int manejo;

        Dictionary<string, int> locacionesUnifomres;

        public Shader (string verticePath, string fragmentoPath)
        {
            int ShaderVertice, ShaderFragmento;

            string fuenteVerticeShader;
            using (StreamReader lector = new StreamReader(verticePath, Encoding.UTF8))
            {
                fuenteVerticeShader = lector.ReadToEnd();
            }

            string fuenteFragmentoShader;
            using (StreamReader lector = new StreamReader(fragmentoPath, Encoding.UTF8))
            {
                fuenteFragmentoShader = lector.ReadToEnd();
            }

            ShaderVertice = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(ShaderVertice, fuenteVerticeShader);

            ShaderFragmento = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(ShaderFragmento, fuenteFragmentoShader);

            GL.CompileShader(ShaderVertice);
            string infoLogVert = GL.GetShaderInfoLog(ShaderVertice);
            if (infoLogVert != System.String.Empty)
                System.Console.WriteLine(infoLogVert);

            GL.CompileShader(ShaderFragmento);
            string infoLogFrag = GL.GetShaderInfoLog(ShaderFragmento);
            if (infoLogFrag != System.String.Empty)
                System.Console.WriteLine(infoLogFrag);

            manejo = GL.CreateProgram();

            GL.AttachShader(manejo, ShaderVertice);
            GL.AttachShader(manejo, ShaderFragmento);

            GL.LinkProgram(manejo);

            //Antes de dejar el constructor, debemos hacer una pequeña limpieza.
            GL.DetachShader(manejo, ShaderVertice);
            GL.DetachShader(manejo, ShaderFragmento);
            GL.DeleteShader(ShaderFragmento);
            GL.DeleteShader(ShaderVertice);

            GL.GetProgram(manejo, GetProgramParameterName.ActiveUniforms, out var numeroDeUniformes);
            locacionesUnifomres = new Dictionary<string, int>();
            for (var i = 0; i < numeroDeUniformes; i++)
            {
                var llave = GL.GetActiveUniform(manejo, i, out _, out _);
                var locacion = GL.GetUniformLocation(manejo, llave);
                locacionesUnifomres.Add(llave, locacion);
            }
        }

        public void use()
        {
            GL.UseProgram(manejo);
        }

        private bool valorDispuesto = false;

        protected virtual void disponer (bool disponiendo)
        {
            if (!valorDispuesto)
            {
                GL.DeleteProgram(manejo);
                valorDispuesto = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(manejo);
        }

        public void disponer()
        {
            disponer(true);
            GC.SuppressFinalize(this);
        }

        public int GetAttribLocation (string nombreAtrib)
        {
            return GL.GetAttribLocation(manejo, nombreAtrib);
        }

        public void SetInt(string nombre, int dato)
        {
            GL.UseProgram(manejo);
            GL.Uniform1(locacionesUnifomres[nombre],dato);
        }

        public void SetFloat(string nombre, float dato)
        {
            GL.UseProgram(manejo);
            GL.Uniform1(locacionesUnifomres[nombre],dato);
        }

        public void SetMatrix4(string nombre, Matrix4 dato)
        {
            GL.UseProgram(manejo);
            GL.UniformMatrix4(locacionesUnifomres[nombre], true, ref dato);
        }

        public void SetVector3(string nombre, Vector3 dato)
        {
            GL.UseProgram(manejo);
            GL.Uniform3(locacionesUnifomres[nombre], dato);
        }

    }
}
