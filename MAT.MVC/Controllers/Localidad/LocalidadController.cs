using MAT.Entities;
using MAT.MVC.Models;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MAT.MVC.Infrastructure;

namespace MAT.MVC.Controllers.Localidad
{
    [Authorize]
    public class LocalidadController : Controller
    {
        public JsonResult GetProvincia(string sIdPais = "")
        {
            string[] sResult = new string[2];
            
            try
            {
                if (sIdPais != "")
                {
                    List<MAT.MVC.Models.Provincia> ListProvincia = new List<Models.Provincia>();
                    
                    SqlParameter[] dbParams = new SqlParameter[]
                            {                    
                                DBHelper.MakeParam("@PaisID", SqlDbType.VarChar, 0, sIdPais),
                            };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_Provincia_GetAllByPaisID", dbParams))
                    {
                        while (_reader.Read())
                        {
                            MAT.MVC.Models.Provincia _Item = new MAT.MVC.Models.Provincia();
                            if (_reader["ID"].ToString() != "")
                            {
                                _Item.ID = Convert.ToInt32(_reader["ID"]);
                            }
                            _Item.IdPais = _reader["IdPais"].ToString();
                            _Item.Nombre = _reader["Nombre"].ToString();
                            ListProvincia.Add(_Item);
                        }
                    }

                    var jsonSerialiser = new JavaScriptSerializer();
                    var jLProvincia = jsonSerialiser.Serialize(ListProvincia);
                    sResult[0] = jLProvincia;
                    sResult[1] = "Done.";
                }

            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "LocalidadController.GetProvincia");
            }

            return Json(new
            {
                LDepartamento = sResult[0],
                Result = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDepartamento(string sIdProvincia = "")
        {
            string[] sResult = new string[2];

            try
            {
                if (sIdProvincia != "")
                {
                    int iIdProvincia = Convert.ToInt32(sIdProvincia);
                    var departamentos = GeoDataAccess.GetDepartamentosByProvinciaId(iIdProvincia);

                    List<MAT.MVC.Models.Departamento> ListDepartamento = new List<MAT.MVC.Models.Departamento>();
                    foreach (var item in departamentos)
                    {
                        MAT.MVC.Models.Departamento oDepartamento = new MAT.MVC.Models.Departamento();
                        oDepartamento.IdDepartamento = item.Id;
                        oDepartamento.Nombre = item.Nombre;
                        ListDepartamento.Add(oDepartamento);
                    }

                    var jsonSerialiser = new JavaScriptSerializer();
                    var jLDepartamento = jsonSerialiser.Serialize(ListDepartamento.OrderBy(item => item.Nombre));
                    sResult[0] = jLDepartamento;
                    sResult[1] = "Done.";
                }

            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "LocalidadController.GetDepartamento");
            }

            return Json(new
            {
                LDepartamento = sResult[0],
                Result = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /Localidad/Create

        public JsonResult AddLocalidad(string sIdDepartamento = "", string sLocalidad = "")
        {
            string[] sResult = new string[2];

            try
            {
                if (string.IsNullOrWhiteSpace(sIdDepartamento) || string.IsNullOrWhiteSpace(sLocalidad))
                {
                    sResult[1] = "Error: Departamento y localidad son obligatorios.";
                    return Json(new
                    {
                        Id = sResult[0],
                        Result = sResult[1]
                    }, JsonRequestBehavior.AllowGet);
                }

                int idDepartamento = Convert.ToInt32(sIdDepartamento);
                GeoDataAccess.InsertLocalidad(idDepartamento, sLocalidad.ToUpper());
                
                sResult[0] = "";
                sResult[1] = "Done.";
              

            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "LocalidadController.AddLocalidad");
            }

            return Json(new
            {
                Id = sResult[0],
                Result = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Localidad/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Localidad/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Localidad/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetLocalidad(string sIdDepartamento = "")
        {
            string[] sResult = new string[2];
            try
            {
                List<Entities.VLocalidad> ListLlocalidades = new List<Entities.VLocalidad>();


                if (sIdDepartamento != "")
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@IdDepartamento", SqlDbType.Int, 0, Convert.ToInt32(sIdDepartamento)),
                    };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_Localidad_GetByIdDepartamento", dbParams))
                    {
                        while (_reader.Read())
                        {
                            VLocalidad item = new VLocalidad();
                            item.Id = Convert.ToInt32(_reader["ID"].ToString());
                            item.Nombre = _reader["Nombre"].ToString();
                            ListLlocalidades.Add(item);
                        }
                    }

                    var jsonSerialiser = new JavaScriptSerializer();
                    var jListLlocalidades = jsonSerialiser.Serialize(ListLlocalidades);
                    sResult[0] = jListLlocalidades;
                    sResult[1] = "Done.";
                }
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "LocalidadController.GetLocalidad");
            }

            return Json(new
            {
                LLocalidad = sResult[0],
                Result = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInfoByLocalidadId(string sLocalidaId = "")
        {
            string[] sResult = new string[5];
            try
            {
                List<Entities.VLocalidad> ListLlocalidades = new List<Entities.VLocalidad>();


                if (sLocalidaId != "")
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@idLocalidad", SqlDbType.Int, 0, Convert.ToInt32(sLocalidaId)),
                    };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_GetInfoByLocalidadId", dbParams))
                    {
                        while (_reader.Read())
                        {
                            sResult[1] = _reader["IdLocalidad"].ToString();
                            sResult[2] = _reader["IdDepartamento"].ToString();
                            sResult[3] = _reader["IdProvincia"].ToString();
                            sResult[4] = _reader["IdPais"].ToString();
                        }
                    }

                   
                }
                sResult[0] = "Done.";
            }
            catch (Exception e)
            {
                sResult[0] = "";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "LocalidadController.GetLocalidadId");
            }

            return Json(new
            {
                Result = sResult[0],
                IdLocalidad = sResult[1],
                IdDepartamento = sResult[2],
                IdProvincia = sResult[3],
                IdPais = sResult[4]
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
