using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MAT.Utilities
{
    public static class CustomHtmlHelper
    {
        //public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        //{
        //    ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
        //    IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

        //    IEnumerable<SelectListItem> items =
        //        values.Select(value => new SelectListItem
        //        {
        //            Text = value.ToString(),
        //            Value = value.ToString(),
        //            Selected = value.Equals(metadata.Model)
        //        });

        //    return htmlHelper.DropDownListFor(
        //        expression,
        //        items
        //        );
        //}
    }
}
