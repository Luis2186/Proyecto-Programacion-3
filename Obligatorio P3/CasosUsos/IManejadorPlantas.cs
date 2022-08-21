using System;
using System.Collections.Generic;
using System.Text;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;
using Repositorios;

namespace CasosUsos
{
    public interface IManejadorPlantas 
    {
        #region Plantas
        public bool AgregarPlanta(Planta unaP, FichaDeCuidado unaF, int idTipoDePlanta, int idTipoDeIluminacion);
        public bool EliminarPlanta(int id);
        public bool ActualizarPlanta(Planta unaP);
        public Planta BuscarPlantaPorId(int id);
        public IEnumerable<Planta> MostrarTodasLasPlantas();
        public IEnumerable<Planta> BuscarPlantasPorTextoEnNombre(string nombre);
        public IEnumerable<Planta> BuscarPlantasPorTipo(int id);
        public IEnumerable<Planta> BuscarPlantasPorAmbiente(string tipoDeAmbiente);
        public IEnumerable<Planta> BuscarPlantasMasBajas(int alturaLimite);
        public IEnumerable<Planta> BuscarPlantasPorAlturaMayorIgual(int alturaLimite);
        #endregion

        #region Tipo de Plantas y Ficha de Cuidados
        public bool AgregarTipoDePlanta(TipoDePlanta unTDP);
        public bool EliminarTipoDePlanta(int id);
        public bool ActualizarTipoDePlanta(TipoDePlanta unTDP);
        public IEnumerable<TipoDePlanta> EncontrarTodosLosTipoDePlanta();
        public TipoDePlanta BuscarPorIdTipoDePlanta(int id);
        public TipoDePlanta BuscarPorNombreTipoDePlanta(string nombre);
        public bool EliminarPorNombreTipoDePlanta(string nombre);      
        public FichaDeCuidado BuscarFichaDeCuidado(int id);
        public IEnumerable<TipoDeIluminacion> EncontrarTodosLosTiposDeIluminacion();
        public bool IngresarFichaDeCuidado(FichaDeCuidado ficha, int idTipoDeIluminacion);
        #endregion
    }
}
