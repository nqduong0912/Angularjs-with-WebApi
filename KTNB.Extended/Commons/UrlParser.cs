using System.Collections.Generic;
using System.Collections.Specialized;

namespace KTNB.Extended.Commons
{
    public class UrlParser
    {
        private readonly NameValueCollection _nameValues = new NameValueCollection();
        private readonly string _urlParameters = "";
        private string _urlAnchor = "";

        public UrlParser(string urlParameters)
        {
            _urlParameters = urlParameters;
            ParseURLParameters();
        }

        public string Anchor
        {
            get { return _urlAnchor; }
        }

        public bool HasAnchor
        {
            get { return (_urlAnchor != ""); }
        }

        public NameValueCollection Parameters
        {
            get { return _nameValues; }
        }

        public int Count
        {
            get { return _nameValues.Count; }
        }

        public string this[string name]
        {
            get { return _nameValues[name]; }
        }

        public string this[int index]
        {
            get { return _nameValues[index]; }
        }

        private void ParseURLParameters()
        {
            string urlTemp = _urlParameters;
            int index;

            // get the url end anchor (#blah) if there is one...
            _urlAnchor = "";
            index = urlTemp.LastIndexOf('#');

            if (index > 0)
            {
                // there's an anchor
                _urlAnchor = urlTemp.Substring(index + 1);
                // remove the anchor from the url...
                urlTemp = urlTemp.Remove(index);
            }

            _nameValues.Clear();
            string[] arrarUrl = urlTemp.Split('?');
            if (arrarUrl.Length > 1)
            {
                urlTemp = arrarUrl[1];
            }

            string[] arrayPairs = urlTemp.Split(new[] { '&' });

            foreach (string tValue in arrayPairs)
            {
                if (tValue.Trim().Length > 0)
                {
                    // parse...
                    string[] nvalue = tValue.Trim().Split(new[] { '=' });
                    if (nvalue.Length == 1)
                        _nameValues.Add(nvalue[0], string.Empty);
                    else if (nvalue.Length > 1)
                        _nameValues.Add(nvalue[0], nvalue[1]);
                }
            }
        }

        public string CreateQueryString(Dictionary<string, string> includeValues)
        {
            string queryString = (_urlParameters.Split('?').Length > 1) ? _urlParameters.Split('?')[0] : string.Empty;
            bool bFirst = true;
            if (includeValues != null)
            {
                for (int i = 0; i < _nameValues.Count; i++)
                {
                    string key = _nameValues.Keys[i];
                    string value = _nameValues[i];
                    if (KeyInsideArray(includeValues, key))
                    {
                        if (string.IsNullOrEmpty(includeValues[key]))
                            break;
                    }
                    if (bFirst)
                    {
                        queryString += "?";
                        bFirst = false;
                    }
                    else
                    {
                        queryString += "&";
                    }

                    if (KeyInsideArray(includeValues, key))
                    {
                        queryString += key + "=" + includeValues[key];
                        includeValues.Remove(key);
                    }
                    else
                    {
                        queryString += key + "=" + value;
                    }
                }

                foreach (var includeValue in includeValues)
                {
                    if (!string.IsNullOrEmpty(includeValue.Value))
                    {
                        queryString += (_nameValues.Count == 0) ? "?" : "&";
                        queryString += includeValue.Key + "=" + includeValue.Value;
                    }
                }
            }

            return queryString;
        }

        public string CreateQueryString(Dictionary<string, string> includeValues, string anchorName)
        {
            return CreateQueryString(includeValues) + (string.IsNullOrEmpty(anchorName) ? string.Empty : "#" + anchorName);
        }

        public string CreateQueryString(string[] excludeValues)
        {
            string queryString = (_urlParameters.Split('?').Length > 1) ? _urlParameters.Split('?')[0] : string.Empty;
            bool bFirst = true;
            if (excludeValues != null)
            {
                for (int i = 0; i < _nameValues.Count; i++)
                {
                    string key = _nameValues.Keys[i];
                    string value = _nameValues[i];
                    if (!KeyInsideArray(excludeValues, key))
                    {
                        if (bFirst)
                        {
                            queryString += "?";
                            bFirst = false;
                        }

                        else queryString += "&";
                        queryString += key + "=" + value;
                    }
                }
            }
            return queryString;
        }

        private bool KeyInsideArray(string[] array, string key)
        {
            foreach (string tmp in array)
            {
                if (tmp.Equals(key)) return true;
            }

            return false;
        }

        private bool KeyInsideArray(Dictionary<string, string> array, string key)
        {
            foreach (var tmp in array)
            {
                if (tmp.Key.Equals(key)) return true;
            }

            return false;
        }
    }
}