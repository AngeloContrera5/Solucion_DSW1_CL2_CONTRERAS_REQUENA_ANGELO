using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DSW1_CL2_CONTRERAS_REQUENA_ANGELO.Models;

namespace DSW1_CL2_CONTRERAS_REQUENA_ANGELO.Controllers
{
    public class HorarioController : Controller
    {
        string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

        IEnumerable<Curso>cursos()
        {
            List<Curso> temporal = new List<Curso>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_cursos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Curso reg = new Curso()
                    {
                        codCurso = dr.GetInt32(0),
                        nomCurso = dr.GetString(1)
                    };
                    temporal.Add(reg);
                }
                dr.Close();
                cn.Close();
            }
            return temporal;
        }

        IEnumerable<Horario>horarios(int? id = null)
        {
            List<Horario> temporal = new List<Horario>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_listar_horario_fecIni", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Horario reg = new Horario();
                    reg.codHorario = dr.GetInt32(0);                    
                    reg.nomCurso = dr.GetString(1);
                    reg.codCurso = dr.GetInt32(2);
                    reg.fecInicio = dr.GetDateTime(3);
                    reg.fecTerminio = dr.GetDateTime(4);
                    reg.vacantes = dr.GetInt32(5);
                                  
                    temporal.Add(reg);
                }
                dr.Close();
                cn.Close();
            }
            return temporal;
        }
        /*
        
        IEnumerable<Horario>horarios(string proc, SqlParameter sql = null)
        {
            List<Horario> temporal = new List<Horario>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand(proc, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (sql != null) cmd.Parameters.Add(sql);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Horario reg = new Horario();
                    reg.codHorario = dr.GetInt32(0);                    
                    reg.nomCurso = dr.GetString(1);
                    reg.codCurso = dr.GetInt32(2);
                    reg.fecInicio = dr.GetDateTime(3);
                    reg.fecTerminio = dr.GetDateTime(4);
                    reg.vacantes = dr.GetInt32(5);
                                  
                    temporal.Add(reg);
                }
                dr.Close();
                cn.Close();
            }
            return temporal;
        }
        
        
        
        ------------------------------------
        Curso Buscar(int? id = null)
        {
            return cursos().Where(c => c.codCurso == id).FirstOrDefault();
        }

        -------------------------------------
        public ActionResult Index(int cod = 0)
        {
            ViewBag.curso = new SelectList(cursos(), "codCurso", "nomCurso", cod);

            SqlParameter par = new SqlParameter("@codCur", cod);

            return View(horarios("sp_listar_registrosxcurso", par));
        }

        */
        Curso BuscarCurso(int? id = null)
        {
            return cursos().Where(c => c.codCurso == id).FirstOrDefault();
        }

        public ActionResult Index(int id = 0)
        {

            Curso reg = BuscarCurso(id);
            ViewBag.curso = new SelectList(cursos(), "codCurso", "nomCurso", id);

            SqlParameter par = new SqlParameter("@codCur", id);

            return View(reg);
        }

        ////////////////////////////////////PREGUNTA 3////////////////////////////////////

        /*
        Horario BuscaHorario(int? id = null)
        {
           return horarios("SELECT codhorario, codcurso, fecinicio, fecterminio, vacantes FROM tb_horario WHERE codhorario = id").Where(h => h.codHorario == id).FirstOrDefault();
        }


        IEnumerable<Horario>BuscarHorario(int id)
        {
        List<Horario> temporal = new List<Horario>();
        using (SqlConnection cn = new SqlConnection(cadena))
        {
            SqlCommand cmd = new SqlCommand("sp_listar_horarioxcodHor", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codHorario", id);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Horario reg = new Horario()
                {
                    nomCurso = dr.GetString(0),
                    fecInicio = dr.GetDateTime(1),
                    fecTerminio = dr.GetDateTime(2),
                    vacantes = dr.GetInt32(3)
                };
                temporal.Add(reg);
            }
            dr.Close();
            cn.Close();
        }
        return temporal;
        }

        public ActionResult Registro(int id = 0)
        {
         return View(BuscarHorario(id));

        }

        [HttpPost]
        public ActionResult Registro(int idVacante, int codHorario, string dni, string nombre, string fono, string email)
        {
         SqlConnection cn = new SqlConnection(cadena);
         cn.Open();
         SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
         try
         {
             SqlCommand cmd = new SqlCommand(
             "Insert tb_registro values(@idVac, @codHor, @fecReg, @dni, @nom, @fono, @email)", cn, tr);

             cmd.Parameters.AddWithValue("@idVac", idVacante);
             cmd.Parameters.AddWithValue("@codHor", codHorario);
             cmd.Parameters.AddWithValue("@fecReg", DateTime.Now);
             cmd.Parameters.AddWithValue("@dni", dni);
             cmd.Parameters.AddWithValue("@nom", nombre);
             cmd.Parameters.AddWithValue("@fono", fono);
             cmd.Parameters.AddWithValue("@email", email);
             cmd.ExecuteNonQuery();

             cmd = new SqlCommand(
             "update tb_horario set vacantes = vacantes -1", cn, tr);
             cmd.ExecuteNonQuery();

             tr.Commit();

             ViewBag.mensaje = "Registrado :D";
             //TempData["mensaje"] = "Registrado :D";

                 }
                 catch (SqlException ex)
                 {
                     ViewBag.mensaje = ex.Message;
             //TempData["mensaje"] = ex.Message;

             tr.Rollback();
         }
         finally
         {
             cn.Close();
         }
         return RedirectToAction("Index");
        }

            */

        Horario BuscarHorario(int? id = null)
        {
            return horarios().Where(h => h.codHorario == id).FirstOrDefault();
        }


        public ActionResult Registro(int?id = null)
        {
            if (id == null)
                return RedirectToAction("Index");
            else
            {
                Horario reg = BuscarHorario(id);
                return View(reg);
            }
        }

        [HttpPost]
        public ActionResult Registro(int codHorario, string dni, string nombre, string fono, string email)
        {
            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            SqlTransaction tr = cn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_registroHorario", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codHorario", codHorario);
                cmd.Parameters.AddWithValue("@fecRegistro", DateTime.Now);
                cmd.Parameters.AddWithValue("@dni", dni);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@fono", fono);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("sp_actualiza_registroHorario", cn, tr);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@@codHorario", codHorario);
                cmd.ExecuteNonQuery();

                tr.Commit();
                ViewBag.mensaje = "Registrado :D";
            }
            catch (SqlException ex)
            {
                ViewBag.mensaje = "No Registrado D:";
                tr.Rollback();
            }
            finally { cn.Close(); }
            Horario reg = BuscarHorario(codHorario);
            return View(reg);
        }
    }
}