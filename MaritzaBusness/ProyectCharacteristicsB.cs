﻿using Dapper;
using MaritzaBusness.Base;
using MaritzaData;
using MaritzaData.ConfigClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaBusness
{
    public class ProyectCharacteristicsB : BaseBusnessB
    {

        public Response Create(tblProyectCharacteristics model)
        {

            Response response = new Response();
            try
            {
                db.tblProyectCharacteristics.Attach(model);
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                response.Result = Result.Ok;
            }
            catch (Exception ex)
            {
                response.Result = Result.Exception;
                response.data = ex.Message;
            }
            return response;
        }

        public Response Update(tblProyectCharacteristics model)
        {
            Response response = new Response();

            try
            {
                db.tblProyectCharacteristics.Attach(model);
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Result = Result.Exception;
                response.data = ex.Message;
            }
            return response;
        }

        public List<tblProyectCharacteristics> GetListByProyectID(int ID)
        {

            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"
                SELECT 
                    tblProyectCharacteristics.*,
                    tblCharacteristics.[Name] as CharacteristicName
                FROM 
                    tblProyectCharacteristics 
                LEFT JOIN tblCharacteristics ON tblCharacteristics.CharacteristicID = tblProyectCharacteristics.CharacteristicID 
                WHERE 
                    tblProyectCharacteristics.Active = 1 AND 
                    tblProyectCharacteristics.ProyectID = @ID";
                var lstModel = dbConnection.Query<tblProyectCharacteristics>(Query, new { ID }).ToList();
                return lstModel;
            }

        }

        public bool CheckIfExistsCharacteristicByCharacteristicIDAndProyectID(int CharacteristicID, int ProyectID)
        {

            using (IDbConnection Connection = new SqlConnection(connection))
            {
                string Query = @"
                IF EXISTS(
                SELECT 
                      [ProyectID]
                      ,[CharacteristicID]
                  FROM [MaritzasSeamsDB].[dbo].[tblProyectCharacteristics]
                  WHERE Active = 1 AND ProyectID = @ProyectID AND CharacteristicID = @CharacteristicID
                )
                    SELECT 'TRUE'
                  ELSE
                    SELECT 'FALSE'";
                var value = Connection.Query<bool>(Query, new { ProyectID, CharacteristicID }).FirstOrDefault();
                return value;
            }

        }

        public tblProyectCharacteristics GetByCharacteristicIDAndProyectID(int CharacteristicID, int ProyectID)
        {

            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string Query = @"
                SELECT 
                    tblProyectCharacteristics.*,
                    tblCharacteristics.[Name] as CharacteristicName
                FROM 
                    tblProyectCharacteristics 
                    LEFT JOIN tblCharacteristics ON tblCharacteristics.CharacteristicID = tblProyectCharacteristics.CharacteristicID 
                WHERE tblProyectCharacteristics.Active = 1 AND ProyectID = @ProyectID AND tblProyectCharacteristics.CharacteristicID = @CharacteristicID";
                var lstModel = dbConnection.Query<tblProyectCharacteristics>(Query, new { ProyectID, CharacteristicID }).FirstOrDefault();
                return lstModel;
            }

        }



    }
}
