using System;
using System.IO;
using test.data_access.evertec;

namespace test.services.evertec
{
    public partial class DBContext
    {
        private static DatabaseContext dbcontext;

        /// <summary>
        /// Contexto de la base de datos
        /// </summary>
        public static DatabaseContext Context
        {
            get
            {
                if (dbcontext == null)
                {
                    var dbPath = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "TipsDB.db3");

                    dbcontext = new DatabaseContext(dbPath);
                }

                return dbcontext;
            }
        }
    }
}
