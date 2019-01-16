using System;

namespace MySite.DBUtility
{
    public class Utility
    {
        public static Utility GetUtility()
        {
            if(_Utility == null)
            {
                lock(_object)
                {
                    _Utility = new Utility();
                }
            }
            return _Utility;
        }

        private static Utility _Utility = null;

        private static readonly object _object = new object();
    }
}
