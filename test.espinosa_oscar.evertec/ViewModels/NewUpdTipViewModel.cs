using System;
using System.Threading.Tasks;
using System.Windows.Input;
using test.data_access.evertec;
using test.services.evertec;
using Xamarin.Forms;

namespace test.espinosa_oscar.evertec.ViewModels
{
    public class NewUpdTipViewModel : BaseViewModel
    {
        #region Global variables
        public ICommand CreateUpdateCommand { private set; get; }
        TipService tipService = new TipService();
        #endregion Global variables

        #region properties
        private Tip _tip;
        /// <summary>
        /// Objeto Tip a modificar o insertar
        /// </summary>
        public Tip TipItem
        {
            get { return _tip; }
            set { _tip = value; OnPropertyChanged(); }
        }

        public bool _create;
        /// <summary>
        /// Bandera para especificar si se debe insertar o modificar
        /// </summary>
        public bool CreateTip
        {
            get { return _create; }
            set { _create = value; OnPropertyChanged(); }
        }
        #endregion properties

        #region constructor
        /// <summary>
        /// constructor
        /// </summary>
        public NewUpdTipViewModel()
        {
            CreateUpdateCommand = new Command(async () => await CreateUpdateTip());
        }
        #endregion constructor

        #region metodos y/o funciones
        /// <summary>
        /// Operación de crear o modificar un Tip
        /// </summary>
        /// <returns></returns>
        async Task CreateUpdateTip()
        {
            try
            {
                //Se invoca la operacion de insertar o modificar en la DB
                var response = await tipService.InsertUpdateTip(TipItem, CreateTip);

                //Se evalua el resultado de la operacion
                if (response.statusCode == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Transacción exitosa", response.statusDesc, "Ok");                    
                    Actualizar_TipListViewModel();
                    if (CreateTip)
                        ClearAll();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.statusDesc, "Ok");
                }
                
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ha ocurrido un error inesperado, por favor vuelve a intentarlo.", "Ok");
            }
        }

        private void Actualizar_TipListViewModel()
        {
            //Send messages
            MessagingCenter.Send(this, "Listview");
            MessagingCenter.Send(this, "TipDetail", TipItem);
        }

        /// <summary>
        /// Reinicia los valores del objeto Tip
        /// </summary>
        private void ClearAll()
        {
            TipItem = new Tip()
            {
                Create_Date = DateTime.Now,
                Autor = "Oscar Espinosa"
            };
        }
        #endregion metodos y/o funciones
    }
}
