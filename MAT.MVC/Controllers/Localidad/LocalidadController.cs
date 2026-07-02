using MAT.MVC.Infrastructure;
using MAT.MVC.Models;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MAT.MVC.Controllers.Localidad
{
    [Authorize]
    public class LocalidadController : Controller
    {
        private const string ResultDone = "Done.";

        public JsonResult GetProvincia(string sIdPais = "")
        {
            string payload = string.Empty;
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(sIdPais))
                {
                    var listProvincia = GeoDataAccess.GetProvinciasByPaisId(sIdPais)
                        .Select(MapProvincia)
                        .ToList();
                    payload = SerializeJson(listProvincia);
                    result = ResultDone;
                }
            }
            catch (Exception e)
            {
                result = BuildErrorMessage(e, "LocalidadController.GetProvincia");
            }

            return Json(new
            {
                LDepartamento = payload,
                LProvincia = payload,
                Result = result
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDepartamento(string sIdProvincia = "")
        {
            string payload = string.Empty;
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(sIdProvincia))
                {
                    int idProvincia;
                    if (!TryParsePositiveInt(sIdProvincia, out idProvincia))
                    {
                        return JsonCascadeError("Error: Provincia inválida.");
                    }

                    var listDepartamento = GeoDataAccess.GetDepartamentosByProvinciaId(idProvincia)
                        .Select(MapDepartamento)
                        .ToList();
                    payload = SerializeJson(listDepartamento);
                    result = ResultDone;
                }
            }
            catch (Exception e)
            {
                result = BuildErrorMessage(e, "LocalidadController.GetDepartamento");
            }

            return JsonCascade(payload, result);
        }

        public JsonResult AddLocalidad(string sIdDepartamento = "", string sLocalidad = "")
        {
            string id = string.Empty;
            string result = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(sIdDepartamento) || string.IsNullOrWhiteSpace(sLocalidad))
                {
                    return JsonAddLocalidad(id, "Error: Departamento y localidad son obligatorios.");
                }

                int idDepartamento;
                if (!TryParsePositiveInt(sIdDepartamento, out idDepartamento))
                {
                    return JsonAddLocalidad(id, "Error: Departamento inválido.");
                }

                id = GeoDataAccess.InsertLocalidad(idDepartamento, sLocalidad.ToUpper()).ToString();
                result = ResultDone;
            }
            catch (Exception e)
            {
                result = BuildErrorMessage(e, "LocalidadController.AddLocalidad");
            }

            return JsonAddLocalidad(id, result);
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult GetLocalidad(string sIdDepartamento = "")
        {
            string payload = string.Empty;
            string result = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(sIdDepartamento))
                {
                    int idDepartamento;
                    if (!TryParsePositiveInt(sIdDepartamento, out idDepartamento))
                    {
                        return JsonLocalidadError("Error: Departamento inválido.");
                    }

                    payload = SerializeJson(GeoDataAccess.GetLocalidadesByDepartamentoId(idDepartamento));
                    result = ResultDone;
                }
            }
            catch (Exception e)
            {
                result = BuildErrorMessage(e, "LocalidadController.GetLocalidad");
            }

            return JsonLocalidad(payload, result);
        }

        public JsonResult GetInfoByLocalidadId(string sLocalidaId = "")
        {
            string idLocalidad = string.Empty;
            string idDepartamento = string.Empty;
            string idProvincia = string.Empty;
            string idPais = string.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(sLocalidaId))
                {
                    int localidadId;
                    if (!TryParsePositiveInt(sLocalidaId, out localidadId))
                    {
                        return JsonGeoInfo(string.Empty, "Error: Localidad inválida.", string.Empty, string.Empty, string.Empty);
                    }

                    var info = GeoDataAccess.GetLocalidadGeoInfo(localidadId);
                    if (info != null)
                    {
                        idLocalidad = info.IdLocalidad.ToString();
                        idDepartamento = info.IdDepartamento.ToString();
                        idProvincia = info.IdProvincia.ToString();
                        idPais = info.IdPais.ToString();
                    }
                }
            }
            catch (Exception e)
            {
                return JsonGeoInfo(string.Empty, BuildErrorMessage(e, "LocalidadController.GetInfoByLocalidadId"), string.Empty, string.Empty, string.Empty);
            }

            return JsonGeoInfo(ResultDone, idLocalidad, idDepartamento, idProvincia, idPais);
        }

        public JsonResult Search(string term, int? idProvincia = null, int? idDepartamento = null)
        {
            try
            {
                var results = GeoDataAccess.SearchLocalidades(term, idProvincia, idDepartamento)
                    .Select(LocalidadLookupDto.From)
                    .ToList();
                return Json(results, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ErrorUtil.LogAndGetPublicMessage(e, "LocalidadController.Search");
                return Json(new List<LocalidadLookupDto>(), JsonRequestBehavior.AllowGet);
            }
        }

        private static Provincia MapProvincia(MAT.Entities.Provincia provincia)
        {
            return new Provincia
            {
                ID = provincia.Id,
                IdPais = provincia.IdPais.ToString(),
                Nombre = provincia.Nombre
            };
        }

        private static Departamento MapDepartamento(MAT.Entities.Departamento departamento)
        {
            return new Departamento
            {
                IdDepartamento = departamento.Id,
                Nombre = departamento.Nombre
            };
        }

        private static bool TryParsePositiveInt(string value, out int id)
        {
            return int.TryParse(value, out id) && id > 0;
        }

        private static string SerializeJson(object value)
        {
            return new JavaScriptSerializer().Serialize(value);
        }

        private static string BuildErrorMessage(Exception e, string context)
        {
            return "Error: " + ErrorUtil.LogAndGetPublicMessage(e, context);
        }

        private static JsonResult ToJson(object data)
        {
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        private static JsonResult JsonCascade(string payload, string result)
        {
            return ToJson(new { LDepartamento = payload, Result = result });
        }

        private static JsonResult JsonCascadeError(string errorMessage)
        {
            return ToJson(new { LDepartamento = string.Empty, Result = errorMessage });
        }

        private static JsonResult JsonLocalidad(string payload, string result)
        {
            return ToJson(new { LLocalidad = payload, Result = result });
        }

        private static JsonResult JsonLocalidadError(string errorMessage)
        {
            return ToJson(new { LLocalidad = string.Empty, Result = errorMessage });
        }

        private static JsonResult JsonAddLocalidad(string id, string result)
        {
            return ToJson(new { Id = id, Result = result });
        }

        private static JsonResult JsonGeoInfo(string result, string idLocalidad, string idDepartamento, string idProvincia, string idPais)
        {
            return ToJson(new
            {
                Result = result ?? string.Empty,
                IdLocalidad = idLocalidad ?? string.Empty,
                IdDepartamento = idDepartamento ?? string.Empty,
                IdProvincia = idProvincia ?? string.Empty,
                IdPais = idPais ?? string.Empty
            });
        }
    }
}
