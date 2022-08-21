using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;

namespace Dominio.InterfacesRepositorio
{
    public interface IRepositorioPlanta : IRepositorio<Planta>
    {
        IEnumerable<Planta> BuscarPorTextoEnNombre(string nombre);
        IEnumerable<Planta> BuscarPorTipo(int id);
        IEnumerable<Planta> BuscarPorAmbiente(string tipoDeAmbiente);
        IEnumerable<Planta> PlantasMasBajas(int alturaLimite);
        IEnumerable<Planta> PlantasConMismaAlturaOMas(int alturaLimite);
        List<string> BuscarTodosNombresVulgares(int idPlanta);
        public Planta BuscarNombreCientifico(string nombreCientifico);

    }
}
