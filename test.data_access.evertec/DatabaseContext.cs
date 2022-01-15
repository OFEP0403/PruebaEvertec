using System.Threading.Tasks;
using System.Collections.Generic;

using SQLite;
namespace test.data_access.evertec
{
    
    public class DatabaseContext
    {
        private readonly SQLiteAsyncConnection connection;

        /// <summary>
        /// Instancia a la base de datos
        /// </summary>
        /// <param name="dbPath">Path para realizar la conexión a la DB</param>
        public DatabaseContext(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<Tip>().Wait();
        }

        /// <summary>
        /// Obtiene los registros de la base de datos
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a devolver</typeparam>
        /// <returns>Lista de objetos</returns>
        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            return await connection.Table<T>().ToListAsync();
        }

        /// <summary>
        /// Obtiene registros especificon basado en una condición
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a devolver</typeparam>
        /// <param name="table">Nombre de la tabla</param>
        /// <param name="condition">Condición para filtar en la consulta</param>
        /// <returns>Lista de objetos</returns>
        public async Task<List<T>> FilterItemsAsync<T>(string table, string condition) where T : new()
        {
            return await connection.QueryAsync<T>($"SELECT * FROM {table} WHERE {condition}");
        }

        /// <summary>
        /// Obtiene un objeto a partir de su id
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a devolver</typeparam>
        /// <param name="id">Identificador del registro</param>
        /// <returns>Objeto generico</returns>
        public async Task<T> GetItemAsync<T>(string id) where T : new()
        {
            return await connection.FindAsync<T>(id);
        }

        /// <summary>
        /// Almacena un registro en la base de datos
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a almacenar</typeparam>
        /// <param name="item">Objeto generico a almacenar</param>
        /// <param name="isInsert">Bandera que especifica si se quiere insertar o modificar</param>
        /// <returns>Entero 1 o 0</returns>
        public async Task<int> SaveItemAsync<T>(T item, bool isInsert) where T : new()
        {
            return (isInsert)
                ? await connection.InsertAsync(item)
                : await connection.UpdateAsync(item);
        }

        /// <summary>
        /// Elimina un registro de la base de datos
        /// </summary>
        /// <typeparam name="T">Tipo de objeto a eliminar</typeparam>
        /// <param name="item">Objeto generico a eliminar</param>
        /// <returns>Entero 1 o 0</returns>
        public async Task<int> DeleteItemAsync<T>(T item) where T : new()
        {
            return await connection.DeleteAsync(item);
        }
    }
    
}
