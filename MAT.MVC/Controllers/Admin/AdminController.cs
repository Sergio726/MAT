using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.MVC.Models;
using MAT.Utilities;
using MAT.Services;
using MAT.Enums;
using MAT.MVC.Common;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using MAT.MVC.Infrastructure;
using MAT.MVC.Filters;
using WebMatrix.WebData;
using Microsoft.Web.WebPages.OAuth;
using AccountManageMessageId = MAT.MVC.Controllers.Account.AccountController.ManageMessageId;
namespace MAT.MVC.Controllers.Admin
{
    [InitializeSimpleMembership]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (!Roles.IsUserInRole(User.Identity.Name, "Administrador")) return RedirectToAction("Index", "Home");
            return View();
        }

        [Authorize]
        public ActionResult Usuarios()
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;

            var list = new List<AdminUserListItem>();
            using (var ctx = new UsersContext())
            {
                foreach (var u in ctx.UserProfiles.AsEnumerable().OrderBy(x => x.UserName))
                {
                    string[] roleArray = { };
                    try
                    {
                        roleArray = Roles.GetRolesForUser(u.UserName) ?? new string[] { };
                    }
                    catch
                    {
                        roleArray = new string[] { };
                    }

                    bool isApproved, isLocked;
                    TryPopulateMembershipDisplayForAdmin(u.UserName, out isApproved, out isLocked);

                    list.Add(new AdminUserListItem
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        RolesSummary = roleArray.Length > 0 ? string.Join(", ", roleArray) : "—",
                        IsApproved = isApproved,
                        IsLockedOut = isLocked
                    });
                }
            }

            if (TempData["UserMessage"] != null)
                ViewBag.StatusMessage = TempData["UserMessage"];
            return View(list);
        }

        [Authorize]
        public ActionResult UsuarioEditar(int? id)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;
            if (id == null) return RedirectToAction("Usuarios");

            UserProfile profile;
            using (var ctx = new UsersContext())
            {
                profile = ctx.UserProfiles.FirstOrDefault(x => x.UserId == id.Value);
            }

            if (profile == null)
            {
                TempData["UserMessage"] = "Usuario no encontrado.";
                return RedirectToAction("Usuarios");
            }

            var userName = profile.UserName;
            bool isAdmin = false;
            try
            {
                isAdmin = Roles.IsUserInRole(userName, "Administrador");
            }
            catch { }

            bool isApproved, isLocked;
            TryPopulateMembershipDisplayForAdmin(userName, out isApproved, out isLocked);

            var model = new AdminUsuarioEditModel
            {
                UserId = id.Value,
                UserName = userName,
                EsAdministrador = isAdmin,
                IsApproved = isApproved,
                IsLockedOut = isLocked
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioEditar(AdminUsuarioEditModel model)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;

            if (model == null || model.UserId <= 0)
                return RedirectToAction("Usuarios");

            UserProfile profile;
            using (var ctx = new UsersContext())
            {
                profile = ctx.UserProfiles.FirstOrDefault(x => x.UserId == model.UserId);
            }

            if (profile == null)
            {
                TempData["UserMessage"] = "Usuario no encontrado.";
                return RedirectToAction("Usuarios");
            }

            var targetUserName = profile.UserName;
            model.UserName = targetUserName;

            bool currentlyAdmin = false;
            try
            {
                currentlyAdmin = Roles.IsUserInRole(targetUserName, "Administrador");
            }
            catch { }

            if (model.EsAdministrador && !currentlyAdmin)
            {
                try
                {
                    Roles.AddUserToRole(targetUserName, "Administrador");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", ErrorUtil.LogAndGetPublicMessage(e, "AdminController.UsuarioEditar.AddRole"));
                }
            }
            else if (!model.EsAdministrador && currentlyAdmin)
            {
                if (string.Equals(User.Identity.Name, targetUserName, StringComparison.OrdinalIgnoreCase))
                    ModelState.AddModelError("", "No puede quitarse el rol Administrador a usted mismo.");
                else if (CountUsersInRole("Administrador") <= 1)
                    ModelState.AddModelError("", "No puede quitarse el último administrador del sistema.");
                else
                {
                    try
                    {
                        Roles.RemoveUserFromRole(targetUserName, "Administrador");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", ErrorUtil.LogAndGetPublicMessage(e, "AdminController.UsuarioEditar.RemoveRole"));
                    }
                }
            }

            if (ModelState.IsValid)
            {
                TempData["UserMessage"] = "Usuario actualizado correctamente.";
                return RedirectToAction("Usuarios");
            }

            TryPopulateMembershipDisplayForAdmin(targetUserName, out bool reIsApproved, out bool reIsLocked);
            model.IsApproved = reIsApproved;
            model.IsLockedOut = reIsLocked;

            return View(model);
        }

        [Authorize]
        public ActionResult UsuarioResetPassword(int? id)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;
            if (id == null) return RedirectToAction("Usuarios");

            UserProfile profile;
            using (var ctx = new UsersContext())
            {
                profile = ctx.UserProfiles.FirstOrDefault(x => x.UserId == id.Value);
            }

            if (profile == null)
            {
                TempData["UserMessage"] = "Usuario no encontrado.";
                return RedirectToAction("Usuarios");
            }

            var m = new AdminUsuarioResetPasswordModel
            {
                UserId = id.Value,
                UserName = profile.UserName
            };
            return View(m);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioResetPassword(AdminUsuarioResetPasswordModel model)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;

            if (model == null || model.UserId <= 0)
                return RedirectToAction("Usuarios");

            UserProfile profile;
            using (var ctx = new UsersContext())
            {
                profile = ctx.UserProfiles.FirstOrDefault(x => x.UserId == model.UserId);
            }

            if (profile == null)
            {
                TempData["UserMessage"] = "Usuario no encontrado.";
                return RedirectToAction("Usuarios");
            }

            model.UserName = profile.UserName;

            if (!ModelState.IsValid)
                return View(model);

            try
            {
                string resetToken = WebSecurity.GeneratePasswordResetToken(profile.UserName);
                WebSecurity.ResetPassword(resetToken, model.NewPassword);
                TempData["UserMessage"] = "La contraseña se restableció correctamente.";
                return RedirectToAction("Usuarios");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", ErrorUtil.LogAndGetPublicMessage(e, "AdminController.UsuarioResetPassword"));
                return View(model);
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioEstablecerEstado(int userId, bool aprobado)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;

            string userName = GetUserNameByProfileId(userId);
            if (string.IsNullOrEmpty(userName))
            {
                TempData["UserMessage"] = "Usuario no encontrado.";
                return RedirectToAction("Usuarios");
            }

            if (!aprobado && string.Equals(User.Identity.Name, userName, StringComparison.OrdinalIgnoreCase))
            {
                TempData["UserMessage"] = "No puede deshabilitar su propia cuenta.";
                return RedirectToAction("Usuarios");
            }

            if (!aprobado)
            {
                bool targetIsAdmin = false;
                try
                {
                    targetIsAdmin = Roles.IsUserInRole(userName, "Administrador");
                }
                catch { }

                if (targetIsAdmin && CountUsersInRole("Administrador") <= 1)
                {
                    TempData["UserMessage"] = "No puede deshabilitar el último administrador del sistema.";
                    return RedirectToAction("Usuarios");
                }
            }

            try
            {
                // SimpleMembershipProvider no implementa Membership.UpdateUser para IsApproved (lanza NotSupportedException).
                // IsApproved del usuario corresponde a IsConfirmed en dbo.webpages_Membership.
                if (Membership.Provider is SimpleMembershipProvider)
                {
                    using (var ctx = new UsersContext())
                    {
                        var rows = ctx.Database.ExecuteSqlCommand(
                            "UPDATE dbo.webpages_Membership SET IsConfirmed = @p0 WHERE UserId = @p1",
                            aprobado,
                            userId);
                        if (rows == 0)
                        {
                            TempData["UserMessage"] = "No se encontró fila de membresía (webpages_Membership) para este usuario.";
                            return RedirectToAction("Usuarios");
                        }
                    }

                    TempData["UserMessage"] = aprobado ? "Usuario habilitado." : "Usuario deshabilitado.";
                    return RedirectToAction("Usuarios");
                }

                MembershipUser mu = Membership.GetUser(userName, false);
                if (mu == null)
                {
                    TempData["UserMessage"] = "No se encontró la cuenta de inicio de sesión (membership) para este usuario.";
                    return RedirectToAction("Usuarios");
                }

                mu.IsApproved = aprobado;
                Membership.UpdateUser(mu);
                TempData["UserMessage"] = aprobado ? "Usuario habilitado." : "Usuario deshabilitado.";
            }
            catch (Exception e)
            {
                TempData["UserMessage"] = ErrorUtil.LogAndGetPublicMessage(e, "AdminController.UsuarioEstablecerEstado");
            }

            return RedirectToAction("Usuarios");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioDesbloquear(int userId)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;

            string userName = GetUserNameByProfileId(userId);
            if (string.IsNullOrEmpty(userName))
            {
                TempData["UserMessage"] = "Usuario no encontrado.";
                return RedirectToAction("Usuarios");
            }

            try
            {
                MembershipUser mu = Membership.GetUser(userName, false);
                if (mu != null && mu.IsLockedOut)
                    mu.UnlockUser();
                TempData["UserMessage"] = "Cuenta desbloqueada.";
            }
            catch (Exception e)
            {
                TempData["UserMessage"] = ErrorUtil.LogAndGetPublicMessage(e, "AdminController.UsuarioDesbloquear");
            }

            return RedirectToAction("Usuarios");
        }

        private static string GetUserNameByProfileId(int userId)
        {
            using (var ctx = new UsersContext())
            {
                var p = ctx.UserProfiles.FirstOrDefault(x => x.UserId == userId);
                return p?.UserName;
            }
        }

        private static int CountUsersInRole(string roleName)
        {
            try
            {
                string[] users = Roles.GetUsersInRole(roleName);
                return users?.Length ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        [Authorize]
        public ActionResult RegistrarVendedor()
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarVendedor(FormCollection form)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;

            if (string.IsNullOrWhiteSpace(form["Password"]) || form["Password"] != form["ConfirmPassword"])
                ModelState.AddModelError("", "La contraseña y la confirmación no coinciden o están vacías.");
            if (string.IsNullOrWhiteSpace(form["UserName"]))
                ModelState.AddModelError("", "El nombre de usuario es obligatorio.");

            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(form["UserName"], form["Password"]);
                    RegistroVendedorModel.InsertVendedor(form);
                    TempData["UserMessage"] = "El usuario vendedor se registró correctamente.";
                    return RedirectToAction("Usuarios");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", MembershipCreateErrorToString(e.StatusCode));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", ErrorUtil.LogAndGetPublicMessage(e, "AdminController.RegistrarVendedor"));
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult MiCuenta(AccountManageMessageId? message)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;

            ViewBag.StatusMessage =
                message == AccountManageMessageId.ChangePasswordSuccess ? "La contraseña se ha cambiado."
                : message == AccountManageMessageId.SetPasswordSuccess ? "Su contraseña se ha establecido."
                : message == AccountManageMessageId.RemoveLoginSuccess ? "El inicio de sesión externo se ha quitado."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("MiCuenta", "Admin");
            ViewBag.PasswordFormAction = "MiCuenta";
            ViewBag.PasswordFormController = "Admin";
            return View("MiCuenta", new LocalPasswordModel());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult MiCuenta(LocalPasswordModel model)
        {
            var redir = RequireAdministrator();
            if (redir != null) return redir;

            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("MiCuenta", "Admin");
            ViewBag.PasswordFormAction = "MiCuenta";
            ViewBag.PasswordFormController = "Admin";

            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("MiCuenta", new { Message = AccountManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "La contraseña actual es incorrecta o la nueva contraseña no es válida.");
                    }
                }
            }
            else
            {
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("MiCuenta", new { Message = AccountManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", string.Format("No se puede crear una cuenta local. Es posible que ya exista una cuenta con el nombre \"{0}\".", User.Identity.Name));
                    }
                }
            }

            return View("MiCuenta", model);
        }

        private ActionResult RequireAdministrator()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            try
            {
                if (!Roles.IsUserInRole(User.Identity.Name, "Administrador"))
                    return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

            return null;
        }

        private static string MembershipCreateErrorToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario ya existe. Escriba un nombre de usuario diferente.";
                case MembershipCreateStatus.DuplicateEmail:
                    return "Ya existe un nombre de usuario para esa dirección de correo electrónico.";
                case MembershipCreateStatus.InvalidPassword:
                    return "La contraseña especificada no es válida.";
                case MembershipCreateStatus.InvalidUserName:
                    return "El nombre de usuario especificado no es válido.";
                default:
                    return "No se pudo crear el usuario. Compruebe los datos e inténtelo de nuevo.";
            }
        }

        public ActionResult ResumenPagos()
        {
            try
            {
                List<DDViaje> _DDViaje = new List<DDViaje>();
                _DDViaje = MAT.MVC.Models.ViajeMethod.DDViaje();

                var json = "";
                var jsonSerialiser = new JavaScriptSerializer();
                json = jsonSerialiser.Serialize(_DDViaje);
                ViewBag.jDDViaje = json;
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "AdminController.ResumenPagos");
                ViewBag.jDDViaje = "[]";
            }
            
            return View();
        }

        public ActionResult ResumenPagosPorFecha()
        {
            return View();
        }

        public ActionResult GridResumenPagos(Guid ViajeID)
        {
            List<PagoModel> _model = new List<PagoModel>();
            try
            {
                _model = PagoMethod.GetPagosByViaje(ViajeID);
                ViewBag.TotalPagos = _model.Sum(l => l.Monto);

                var jsonPatientList = JsonConvert.SerializeObject(_model);
                ViewBag.sbDataSetJson = jsonPatientList.ToString();
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "AdminController.GridResumenPagos");
            }
            return PartialView();
        }

        public ActionResult GridResumenPagosFecha(string fecha)
        {
            try
            {
                List<PagoModel> model = new List<PagoModel>();
                DateTime dFecha = Convert.ToDateTime(fecha);
                model = PagoMethod.GetPagosByFecha(dFecha);
                return PartialView(model);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error: " + e.Message;
                return PartialView();

            }
        }

        public ActionResult GridPlanillaHotelPrint(Guid viajeid, Guid planillaid)
        {
            List<List<PlanillaHotelPrintModel>> conjuntoplanillas = new List<List<PlanillaHotelPrintModel>>();
            Services.ViajeHotelService viajehotelService = new ViajeHotelService();
            List<Entities.ViajeHotel> hoteles = viajehotelService.GetByViajeId(viajeid).ToList();
            foreach (var h in hoteles)
            {
                Entities.Hotel hotel = new Services.HotelService().GetByHotelId(h.HotelId);
                List<Entities.Habitacion> _habitaciones = new HabitacionService().GetByHotelId(h.HotelId).ToList();
                List<Models.PlanillaHotelPrintModel> _planillahotelmodel = new List<Models.PlanillaHotelPrintModel>();
                foreach (var item in _habitaciones)
                {
                    PlanillaHotelPrintModel planillahotellinea = new Models.PlanillaHotelPrintModel(item.HabitacionId, planillaid, hotel.Nombre, viajeid);
                    _planillahotelmodel.Add(planillahotellinea);
                }
                conjuntoplanillas.Add(_planillahotelmodel.OrderBy(pl => pl.Habitacion.Tipo).ToList());
            }
            return PartialView(conjuntoplanillas);
        }

        public ActionResult GridPlanillaHotelDetallePrint(Guid viajeid, Guid planillaid)
        {
            List<List<PlanillaHotelPrintModel>> conjuntoplanillas = new List<List<PlanillaHotelPrintModel>>();
            Services.ViajeHotelService viajehotelService = new ViajeHotelService();
            List<Entities.ViajeHotel> hoteles = viajehotelService.GetByViajeId(viajeid).ToList();
            foreach (var h in hoteles)
            {
                Entities.Hotel hotel = new Services.HotelService().GetByHotelId(h.HotelId);
                List<Entities.Habitacion> _habitaciones = new HabitacionService().GetByHotelId(h.HotelId).ToList();
                List<Models.PlanillaHotelPrintModel> _planillahotelmodel = new List<Models.PlanillaHotelPrintModel>();
                foreach (var item in _habitaciones)
                {
                    PlanillaHotelPrintModel planillahotellinea = new Models.PlanillaHotelPrintModel(item.HabitacionId, planillaid, hotel.Nombre, viajeid);
                    _planillahotelmodel.Add(planillahotellinea);
                }
                conjuntoplanillas.Add(_planillahotelmodel.OrderBy(pl => pl.Habitacion.Tipo).ToList());
            }
            return PartialView(conjuntoplanillas);
        }

        public ActionResult PartialGridResumenPlanillaHotelPrint(List<PlanillaHotelPrintModel> planilla)
        {
            List<ResumenPlanillaPrintModel> resumen = new List<ResumenPlanillaPrintModel>();
            foreach (HabitacionTipo item in HabitacionTipoMethod.GetAllHabitacionTipo())
            {
                resumen.Add(new ResumenPlanillaPrintModel(planilla, item.Id));
            }
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Single));
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Doble));
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Matrimonial));
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Triple));
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Cuadruple));
            double _total = 0;
            foreach (var item in resumen)
            {
                _total += item.Subtotal;
            }
            ViewData["TotalHotel"] = _total;
            ViewData["Hotel"] = planilla.FirstOrDefault().Hotel;
            return PartialView(resumen);
        }

        public ActionResult ImprimirPlanilla(Guid planillaid)
        {
            
            return PartialView(new Services.PlanillaService().GetByPlanillaId(planillaid));
        }

        public ActionResult ImprimirPlanillaDetalle(Guid planillaid)
        {

            return PartialView(new Services.PlanillaService().GetByPlanillaId(planillaid));
        }

        public ActionResult GridPlanillaServiciosItemPrint(Guid planillaid)
        {
            Services.PlanillaServicioItemService servicioitemService = new PlanillaServicioItemService();
            List<Entities.PlanillaServicioItem> servicioitems = servicioitemService.GetByPlanillaId(planillaid).ToList();
            return PartialView(servicioitems);
        }

        public ActionResult DeletePlanilla(Guid id)
        {
            Services.PlanillaServicioItemService planillaservicioService = new PlanillaServicioItemService();
            Services.PlanillaHabitacionItemService planillahotelService = new PlanillaHabitacionItemService();
            Services.PlanillaService planillaService = new PlanillaService();
            List<Entities.PlanillaServicioItem> servicios = planillaservicioService.GetByPlanillaId(id).ToList();
            for (int i = servicios.Count-1; i > -1; i--)
            {
                var item = servicios[i];
                planillaservicioService.Delete(item.PlanillaServicioItemId);
            }

            List<Entities.PlanillaHabitacionItem> habitaciones = planillahotelService.GetByPlanillaId(id).ToList();
            for (int i = habitaciones.Count-1; i > -1; i--)
            {
                var item = habitaciones[i];
                planillahotelService.Delete(item.PlanillaHabitacionItemId);
            }
            planillaService.Delete(id);
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult EditarPlanilla(Guid id)
        {
            return View(new Services.PlanillaService().GetByPlanillaId(id));
        }

        public ActionResult GridPlanillaHotelDetalleEdit(Guid planillaid, Guid viajeid)
        {
            List<List<PlanillaHotelPrintModel>> conjuntoplanillas = new List<List<PlanillaHotelPrintModel>>();
            Services.ViajeHotelService viajehotelService = new ViajeHotelService();
            List<Entities.ViajeHotel> hoteles = viajehotelService.GetByViajeId(viajeid).ToList();
            foreach (var h in hoteles)
            {
                Entities.Hotel hotel = new Services.HotelService().GetByHotelId(h.HotelId);
                List<Entities.Habitacion> _habitaciones = new HabitacionService().GetByHotelId(h.HotelId).ToList();
                List<Models.PlanillaHotelPrintModel> _planillahotelmodel = new List<Models.PlanillaHotelPrintModel>();
                foreach (var item in _habitaciones)
                {
                    PlanillaHotelPrintModel planillahotellinea = new Models.PlanillaHotelPrintModel(item.HabitacionId, planillaid, hotel.Nombre, viajeid);
                    _planillahotelmodel.Add(planillahotellinea);
                }
                conjuntoplanillas.Add(_planillahotelmodel.OrderBy(pl => pl.Habitacion.Tipo).ToList());
            }
            return PartialView(conjuntoplanillas);
        }

        public ActionResult GridPlanillaServiciosItemEdit(Guid planillaid)
        {
            Services.PlanillaServicioItemService servicioitemService = new PlanillaServicioItemService();
            List<Entities.PlanillaServicioItem> servicioitems = servicioitemService.GetByPlanillaId(planillaid).ToList();
            return PartialView(servicioitems);
        }

        public bool GuardarDatosPlanilla(Guid planillaid, string fecha, string total)
        {
            bool result = false;
            try
            {
                DateTime _fecha = !string.IsNullOrEmpty(fecha) ? Convert.ToDateTime(fecha) : DateTime.Now;
                Double _total = !string.IsNullOrEmpty(total) ? Convert.ToDouble(total) : 0;
                Services.PlanillaService planillaService = new PlanillaService();
                Entities.Planilla planilla = planillaService.GetByPlanillaId(planillaid);
                planilla.FechaRegistro = _fecha;
                planilla.Total = _total;
                planillaService.Update(planilla);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public bool GuardarDatosItem(Guid itemid, string dias, string subtotal)
        {
            bool result = false;
            try
            {
                PlanillaHabitacionItemService habitacionitemService = new PlanillaHabitacionItemService();
                Entities.PlanillaHabitacionItem habitacionitem = habitacionitemService.GetByPlanillaHabitacionItemId(itemid);
                habitacionitem.Cantidad = !string.IsNullOrEmpty(dias) ? Convert.ToInt32(dias) : 0 ;
                habitacionitem.Subtotal = !string.IsNullOrEmpty(subtotal) ? Convert.ToDouble(subtotal) : 0;
                habitacionitemService.Update(habitacionitem);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public bool GuardarDatosServiciosItem(Guid id, string cantidad, string subtotal)
        {
            bool result = false;
            try
            {
                Services.PlanillaServicioItemService servicioitemService = new PlanillaServicioItemService();
                Entities.PlanillaServicioItem servicioitem = servicioitemService.GetByPlanillaServicioItemId(id);
                servicioitem.Cantidad = !string.IsNullOrEmpty(cantidad) ? Convert.ToInt32(cantidad) : 0;
                servicioitem.Subtotal = !string.IsNullOrEmpty(subtotal) ? Convert.ToDouble(subtotal) : 0;
                servicioitemService.Update(servicioitem);
                result = true;
            }
            catch (Exception )
            {
                result = false;
            }
            return result;
        }

        [Authorize]
        public ActionResult AuditoriaFacturas() {
            return View();
        }

        [Authorize]
        public ActionResult Logs(string correlationId = null)
        {
            if (!IsAdminUser()) return RedirectToAction("Index", "Home");

            var logs = MATLogger.GetRecentLogs(1000);

            if (!string.IsNullOrWhiteSpace(correlationId))
                logs = logs.Where(l => l.Contains(correlationId)).ToList();

            ViewBag.CorrelationId = correlationId;
            ViewBag.Logs = logs;
            return View();
        }

        [Authorize]
        public ContentResult LogsRaw(string correlationId = null)
        {
            if (!IsAdminUser()) return Content("Sin permisos");

            var logs = MATLogger.GetRecentLogs(1000);
            if (!string.IsNullOrWhiteSpace(correlationId))
                logs = logs.Where(l => l.Contains(correlationId)).ToList();

            return Content(string.Join("\n", logs), "text/plain", Encoding.UTF8);
        }

        [Authorize]
        public ActionResult ErrorLog()
        {
            if (!IsAdminUser()) return RedirectToAction("Index", "Home");
            return View();
        }

        [Authorize]
        public JsonResult ErrorLogJson(string correlationId = null, string fechaDesde = null)
        {
            if (!IsAdminUser())
                return Json(new { ok = false, mensaje = "Sin permisos" }, JsonRequestBehavior.AllowGet);

            DateTime? fecha = null;
            if (!string.IsNullOrWhiteSpace(fechaDesde))
            {
                DateTime parsed;
                if (DateTime.TryParse(fechaDesde, out parsed))
                    fecha = parsed;
            }

            var dt = DbErrorLogger.GetRecent(200, correlationId, fecha);
            var lista = new System.Collections.Generic.List<object>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];
                lista.Add(new
                {
                    id     = row["Id"] == DBNull.Value     ? "" : row["Id"].ToString(),
                    fecha  = row["FechaHora"] == DBNull.Value ? "" : Convert.ToDateTime(row["FechaHora"]).ToString("dd/MM/yyyy HH:mm:ss"),
                    corrId = row["CorrelationId"] == DBNull.Value ? "" : row["CorrelationId"].ToString(),
                    tipo   = row["Tipo"] == DBNull.Value   ? "" : row["Tipo"].ToString(),
                    msg    = row["Mensaje"] == DBNull.Value ? "" : row["Mensaje"].ToString(),
                    stack  = row["StackTrace"] == DBNull.Value ? "" : row["StackTrace"].ToString(),
                    url    = row["Url"] == DBNull.Value    ? "" : row["Url"].ToString(),
                    user   = row["Usuario"] == DBNull.Value ? "" : row["Usuario"].ToString(),
                    imp    = row["Importancia"] == DBNull.Value ? "1" : row["Importancia"].ToString()
                });
            }

            return Json(new { ok = true, errores = lista }, JsonRequestBehavior.AllowGet);
        }

        private bool IsAdminUser()
        {
            try
            {
                return Roles.IsUserInRole(User.Identity.Name, "Administrador");
            }
            catch
            {
                // RoleManager puede no estar configurado; en ese caso permitir a cualquier usuario autenticado
                return User.Identity.IsAuthenticated;
            }
        }

        /// <summary>
        /// SimpleMembershipProvider.GetUser devuelve siempre IsApproved=true e IsLockedOut=false en el objeto MembershipUser.
        /// El estado de cuenta aprobada está en webpages_Membership.IsConfirmed (expuesto como WebSecurity.IsConfirmed).
        /// </summary>
        private static void TryPopulateMembershipDisplayForAdmin(string userName, out bool isApproved, out bool isLockedOut)
        {
            isApproved = true;
            isLockedOut = false;
            if (string.IsNullOrWhiteSpace(userName))
                return;

            try
            {
                if (Membership.Provider is SimpleMembershipProvider && WebSecurity.Initialized)
                {
                    isApproved = WebSecurity.IsConfirmed(userName);
                    try
                    {
                        int failures = WebSecurity.GetPasswordFailuresSinceLastSuccess(userName);
                        int max = Membership.MaxInvalidPasswordAttempts;
                        if (max > 0 && failures >= max)
                            isLockedOut = true;
                    }
                    catch { }
                    return;
                }

                MembershipUser mu = Membership.GetUser(userName, false);
                if (mu != null)
                {
                    isApproved = mu.IsApproved;
                    isLockedOut = mu.IsLockedOut;
                }
            }
            catch
            {
            }
        }

        [Authorize]
        public JsonResult AuditFactura(string dateFrom, string dateTo)
        {
            if (!IsAdminUser())
                return Json(new List<AuditFactura>(), JsonRequestBehavior.AllowGet);

            var listFactura = new List<AuditFactura>();
            SqlParameter[] _dbParams = new SqlParameter[]
                        {
                            DBHelper.MakeParam("@dateFrom", SqlDbType.VarChar, 0, dateFrom),
                            DBHelper.MakeParam("@dateTo", SqlDbType.VarChar, 0, dateTo)
                        };
            using (SqlDataReader _Reader = DBHelper.ExecuteDataReader("usp_MAT_Admin_AuditoriaFacturas", _dbParams))
            {
                while (_Reader.Read())
                {
                    AuditFactura _item = new Models.AuditFactura();
                    _item.ID = _Reader["ID"].ToString();
                    _item.Accion = _Reader["Accion"].ToString();
                    _item.Descripcion = _Reader["Descripcion"].ToString();
                    _item.Fecha = _Reader["Fecha"].ToString();
                    _item.Cliente = _Reader["Cliente"].ToString();
                    _item.Vendedor = _Reader["Vendedor"].ToString();
                    listFactura.Add(_item);
                }
            }

            // Devolver el array directamente; Json() ya serializa (evita doble JSON string).
            return Json(listFactura, JsonRequestBehavior.AllowGet);
        }
    }
}
