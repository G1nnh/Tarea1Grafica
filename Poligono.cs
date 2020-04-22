using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Tarea1Grafica
{
    class Poligono : GameWindow
    {
        //int bufferDeVertices;
        //int bufferDeElementosIndices;
        //int arregloDeVertices;

        VerticesBuffer bufferDeVertices;
        IndicesBuffer bufferDeIndices;

        VerticesArreglo arregloDeVertices;

        static float[] vertices =
        {
            /*-0.5f, -0.5f, 0.0f,
             0.5f, -0.5f, 0.0f,     //Triángulo 
             0.0f,  0.5f, 0.0f*/

             /*0.5f,  0.5f, 0.0f, 1.0f, 1.0f, 
             0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 
            -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 
            -0.5f,  0.5f, 0.0f, 0.0f, 1.0f*/    

            -0.500000f, -0.500000f,  0.500000f, 0.0f, 0.0f, //0
            -0.500000f,  0.500000f,  0.500000f, 0.0f, 1.0f, //1
            -0.500000f, -0.500000f, -0.500000f, 1.0f, 0.0f, //2
            -0.500000f,  0.500000f, -0.500000f, 1.0f, 1.0f, //3

             0.500000f, -0.500000f,  0.500000f, 1.0f, 1.0f, //4
             0.500000f,  0.500000f,  0.500000f, 1.0f, 0.0f, //5
             0.500000f, -0.500000f, -0.500000f, 0.0f, 1.0f, //6
             0.500000f,  0.500000f, -0.500000f, 0.0f, 0.0f  //7
        };

        static uint[] indices =
        {
            /*0, 1, 3,
            1, 2, 3*/

            1, 2, 0,
            3, 6, 2,
            7, 4, 6,
            5, 0, 4,
            6, 0, 2,
            3, 5, 7,
            1, 3, 2,
            3, 7, 6,
            7, 5, 4,
            5, 1, 0,
            6, 4, 0,
            3, 1, 5
                
        };
        Textura textura;

        Shader shader;

        /*private Matrix4 vista;
        private Matrix4 proyeccion;*/
        //Reemplazamos por la calse Camara
        private Camara camara;

        private bool primerMovimiento = true;
        private Vector2 ultimaPosicion;

        private double tiempo;

        Render render;
        
        public Poligono (int ancho, int alto, string titulo) : base (ancho, alto, GraphicsMode.Default, titulo)
        {
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (!Focused)
            {
                return;
            }

            KeyboardState entrada = Keyboard.GetState();

            if (entrada.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            const float velCamara = 1.5f;
            const float sensibilidad = 0.2f;

            if (entrada.IsKeyDown(Key.W))
            {
                camara.Posicion += camara.Frente * velCamara * (float)e.Time; 
            }

            if (entrada.IsKeyDown(Key.S))
            {
                camara.Posicion -= camara.Frente * velCamara * (float)e.Time;
            }

            if (entrada.IsKeyDown(Key.A))
            {
                camara.Posicion -= camara.Derecha * velCamara * (float)e.Time;
            }

            if (entrada.IsKeyDown(Key.D))
            {
                camara.Posicion += camara.Derecha * velCamara * (float)e.Time;
            }

            if (entrada.IsKeyDown(Key.Space)){
                camara.Posicion += camara.Arriba * velCamara * (float)e.Time;
            }

            if (entrada.IsKeyDown(Key.LShift))
            {
                camara.Posicion -= camara.Arriba * velCamara * (float)e.Time;
            }

            var raton = Mouse.GetState();

            if (primerMovimiento)
            {
                ultimaPosicion = new Vector2(raton.X, raton.Y);
                primerMovimiento = false;
            }
            else
            {
                var deltaX = raton.X - ultimaPosicion.X;
                var deltaY = raton.Y - ultimaPosicion.Y;
                ultimaPosicion = new Vector2(raton.X, raton.Y);

                camara.Guiñada += deltaX * sensibilidad;
                camara.Cabeceo -= deltaY * sensibilidad;
            }

            base.OnUpdateFrame(e);
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (Focused)
            {
                Mouse.SetPosition(X + Width / 2f, Y + Height / 2f);
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            camara.CampoDeVision -= e.DeltaPrecise;
            base.OnMouseWheel(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            render = new Render();

            GL.ClearColor(0.82f, 1.0f, 1.0f, 1.0f);

            GL.Enable(EnableCap.DepthTest);

            //Creamos el VertexBufferObject (VBO)
            /*bufferDeVertices = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, bufferDeVertices);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);*/
            bufferDeVertices = new VerticesBuffer(vertices, vertices.Length * sizeof(float));

            //Creamos el ElementBufferObject (EBO)
            /*bufferDeElementosIndices = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, bufferDeElementosIndices);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);*/
            bufferDeIndices = new IndicesBuffer(indices, indices.Length);

            shader = new Shader("d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.vert", "d:/ginno/documents/visual studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/shader/shader.frag");
            shader.use();

            textura = new Textura("d:/ginno/Documents/Visual Studio 2017/Projects/Tarea1Grafica/Tarea1Grafica/Recursos/wall.png");
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

            //Esta parte ya no es necesaria, la reemplazamos al hacer añadir el buffer con el array
            /*var locacionVertice = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(locacionVertice);
            GL.VertexAttribPointer(locacionVertice, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            int locacionTexCoord = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(locacionTexCoord);
            GL.VertexAttribPointer(locacionTexCoord, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            */
            arregloDeVertices.añadirBuffer(bufferDeVertices, shader);

            /*vista = Matrix4.CreateTranslation(0.0f, 0.0f, -1.5f);
            proyeccion = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), Width / (float)Height, 0.1f, 100.0f);
            */
            camara = new Camara(Vector3.UnitZ * 3, Width / (float)Height);

            CursorVisible = false;

            base.OnLoad(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            tiempo += 4.0 * e.Time;

            //GL.Clear(ClearBufferMask.ColorBufferBit|ClearBufferMask.DepthBufferBit);
            render.clear();
            
            textura.Use();
            //shader.use();

            //GL.BindVertexArray(arregloDeVertices);
            //arregloDeVertices.enlazar();

            /*var transformacion = Matrix4.Identity;
            transformacion *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(20f));
            transformacion *= Matrix4.CreateTranslation(0.25f, 0.25f, 0.0f);
            shader.SetMatrix4("transform", transformacion);*/

            //Establecemos las matrices modelo, vista, proyeccion
            var modelo = Matrix4.Identity; //* Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(tiempo));
            //modelo *= Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(tiempo));
            shader.SetMatrix4("model", modelo);
            shader.SetMatrix4("view", camara.getMatrizVista());
            shader.SetMatrix4("projection", camara.getMatrizProyeccion());

            //GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            render.draw(arregloDeVertices, bufferDeIndices, shader);
            
            Context.SwapBuffers();

            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            camara.RelacionDeAspecto = Width / (float)Height;

            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            //Desenlazamos los buffers
            //GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            bufferDeVertices.desenlazar();
            bufferDeIndices.desenlazar();
            //Tambien el arreglo
            //GL.BindVertexArray(0);
            arregloDeVertices.desenlazar();                                    
            
            GL.UseProgram(0);

            //GL.DeleteBuffer(bufferDeVertices);
            //GL.DeleteBuffer(bufferDeElementosIndices);
            //GL.DeleteVertexArray(arregloDeVertices);

            bufferDeVertices.eliminarBuffer();
            bufferDeIndices.eliminarBuffer();
            arregloDeVertices.eliminarArreglo();       
            
            shader.disponer();
            GL.DeleteTexture(textura.manejo);

            base.OnUnload(e);
        }
    }
}
