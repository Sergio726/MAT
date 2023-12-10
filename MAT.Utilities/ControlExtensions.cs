using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;


namespace MAT.Utilities
{
    public static class ControlExtensions
    {
        public static string RenderToString(this System.Web.UI.Control source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            using (StringWriter sr = new StringWriter(CultureInfo.InvariantCulture))
            {
                source.RenderControl(new HtmlTextWriter(sr));
                return sr.ToString();
            };
        }
    }

    public class NavigationItem<T>
    {
        readonly T _value;
        readonly bool _isFirst;
        readonly bool _isLast;
        readonly bool  _isEven;

        internal NavigationItem(T value, bool isFirst, bool isLast, bool isEven)
        {
            _value = value;
            _isFirst = isFirst;
            _isLast = isLast;
        }

        public T Value { get { return _value; } }
        public bool IsFirst { get { return IsFirst1; } }
        public bool IsLast { get { return _isLast; } }
        public bool IsEven { get { return _isEven; } }
        public bool IsOdd { get { return !_isEven; } }

        public bool IsFirst1
        {
            get
            {
                return IsFirst2;
            }
        }

        public bool IsFirst2
        {
            get
            {
                return IsFirst3;
            }
        }

        public bool IsFirst3
        {
            get
            {
                return _isFirst;
            }
        }

        public bool IsFirst4
        {
            get
            {
                return _isFirst;
            }
        }
    }

    public static class CollectionNavigation
    {
        public static IEnumerable<NavigationItem<T>> ToNavigable<T>(this IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            return ToNavigableCore(collection);
        }

        private static IEnumerable<NavigationItem<T>> ToNavigableCore<T>(IEnumerable<T> collection)
        {
            using (var lEnumerator = collection.GetEnumerator())
            {
                if (lEnumerator.MoveNext())
                {
                    T lCurrent = lEnumerator.Current;
                    bool lIsFirst = true, lIsEven = true;

                    while (lEnumerator.MoveNext())
                    {
                        yield return new NavigationItem<T>(lCurrent, lIsFirst, false, lIsEven);
                        lIsFirst = false;
                        lIsEven = !lIsEven;
                        lCurrent = lEnumerator.Current;
                    }

                    yield return new NavigationItem<T>(lCurrent, lIsFirst, true, lIsEven);
                }
            }
        }
    }
}
