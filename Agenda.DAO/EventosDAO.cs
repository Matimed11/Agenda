﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Agenda.DTO;

namespace Agenda.DAO
{
    public class EventosDAO
    {
        public List<EventoDTO> CargarListaDTOs(DataTable dataTable)
        {
            List<EventoDTO> listaEventos = new List<EventoDTO>();
            try
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    EventoDTO eventoDTO = new EventoDTO();
                    eventoDTO.Id = (int)dataRow["Id"];
                    eventoDTO.IdPersona = (int)dataRow["IdPersona"];
                    eventoDTO.FechaHora = (DateTime)dataRow["FechaHora"];
                    eventoDTO.Descripcion = (string)dataRow["Descripcion"];
                    eventoDTO.PersonaNombreApellido = (string)dataRow["NombreApellido"];
                    eventoDTO.Prioridad = (int)dataRow["Prioridad"];
                    //Hago una funcion que lea prioridad y escriba el prioridad texto o lo configuro en presentacion
                    //eventoDTO.PrioridadTexto = (string)dataRow["PrioridadTexto"];
                    listaEventos.Add(eventoDTO);
                }
            }
            catch (Exception ex)
            {
                throw new Exception ("Error al convertir los datos" + ex);
            }
            return listaEventos;
        }

        public List<EventoDTO> CargarEventos(DateTime fechaInicio, DateTime fechaFinal)
        {
            string fInicio = fechaInicio.ToString("yyyyMMdd");
            string fFinal = fechaFinal.ToString("yyyyMMdd");
            return CargarListaDTOs(HelperDAO.CargarDataTable($"SELECT a.Id, a.IdPersona, a.Descripcion," +
                $" a.Prioridad, a.FechaHora, (b.Nombre + ' ' + b.Apellido) AS NombreApellido " +
                $"FROM Eventos a JOIN Personas b ON a.IdPersona = b.Id " +
                $"WHERE a.FechaHora between '{fInicio} 00:00:00' and '{fFinal} 23:59:59';"));
        }
    }
}
