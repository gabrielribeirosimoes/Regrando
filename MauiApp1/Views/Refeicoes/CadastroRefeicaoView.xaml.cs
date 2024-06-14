using MauiApp1.ViewModels.Refeicoes;

namespace MauiApp1.Views.Refeicoes;

public partial class CadastroRefeicaoView : ContentPage
{
	private CadastroRefeicaoViewModel cadastroRefeicaoViewModel;
	public CadastroRefeicaoView()
	{
		InitializeComponent();
		cadastroRefeicaoViewModel = new CadastroRefeicaoViewModel();
		BindingContext = cadastroRefeicaoViewModel;
		Title = "Nova Refeição";
	}
}