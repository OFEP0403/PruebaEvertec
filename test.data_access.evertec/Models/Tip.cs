using System;
using SQLite;

namespace test.data_access.evertec
{
    public class Tip
    {
        /// <summary>
        /// Id del registro
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Fecha de creación del tip
        /// </summary>
        public DateTime Create_Date { get; set; }

        /// <summary>
        /// Fecha de modificacion del tip
        /// </summary>
        public DateTime Update_Date { get; set; }

        /// <summary>
        /// Autor del tip
        /// </summary>
        public string Autor { get; set; }

        /// <summary>
        /// Titulo del tip
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Descripción del tip
        /// </summary>
        public string Description { get; set; }
    }
}
