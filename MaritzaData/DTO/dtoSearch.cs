using MaritzaData.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaritzaData.DTO
{
    public class dtoSearch : dtoBase
    {
        public int ProyectID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }

        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public int ProyectTypeID { get; set; }
        public string ProyectTypeName { get; set; }
        public decimal? Budget { get; set; }
        public int ProyectDetailID { get; set; }



        public string lstCharacteristics { get; set; }

        public List<dtoCharacteristics> lstCharacteristicsModel
        {
            get
            {
                if (lstCharacteristics != null)
                {
                    try
                    {
                        var List = JsonConvert.DeserializeObject<List<dtoCharacteristics>>(lstCharacteristics);
                        if (List != null)
                        {
                            return List;
                        }
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                return null;

            }
        }

        public List<dtoProyectComments> lstComments { get; set; }

        public tblProyectLikes tblProyectLikes { get; set; }
    }
}
