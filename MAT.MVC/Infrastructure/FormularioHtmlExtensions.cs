using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Etiquetas de formulario que toman el texto de [Display] y marcan con * solo los campos con [Required],
    /// para que el asterisco no se desalinee de la validación real.
    /// </summary>
    public static class FormularioHtmlExtensions
    {
        /// <param name="idCampo">Id del control cuando no es el generado por MVC (por ejemplo, "ddLocalidad").</param>
        public static MvcHtmlString EtiquetaPara<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, string idCampo = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var nombre = ExpressionHelper.GetExpressionText(expression);
            var id = idCampo ?? html.ViewData.TemplateInfo.GetFullHtmlFieldId(nombre);
            var texto = metadata.DisplayName ?? metadata.PropertyName ?? nombre;

            var label = new TagBuilder("label");
            label.AddCssClass("modern-form-label");
            label.Attributes["for"] = id;
            label.InnerHtml = HttpUtility.HtmlEncode(texto);
            if (EsObligatorio(metadata))
                label.InnerHtml += "<span class=\"field-required\" aria-hidden=\"true\"> *</span>";
            return MvcHtmlString.Create(label.ToString());
        }

        private static bool EsObligatorio(ModelMetadata metadata)
        {
            if (metadata.ContainerType == null || metadata.PropertyName == null) return false;
            var propiedad = metadata.ContainerType.GetProperty(metadata.PropertyName);
            return propiedad != null && propiedad.GetCustomAttributes(typeof(RequiredAttribute), true).Any();
        }
    }
}
