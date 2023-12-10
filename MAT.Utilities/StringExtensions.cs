using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MAT.Utilities
{
    public static class StringExtensions
    {

        #region Métodos públicos

        /// <summary>
        /// Recupera un diccionario a partir de un string
        /// </summary>
        /// <param name="source">String para ser pasado a diccionario.</param>
        /// <returns>Diccionario resultante</returns>
        public static NameValueCollection GetDictionary(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException("source");

            return HttpUtility.ParseQueryString(source);
        }

        /// <summary>
        /// Sustituye caracteres acentuados por su correspondiente HTML
        /// </summary>
        /// <param name="source">String para ser tratado</param>
        /// <returns>String convertido.</returns>
        public static string ParseAcentos(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException("source");

            source = source.Replace("á", "&aacute;");
            source = source.Replace("é", "&eacute;");
            source = source.Replace("í", "&iacute;");
            source = source.Replace("ó", "&oacute;");
            source = source.Replace("ú", "&uacute;");

            source = source.Replace("Á", "&Aacute;");
            source = source.Replace("É", "&Eacute;");
            source = source.Replace("Í", "&Iacute;");
            source = source.Replace("Ó", "&Oacute;");
            source = source.Replace("Ú", "&Uacute;");

            source = source.Replace("ñ", "&ntilde;");
            source = source.Replace("Ñ", "&Ntilde;");
            return source;
        }

        /// <summary>
        /// Sustituye caracteres acentuados en HTML por sus caracteres acentuados
        /// </summary>
        /// <param name="source">String para ser tratado.</param>
        /// <returns>String convertido.</returns>
        public static string DecodeAcute(this string source)
        {
            if (string.IsNullOrEmpty(source))
                throw new ArgumentNullException("source");

            source = source.Replace("&aacute;", "á");
            source = source.Replace("&eacute;", "é");
            source = source.Replace("&iacute;", "í");
            source = source.Replace("&oacute;", "ó");
            source = source.Replace("&uacute;", "ú");

            source = source.Replace("&Aacute;", "Á");
            source = source.Replace("&Eacute;", "É");
            source = source.Replace("&Iacute;", "Í");
            source = source.Replace("&Oacute;", "Ó");
            source = source.Replace("&Uacute;", "Ú");

            //grave
            source = source.Replace("&agrave;", "à");
            source = source.Replace("&egrave;", "è");
            source = source.Replace("&igrave;", "ì");
            source = source.Replace("&ograve;", "ò");
            source = source.Replace("&ugrave;", "ù");
            source = source.Replace("&Agrave;", "À");
            source = source.Replace("&Egrave;", "È");
            source = source.Replace("&Igrave;", "Ì");
            source = source.Replace("&Ograve;", "Ò");
            source = source.Replace("&Ugrave;", "Ù");

            //tilde
            source = source.Replace("&atilde;", "ã");
            source = source.Replace("&Atilde;", "Ã");
            source = source.Replace("&otilde;", "õ");
            source = source.Replace("&Otilde;", "Õ");
            source = source.Replace("&ntilde;", "ñ");
            source = source.Replace("&Ntilde;", "Ñ");

            //uml
            source = source.Replace("&auml;", "ä");
            source = source.Replace("&Auml;", "Ä");
            source = source.Replace("&euml;", "ë");
            source = source.Replace("&Euml;", "Ë");
            source = source.Replace("&iuml;", "ï");
            source = source.Replace("&Iuml;", "Ï");
            source = source.Replace("&ouml;", "ö");
            source = source.Replace("&Ouml;", "Ö");
            source = source.Replace("&uuml;", "ü");
            source = source.Replace("&Uuml;", "Ü");
            source = source.Replace("&yuml;", "ÿ");

            //circ
            source = source.Replace("&acirc;", "â");
            source = source.Replace("&ecirc;", "ê");
            source = source.Replace("&icirc;", "î");
            source = source.Replace("&ocirc;", "ô");
            source = source.Replace("&ucirc;", "û");
            source = source.Replace("&Acirc;", "Â");
            source = source.Replace("&Ecirc;", "Ê");
            source = source.Replace("&Icirc;", "Î");
            source = source.Replace("&Ocirc;", "Ô");
            source = source.Replace("&Ucirc;", "Ô");

            //OTROS
            source = source.Replace("&ccedil;", "ç");
            source = source.Replace("&Ccedil;", "Ç");
            source = source.Replace("&aelig;", "æ");
            source = source.Replace("&AElig;", "Æ");
            source = source.Replace("&aring;", "å");
            source = source.Replace("&Aring;", "Å");
            source = source.Replace("&oslash;", "ø");
            source = source.Replace("&Oslash;", "Ø");
            source = source.Replace("&ordm;", "º");
            source = source.Replace("&ordf;", "ª");
            return source;
        }

        /// <summary>
        /// Corrige un texto en formato HTML
        /// </summary>
        /// <param name="source">Texto en HTML</param>
        /// <param name="replaceSpecialChars">Si es verdadero, reemplaza caracteres especiales.</param>
        /// <returns>Texto HTML formateado</returns>
        public static string StripHtml(this string source, bool replaceSpecialChars)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            try
            {
                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                string result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = Regex.Replace(result,
                                       @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = Regex.Replace(result,
                                       @"<( )*head([^>])*>", "<head>",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       @"(<( )*(/)( )*head( )*>)", "</head>",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       "(<head>).*(</head>)", string.Empty,
                                       RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = Regex.Replace(result,
                                       @"<( )*script([^>])*>", "<script>",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       @"(<( )*(/)( )*script( )*>)", "</script>",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       @"(<script>).*(</script>)", string.Empty,
                                       RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = Regex.Replace(result,
                                       @"<( )*style([^>])*>", "<style>",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       @"(<( )*(/)( )*style( )*>)", "</style>",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       "(<style>).*(</style>)", string.Empty,
                                       RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = Regex.Replace(result,
                                       @"<( )*td([^>])*>", "\t",
                                       RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = Regex.Replace(result,
                                       @"<( )*br( )*>", "\r",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       @"<( )*li( )*>", "\r",
                                       RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = Regex.Replace(result,
                                       @"<( )*div([^>])*>", "\r\r",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       @"<( )*tr([^>])*>", "\r\r",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       @"<( )*p([^>])*>", "\r\r",
                                       RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = Regex.Replace(result,
                                       @"<[^>]*>", string.Empty,
                                       RegexOptions.IgnoreCase);

                // Remove embed tags like <embed>, <video>, etc.
                result = Regex.Replace(result,
                                       @"<( )*embed( )*>", string.Empty,
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       @"<( )*video( )*>", string.Empty,
                                       RegexOptions.IgnoreCase);

                if (replaceSpecialChars)
                {
                    // replace special characters:
                    result = Regex.Replace(result,
                                           @" ", " ",
                                           RegexOptions.IgnoreCase);

                    result = Regex.Replace(result,
                                           @"&bull;", " * ",
                                           RegexOptions.IgnoreCase);
                    result = Regex.Replace(result,
                                           @"&lsaquo;", "<",
                                           RegexOptions.IgnoreCase);
                    result = Regex.Replace(result,
                                           @"&rsaquo;", ">",
                                           RegexOptions.IgnoreCase);
                    result = Regex.Replace(result,
                                           @"&trade;", "(tm)",
                                           RegexOptions.IgnoreCase);
                    result = Regex.Replace(result,
                                           @"&frasl;", "/",
                                           RegexOptions.IgnoreCase);
                    result = Regex.Replace(result,
                                           @"&lt;", "<",
                                           RegexOptions.IgnoreCase);
                    result = Regex.Replace(result,
                                           @"&gt;", ">",
                                           RegexOptions.IgnoreCase);
                    result = Regex.Replace(result,
                                           @"&copy;", "(c)",
                                           RegexOptions.IgnoreCase);
                    result = Regex.Replace(result,
                                           @"&reg;", "(r)",
                                           RegexOptions.IgnoreCase);
                    // Remove all others. More can be added, see
                    result = Regex.Replace(result,
                                           @"&(.{2,6});", string.Empty,
                                           RegexOptions.IgnoreCase);
                }

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = Regex.Replace(result,
                                       "(\r)( )+(\r)", "\r\r",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       "(\t)( )+(\t)", "\t\t",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       "(\t)( )+(\r)", "\t\r",
                                       RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                                       "(\r)( )+(\t)", "\r\t",
                                       RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = Regex.Replace(result,
                                       "(\r)(\t)+(\r)", "\r\r",
                                       RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = Regex.Replace(result,
                                       "(\r)(\t)+", "\r\t",
                                       RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch (ArgumentNullException)
            {
                return source;
            }
            catch (ArgumentOutOfRangeException)
            {
                return source;
            }
            catch (ArgumentException)
            {
                return source;
            }
        }


        #endregion
    }
}
