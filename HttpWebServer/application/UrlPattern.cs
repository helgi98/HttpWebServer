using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.application
{
    class UrlPattern: IComparable<UrlPattern>
    {
        private String[] _pattern;

        public UrlPattern(String pattern)
        {
            _pattern = pattern.Split('/', '\\').Skip(1).ToArray();
        }

        public int CompareTo(UrlPattern other)
        {
            if (_pattern.Length != other._pattern.Length) return _pattern.Length - _pattern.Length;

            int count1 = 0;
            int count2 = 0;
            for (int i = 0; i < _pattern.Length; ++i)
            {
                if (_pattern[i] == "*") count1++;
                if (other._pattern[i] == "*") count2++;
            }

            return count1 - count2;
        }

        public bool Matches(String url)
        {
            String[] urlElems = url.Split('/', '\\').Skip(1).ToArray();
            if (_pattern.Length != urlElems.Length) return false;

            for (int i = 0; i < _pattern.Length; ++i)
            {
                if (_pattern[i] == "*") continue;
                else if (_pattern[i] != urlElems[i]) return false;
            }
            

            return true;
        }
    }
}
