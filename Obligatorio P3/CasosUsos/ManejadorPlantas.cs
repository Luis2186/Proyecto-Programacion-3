using System;
using System.Collections.Generic;
using System.Text;
using Repositorios;
using Dominio.EntidadesNegocio;
using Dominio.InterfacesRepositorio;

namespace CasosUsos
{
    public class ManejadorPlantas : IManejadorPlantas
    {
        public IRepositorioPlanta _repoPlanta { get; set; }
        public IRepositorioTipoDePlanta _repoTipoDePlanta { get; set; }
        public IRepositorio<TipoDeIluminacion> _repoTipoDeIluminacion { get; set; }
        public IRepositorio<FichaDeCuidado> _repoFichaDeCuidado { get; set; }


        public ManejadorPlantas(IRepositorioPlanta repoP, IRepositorioTipoDePlanta repoTDP, IRepositorio<TipoDeIluminacion> repoTdi, IRepositorio<FichaDeCuidado> repoFdc) 
        {
            _repoPlanta = repoP;
            _repoTipoDePlanta = repoTDP;
            _repoTipoDeIluminacion = repoTdi;
            _repoFichaDeCuidado = repoFdc;
        }

        #region Plantas
        public bool AgregarPlanta(Planta unaP,FichaDeCuidado unaF, int idTipoDePlanta, int idTipoDeIluminacion)
        {
            bool agregarPlanta = false;

            if (unaP.SoyValido() && _repoPlanta.BuscarNombreCientifico(unaP.NombreCientifico) == null)
            {
                TipoDePlanta tdp = _repoTipoDePlanta.BuscarPorId(idTipoDePlanta);

                if (tdp != null && tdp.SoyValido())
                {                 
                    TipoDeIluminacion tdi = _repoTipoDeIluminacion.BuscarPorId(idTipoDeIluminacion);
                    
                    if (tdi != null && tdi.SoyValido() && unaF.SoyValido())
                    {
                        unaP.Tipo = tdp;
                        unaF.TipoDeIluminacion = tdi; 
                        unaP.FichaDeCuidados = unaF;                     
                        agregarPlanta = _repoPlanta.Agregar(unaP);                     
                    }
                }
            }
            return agregarPlanta;
        }
        public bool EliminarPlanta(int id) 
        {
            return _repoPlanta.Eliminar(id);
        }
        public bool ActualizarPlanta(Planta unaP) 
        {
            bool actualizada = false;
            if (unaP.SoyValido())
            {
                actualizada = _repoPlanta.Actualizar(unaP); ;
            }
            return actualizada;
        }
        public Planta BuscarPlantaPorId(int id)
        {
            return _repoPlanta.BuscarPorId(id);
        }
        public IEnumerable<Planta> MostrarTodasLasPlantas() {
            return _repoPlanta.EncontrarTodas();
        }
        public IEnumerable<Planta> BuscarPlantasPorTextoEnNombre(string nombre) {
            return _repoPlanta.BuscarPorTextoEnNombre(nombre);
        }
        public IEnumerable<Planta> BuscarPlantasPorTipo(int id) 
        {
            return _repoPlanta.BuscarPorTipo(id);
        }
        public IEnumerable<Planta> BuscarPlantasPorAmbiente(string tipoDeAmbiente) 
        {
            return _repoPlanta.BuscarPorAmbiente(tipoDeAmbiente);
        }
        public  IEnumerable<Planta> BuscarPlantasMasBajas(int alturaLimite)
        {
            return _repoPlanta.PlantasMasBajas(alturaLimite);
        }
        public IEnumerable<Planta> BuscarPlantasPorAlturaMayorIgual(int alturaLimite) 
        {
            return _repoPlanta.PlantasConMismaAlturaOMas(alturaLimite);
        }
        private bool IENumerableLLeno(IEnumerable<Planta> ieNumerable)
        {
            bool lleno = false;

            foreach (Planta item in ieNumerable)
            {
                lleno = true;
                break;
            }
            return lleno;
        }
        #endregion

        #region TipoDePlantas
        public bool AgregarTipoDePlanta(TipoDePlanta unTDP)
        {
            bool agregado = false;

            if (unTDP.SoyValido() && _repoTipoDePlanta.BuscarPorNombre(unTDP.Nombre) == null ) 
            {
                agregado = _repoTipoDePlanta.Agregar(unTDP);
            }
            return agregado;
        }
        public bool EliminarTipoDePlanta(int id)
        {
            bool eliminado = false;
            IEnumerable<Planta> unaP = _repoPlanta.BuscarPorTipo(id);
            if (!IENumerableLLeno(unaP))
            {
                _repoTipoDePlanta.Eliminar(id);
                eliminado = true;
            }
            return eliminado;
        }
        public bool ActualizarTipoDePlanta(TipoDePlanta unTDP)
        {
            if (unTDP.SoyValido()) { 
                 return _repoTipoDePlanta.Actualizar(unTDP);
            }
            return false;
        }
        public IEnumerable<TipoDePlanta> EncontrarTodosLosTipoDePlanta()
        {
            return _repoTipoDePlanta.EncontrarTodas();
        }
        public TipoDePlanta BuscarPorIdTipoDePlanta(int id)
        {
            return _repoTipoDePlanta.BuscarPorId(id);
        }
        public TipoDePlanta BuscarPorNombreTipoDePlanta(string nombre)
        {
            return _repoTipoDePlanta.BuscarPorNombre(nombre);
        }
        public bool EliminarPorNombreTipoDePlanta(string nombre)
        {
            return _repoTipoDePlanta.EliminarPorNombre(nombre);
        }
        public IEnumerable<TipoDeIluminacion> EncontrarTodosLosTiposDeIluminacion()
        {
            return _repoTipoDeIluminacion.EncontrarTodas();
        }
        #endregion

        #region FichaDeCuidados
        public FichaDeCuidado BuscarFichaDeCuidado(int id)
        {
            return _repoFichaDeCuidado.BuscarPorId(id);
        }
        public bool IngresarFichaDeCuidado(FichaDeCuidado ficha,int idTipoDeIluminacion)
        {
            bool agregado= false;
            TipoDeIluminacion tdi = _repoTipoDeIluminacion.BuscarPorId(idTipoDeIluminacion);

            if (tdi != null) 
            {
                ficha.TipoDeIluminacion = tdi;
                _repoFichaDeCuidado.Agregar(ficha);
                agregado = true;
            }
            return agregado;
        }
        #endregion
        
       

    }
}
