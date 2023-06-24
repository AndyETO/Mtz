using MaritzaBusness.Base;
using MaritzaData;
using MaritzaData.ConfigClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaBusness
{
    public class UsersB : BaseBusnessB
    {
        public Response Create(tblUsers model)
        {
            Response response = new Response();

            try
            {
                db.tblUsers.Attach(model);
                db.Entry(model).State=EntityState.Added;
                db.SaveChanges();
                response.Result = Result.Ok;
            }catch (Exception ex)
            {
                response.Result = Result.Error;
                response.data = ex.Message;
            }
            return response;
        }

        public Response Delete(tblUsers model)
        {
            Response response = new Response();
            try
            {
                db.tblUsers.Attach(model);
                db.Entry(model).State=EntityState.Deleted;
                db.SaveChanges();
                response.Result= Result.Ok;
            }catch (Exception ex)
            {
                response.Result = Result.Error;
                response.data = ex.Message;
            }
            return response;
        }

        public Response Update(tblUsers model)
        {

            Response response = new Response();
            try
            {
                db.tblUsers.Attach(model);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                response.Result= Result.Ok;
            }catch (Exception ex)
            {
                response.Result = Result.Error;
                response.data = ex.Message;
            }
            return response;
        }
    }
}
