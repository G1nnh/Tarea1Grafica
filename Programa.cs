using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea1Grafica
{
    class Programa
    {
        static void Main(string[] args)
        {
            using (Juego juego = new Juego(800,600,"Juego con OpenTK"))
            {
                juego.Run(60.0);
            }
        }
    }
}
