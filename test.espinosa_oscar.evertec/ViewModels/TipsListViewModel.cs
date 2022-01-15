using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using test.data_access.evertec;
using test.espinosa_oscar.evertec.Views;
using test.services.evertec;
using Xamarin.Forms;

namespace test.espinosa_oscar.evertec.ViewModels
{
    public class TipsListViewModel : BaseViewModel
    {
        #region Global variables
        public ICommand IDetails { set; get; }
        public ICommand IDelete { set; get; }
        public ICommand INew { set; get; }
        public INavigation Navigation { get; set; }
        TipService tipService = new TipService();
        #endregion Global variables

        #region properties
        private ObservableCollection<Tip> _tips;

        /// <summary>
        /// Lista de tips consultados de la DB
        /// </summary>
        public ObservableCollection<Tip> Tips
        {
            get { return _tips; }
            set { _tips = value; OnPropertyChanged(); }
        }
        #endregion properties

        #region constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigation">Navigation object</param>
        public TipsListViewModel(INavigation navigation)
        {
            Navigation = navigation;
            IDetails = new Command(DetailsTip);
            IDelete = new Command(DeleteTip);
            INew = new Command<Type>(async (pageType) => await NewTip(pageType));

            MessagingCenter.Subscribe<NewUpdTipViewModel>
                (this, "Listview", OnInsertUpdateTip);

            Tips = new ObservableCollection<Tip>();
            getTipsItems();
        }
        #endregion constructor

        #region metodos y/o funciones
        private void OnInsertUpdateTip(NewUpdTipViewModel sender)
        {
            getTipsItems();
        }

        /// <summary>
        /// Consulta los tips registrados en la base de datos
        /// </summary>
        public async void getTipsItems()
        {
            //Se invoca la operacion de consultar tips
            var response = await tipService.GetAllTips();

            //Se evalua el resultado
            if (response.statusCode == 0)
            {
                Tips = new ObservableCollection<Tip>(response.result);
            }
            else
            {
                Tips = new ObservableCollection<Tip>();
            }
        }

        /// <summary>
        /// Operacion para navegar a la pagina de creacipn de tip
        /// </summary>
        /// <param name="pageType">Nombre de la vista a navegar</param>
        /// <returns></returns>
        async Task NewTip(Type pageType)
        {
            var page = (Page)Activator.CreateInstance(pageType);

            page.BindingContext = new NewUpdTipViewModel()
            {
                TipItem = new Tip() { Create_Date = DateTime.Now, Autor = "Oscar Espinosa"},
                CreateTip = true
            };

            await Navigation.PushAsync(page);
        }        

        /// <summary>
        /// Operacion para visualizar el detalle de un tip
        /// </summary>
        /// <param name="item">Tip a visualizar</param>
        private void DetailsTip(object item)
        {
            Tip tip = (Tip)item;
            var page = (Page)Activator.CreateInstance(typeof(TipView));
            page.BindingContext = new TipViewModel()
            {
                TipItem = tip
            };
            Navigation.PushAsync(page);
        }

        /// <summary>
        /// Operacion para eliminar un tip de la DB
        /// </summary>
        /// <param name="item">Tip a eliminar</param>
        private async void DeleteTip(object item)
        {
            Tip tip = (Tip)item;
            //Se invoca la operacion de eliminar tip en la DB
            var res = await Application.Current.MainPage.DisplayAlert("Confirmación", "¿Deseas borrar este tip?", "Ok", "Cancel");

            //Se evalua el resultado
            if (res)
            {
                var response = await tipService.DeleteTip(tip);
                if (response.statusCode == 0)
                {
                    getTipsItems();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.statusDesc, "Ok");
                }
            }
        }
        #endregion metodos y/o funciones
    }
}
