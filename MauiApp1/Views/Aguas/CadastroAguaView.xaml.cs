using MauiApp1.ViewModels.Aguas;

namespace MauiApp1.Views.Aguas;

public partial class CadastroAguaView : ContentPage
{
	private CadastroAguaViewModel cadViewModel;

	public CadastroAguaView()
	{
		InitializeComponent();
        cadViewModel = new CadastroAguaViewModel();
        BindingContext = cadViewModel;
        Title = "Novo Consumo";
    }
}