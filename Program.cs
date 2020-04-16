using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea1Grafica
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Poligono poligono = new Poligono(800,600,"Aprendiendo OpenTK"))
            {
                poligono.Run(60.0);
            }
        }
    }
}
