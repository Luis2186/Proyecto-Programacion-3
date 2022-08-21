using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorio<T>
    {
        bool Agregar(T obj);
        bool Eliminar(int id);
        bool Actualizar(T obj);
        IEnumerable<T> EncontrarTodas();
        T BuscarPorId(int id);
    }
}
