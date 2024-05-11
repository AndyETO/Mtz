using MaritzaBusness.Base;
using MaritzaData.DTO;
using MaritzaData.Filters;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MaritzaData;
using System.Xml.Linq;

namespace MaritzaBusness
{
    public class SearchB : BaseBusnessB
    {

        public StaticPagedList<dtoSearch> GetAllItems(fltSearch filter)
        {

            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string joins = " LEFT JOIN tblProyectImages ON tblProyectImages.ProyectID = tblProyects.ProyectID AND tblProyectImages.IsMainImage = 1 ";
                joins += " LEFT JOIN tblProyectTypes ON tblProyectTypes.ProyectTypeID = tblProyects.ProyectTypeID ";
                joins += " LEFT JOIN tblGenders ON tblGenders.GenderID = tblProyects.GenderID ";
                joins += " LEFT JOIN tblTopics ON tblTopics.TopicID = tblProyects.TopicID ";
                //joins += " LEFT JOIN tblProyectDetails ON tblProyectDetails.ProyectID = tblProyects.ProyectID";
                //Base columns
                string columns = $" , {filter.TableName}.{nameof(filter.ProyectID)}, {filter.TableName}.{nameof(filter.Title)}, {filter.TableName}.{nameof(filter.Description)},  {filter.TableName}.{nameof(filter.Active)}";
                columns += $" , {filter.TableName}.{nameof(tblProyects.Budget)}";
                //columns += $" , tblProyectDetails.ProyectDetailID";
                columns += $" , tblProyectImages.[Url] as UrlImage";
                columns += $" , tblProyectTypes.[Name] as ProyectTypeName, tblGenders.[Name] as GenderName,tblTopics.[Name] as TopicName";
                columns += @" ,
                                 (
                                     SELECT tblCharacteristics.CharacteristicID,tblCharacteristics.[Name] FROM tblProyectCharacteristics
                                     LEFT JOIN tblCharacteristics ON tblCharacteristics.CharacteristicID = tblProyectCharacteristics.CharacteristicID
                                     where 
                                     tblCharacteristics.Active = 1 AND 
                                     tblProyectCharacteristics.Active = 1 AND 
                                     tblProyectCharacteristics.ProyectID = tblProyects.ProyectID 
                                     FOR JSON PATH
                                 ) as lstCharacteristics";
                //Base conditions
                List<object> fields = new List<object>() { nameof(filter.ProyectID), nameof(filter.Title), nameof(filter.Description), nameof(filter.GenderID), nameof(filter.TopicID), nameof(filter.ProyectTypeID), nameof(filter.Active) };
                List<object> values = new List<object>() { filter.ProyectID, filter.Title, filter.Description, filter.GenderID, filter.TopicID, filter.ProyectTypeID, 1 };
                string conditions = GenereteConditions(filter.TableName, fields, values);
                if (filter.MinBudget != null)
                    conditions += $" AND ({filter.TableName}.{nameof(tblProyects.Budget)} >= {filter.MinBudget}) ";
                if (filter.MaxBudget != null)
                    conditions += $" AND ({filter.TableName}.{nameof(tblProyects.Budget)} <= {filter.MaxBudget}) ";
                conditions += $" AND ( ('' LIKE '{filter.SearchTerm}' OR {filter.TableName}.{nameof(filter.Title)} LIKE '%{filter.SearchTerm}%') OR ('' LIKE '{filter.SearchTerm}' OR {filter.TableName}.{nameof(filter.Description)} LIKE '%{filter.SearchTerm}%'))";


                filter.SortingOrder = string.IsNullOrEmpty(filter.SortingOrder) ? filter.dfSorting : filter.SortingOrder;
                string sQuery = GenereteQuery(filter.TableName, filter.SortingOrder, columns, joins, conditions, filter.PageNumber, filter.pageSize);
                var model = dbConnection.Query<dtoSearch>(sQuery).ToList();
                int iTotalItemCount = model?.FirstOrDefault()?.TotalCount ?? 0;
                return new StaticPagedList<dtoSearch>(model, filter.PageNumber, filter.pageSize, iTotalItemCount);
            }

        }

        public dtoSearch GetProyectDetails(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(connection))
            {
                string query = @"
                    SELECT 
                    tblProyects.*,
                    tblTopics.[Name] as TopicName,
                    tblProyectTypes.[Name] as ProyectTypeName,
                    tblGenders.[Name] as GenderName,
	                tblProyectImages.[Url] as UrlImage
                    ,(
                        SELECT tblCharacteristics.CharacteristicID,tblCharacteristics.[Name] FROM tblProyectCharacteristics
                        LEFT JOIN tblCharacteristics ON tblCharacteristics.CharacteristicID = tblProyectCharacteristics.CharacteristicID
                        where 
                        tblCharacteristics.Active = 1 AND 
                        tblProyectCharacteristics.Active = 1 AND 
                        tblProyectCharacteristics.ProyectID = tblProyects.ProyectID 
                        FOR JSON PATH
                    ) as lstCharacteristics
                    , tblProyectDetails.ProyectDetailID
                FROM tblProyects
                   LEFT JOIN tblGenders ON tblGenders.GenderID = tblProyects.GenderID
                   LEFT JOIN tblTopics ON tblTopics.TopicID = tblProyects.TopicID
                   LEFT JOIN tblProyectTypes ON tblProyectTypes.ProyectTypeID = tblProyects.ProyectTypeID
                   LEFT JOIN tblProyectImages ON tblProyectImages.ProyectID = tblProyects.ProyectID AND tblProyectImages.IsMainImage = 1 
                   LEFT JOIN tblProyectDetails ON tblProyectDetails.ProyectID = tblProyects.ProyectID
                WHERE 
                    tblProyects.Active = 1 
	                AND tblProyectImages.Active = 1 
                    AND tblProyects.ProyectID = @id";
                var model = dbConnection.Query<dtoSearch>(query, new { id }).FirstOrDefault();

                return model;
            }
        }

    }
}
