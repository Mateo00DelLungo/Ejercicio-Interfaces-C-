﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1._6.Dominio
{
    public class Cliente
    {
        public int DNI { get; set; }
        public string Nombre{ get; set; }
        public string Apellido { get; set; }

        public Cliente(int dni, string nombre, string apellido)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
        }
        public Cliente() { }

    }
}
