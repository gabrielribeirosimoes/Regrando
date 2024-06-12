using MauiApp1.ViewModels.Receitas;

namespace MauiApp1.Views.Receitas;

public partial class CadastroReceitaView : ContentPage
{
    private CadastroReceitaViewModel cadViewModel;

    public CadastroReceitaView()
	{
		InitializeComponent();
        cadViewModel = new CadastroReceitaViewModel();
        BindingContext = cadViewModel;
        Title = "Nova Receita";
    }
}