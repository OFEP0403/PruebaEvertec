using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.data_access.evertec;
using test.data_access.evertec.Models;

namespace test.services.evertec
{
    public class TipService
    {

        /// <summary>
        /// Inserta o modifica un tip en la base de datos
        /// </summary>
        /// <param name="tip">Objeto de tipo Tip a insertar o modificar</param>
        /// <param name="insert">Bandera que especifica si el registro se debe insertar o modificar</param>
        /// <returns>Response generico con valor bool</returns>
        public async Task<Response<bool>> InsertUpdateTip(Tip tip, bool insert)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                //Valida que la fecha de creacion este asignada
                if (tip.Create_Date == null)
                {
                    response.statusCode = 1001;
                    response.statusDesc = "Te falta seleccionar la fecha de creación.";
                    response.result = false;
                }

                //Valida que el titulo del tip se haya ingresado
                if (string.IsNullOrEmpty(tip.Title))
                {
                    response.statusCode = 1002;
                    response.statusDesc = "TDebes ingresar un titulo para tu tip.";
                    response.result = false;
                }

                //Valida que la descripcion del tip se haya ingresado
                if (string.IsNullOrEmpty(tip.Description))
                {
                    response.statusCode = 1003;
                    response.statusDesc = "Debes ingresar tu tip.";
                    response.result = false;
                }

                //Cuando se va a modificar el item, se asigna la fecha del dia a la propiedad update_date
                if (!insert)
                    tip.Update_Date = DateTime.Now;

                //Se invoca la operacion de salvar registro en la base de datos
                var result = await DBContext.Context.SaveItemAsync<Tip>(tip, insert);
                //Se evalua el resultado
                if (result == 1)
                {
                    response.result = true;
                    response.statusCode = 0;
                    if (insert)
                        response.statusDesc = "Tip registrado exitosamente.";
                    else
                        response.statusDesc = "Tip modificado exitosamente.";
                }
                else
                {
                    response.result = true;
                    response.statusCode = 1000;
                    if (insert)
                        response.statusDesc = "Error al registrar el Tip, por favor vuelve a intentarlo.";
                    else
                        response.statusDesc = "Error al modificar el Tip, por favor vuelve a intentarlo.";
                }
            }catch(Exception ex)
            {
                response.result = true;
                response.statusCode = 1004;
                response.statusDesc = ex.Message;
            }
            return response;
        }


        /// <summary>
        /// Obtiene los tips registrados en la base de datos
        /// </summary>
        /// <returns>Response generico con la lista de tips</returns>
        public async Task<Response<List<Tip>>> GetAllTips()
        {
            Response<List<Tip>> response = new Response<List<Tip>>();
            try
            {
                //Se invoca la operacion que obtiene los registros de tipo Tip
                var items = await DBContext.Context.GetItemsAsync<Tip>();

                //Se evalua el resultado
                if (items?.Count > 0)
                {
                    response.statusCode = 0;
                    response.statusDesc = "Transacción exitosa";
                    response.result = items.OrderByDescending(x => x.Create_Date).ToList();
                }
                else
                {
                    response.statusCode = 1001;
                    response.statusDesc = "No se encontraron registros";
                    response.result = null;
                }
            }
            catch(Exception ex)
            {
                response.statusCode = 1002;
                response.statusDesc = ex.Message;
                response.result = null;
            }
            return response;
        }

        /// <summary>
        /// Elimina un Tip especifico de la base de datos
        /// </summary>
        /// <param name="tip">Objeto de tipo Tip que se quiere eliminar</param>
        /// <returns>Respunse generico con valor bool</returns>
        public async Task<Response<bool>> DeleteTip(Tip tip)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                //Se invoca la operacion que elimina el registro en la base de datos
                var result = await DBContext.Context.DeleteItemAsync(tip);

                //Se evalua el resultado
                if (result == 1)
                {
                    response.statusCode = 0;
                    response.statusDesc = "Registro eliminado exitosamente.";
                    response.result = true;
                }
                else
                {
                    response.statusCode = 1001;
                    response.statusDesc = "Error al eliminar el Tip, por favor vuelve a intentarlo de nuevo.";
                    response.result = false;
                }
            }
            catch (Exception ex)
            {
                response.statusCode = 1002;
                response.statusDesc = ex.Message;
                response.result = false;
            }
            return response;
        }
    }
}
