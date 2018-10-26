using System;
using MySite.Model;
using MySite.DAL;

namespace MySite.BLL
{
    public class DoctorBll
    {
        private DoctorDal _dal = new DoctorDal();
        public List<Doctor> GetList()
        {
            return _dal.GetList();
        } 
    }
}
