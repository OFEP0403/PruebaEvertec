using System;
using System.Threading.Tasks;
using System.Windows.Input;
using test.data_access.evertec;
using test.espinosa_oscar.evertec.Views;
using Xamarin.Forms;

namespace test.espinosa_oscar.evertec.ViewModels
{
    public class TipViewModel : BaseViewModel
    {
        #region global variables
        public ICommand UpdateCommand { private set; get; }
        #endregion global variables

        #region properties
        private Tip _tip;
        /// <summary>
        /// Objeto Tip a visualizar
        /// </summary>
        public Tip TipItem
        {
            get { return _tip; }
            set { _tip = value; OnPropertyChanged(); }
        }
        #endregion properties

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TipViewModel()
        {
            UpdateCommand = new Command(UpdateTip);

            MessagingCenter.Subscribe<NewUpdTipViewModel, Tip>
                (this, "TipDetail", OnInsertUpdateTip);
        }
        #endregion constructor

        #region metodos y/o funciones
        private void OnInsertUpdateTip(NewUpdTipViewModel sender, Tip tip)
        {
            TipItem = tip;
        }

        /// <summary>
        /// Operacion para iniciar la modificacion de algun tip
        /// </summary>
        private void UpdateTip()
        {
            try
            {
                //Navegación a la pagina de edición del tip
                var page = (Page)Activator.CreateInstance(typeof(NewUpdTipView));
                page.BindingContext = new NewUpdTipViewModel()
                {
                    TipItem = TipItem,
                    CreateTip = false
                };
                Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch (Exception)
            {
                //Almacenar Error
            }
        }
        #endregion metodos y/o funciones
    }
}
