using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T3_RamirezMoreno.Models;

namespace T3_RamirezMoreno.Controllers
{
    public class ConsumidorController : Controller
    {

        private string cn = ConfigurationManager.ConnectionStrings["conexionADatos"].ConnectionString;

        IEnumerable<Consumidor> obtenerConsumidores()
        {
            List<Consumidor> lista = new List<Consumidor>();
            SqlConnection sqlConnection = new SqlConnection(cn);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("usp_listar_consumidor", sqlConnection);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new Consumidor
                {
                    dnicont = dr.GetString(0),
                    nomcont = dr.GetString(1),
                    apecont = dr.GetString(2),
                    emailcont = dr.IsDBNull(3) ? "" : dr.GetString(3),
                    idpais = dr.IsDBNull(4) ? "" : dr.GetString(4)
                });
            }
            return lista;
        }

        void eliminarConsumidor(string dni)
        {
            SqlConnection sqlConnection = new SqlConnection(cn);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("usp_eliminar_consumidor", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dnicont", dni);
            cmd.ExecuteNonQuery();
        }

        Consumidor buscarPorDni(string dni)
        {
            SqlConnection sqlConnection = new SqlConnection(cn);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("usp_buscar_consumidor", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dnicont", dni);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return new Consumidor
                {
                    dnicont = dr.GetString(0),
                    nomcont = dr.GetString(1),
                    apecont = dr.GetString(2),
                    emailcont = dr.IsDBNull(3) ? "" : dr.GetString(3),
                    idpais = dr.IsDBNull(4) ? "" : dr.GetString(4)
                };
            }
            return null;
        }

        void actualizarConsumidor(Consumidor c)
        {
            SqlConnection sqlConnection = new SqlConnection(cn);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("usp_actualizar_consumidor", sqlConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dnicont", c.dnicont);
            cmd.Parameters.AddWithValue("@nomcont", c.nomcont);
            cmd.Parameters.AddWithValue("@apecont", c.apecont);
            cmd.Parameters.AddWithValue("@emailcont", c.emailcont ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@idpais", c.idpais ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public ActionResult Index() => View(obtenerConsumidores());

        public ActionResult Create() => View(new Consumidor());

        [HttpPost]
        public ActionResult Create(Consumidor consumidor)
        {
            agregarConsumidor(consumidor);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            eliminarConsumidor(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string id) => View(buscarPorDni(id));

        public ActionResult Update(string id) => View(buscarPorDni(id));

        
    
    }
}