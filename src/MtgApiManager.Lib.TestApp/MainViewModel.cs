using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.TestApp.Core;

namespace MtgApiManager.Lib.TestApp
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly MtgController _controller;

        [ObservableProperty]
        private string _cardSearchString = null;

        [ObservableProperty]
        private ICard _selectedCard = null;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(FindSelectedCardCommand))]
        private string _selectedCardId = null;

        [ObservableProperty]
        private ISet _selectedSet = null;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GenerateBoosterCommand))]
        [NotifyCanExecuteChangedFor(nameof(FindSelectedSetCommand))]
        private string _selectedSetCode = null;

        [ObservableProperty]
        private string _setSearchString = null;

        public MainViewModel(MtgController controller)
        {
            _controller = controller;

            // Subscribe to controller's IsSearchingChanged event
            _controller.IsSearchingChanged += (s, isSearching) =>
            {
                OnPropertyChanged(nameof(IsLoading));
            };

            CardsCollection = [];
            SetsCollection = [];
            TypesCollection = [];
            GeneratedBoosterCollection = [];
        }

        public bool IsLoading => _controller.IsSearching;

        public ObservableCollection<ICard> CardsCollection { get; }

        public ObservableCollection<ISet> SetsCollection { get; }

        public ObservableCollection<ICard> GeneratedBoosterCollection { get; }

        public ObservableCollection<string> TypesCollection { get; }

        [RelayCommand]
        private async Task CardSearch()
        {
            var cards = await _controller.GetCards(CardSearchString);

            CardsCollection.Clear();
            foreach (var item in cards)
            {
                CardsCollection.Add(item);
            }
        }

        [RelayCommand(CanExecute = nameof(CanFindSelectedCard))]
        private async Task FindSelectedCard()
        {
            var card = await _controller.FindCardById(SelectedCardId);
            SelectedCard = card;
        }

        private bool CanFindSelectedCard() => !string.IsNullOrWhiteSpace(SelectedCardId);

        [RelayCommand(CanExecute = nameof(CanFindSelectedSet))]
        private async Task FindSelectedSet()
        {
            var set = await _controller.FindSetById(SelectedSetCode);
            SelectedSet = set;
        }

        private bool CanFindSelectedSet() => !string.IsNullOrWhiteSpace(SelectedSetCode);

        [RelayCommand(CanExecute = nameof(CanGenerateBooster))]
        private async Task GenerateBooster()
        {
            var cards = await _controller.GenerateBooster(SelectedSetCode);

            GeneratedBoosterCollection.Clear();
            foreach (var item in cards)
            {
                GeneratedBoosterCollection.Add(item);
            }
        }

        private bool CanGenerateBooster() => !string.IsNullOrWhiteSpace(SelectedSetCode);

        [RelayCommand]
        private async Task GetCardFormats()
        {
            var formats = await _controller.GetCardFormats();

            TypesCollection.Clear();
            foreach (var item in formats)
            {
                TypesCollection.Add(item);
            }
        }

        [RelayCommand]
        private async Task GetCardSubTypes()
        {
            var subTypes = await _controller.GetCardSubTypes();

            TypesCollection.Clear();
            foreach (var item in subTypes)
            {
                TypesCollection.Add(item);
            }
        }

        [RelayCommand]
        private async Task GetCardSuperTypes()
        {
            var superTypes = await _controller.GetCardSuperTypes();

            TypesCollection.Clear();
            foreach (var item in superTypes)
            {
                TypesCollection.Add(item);
            }
        }

        [RelayCommand]
        private async Task GetCardTypes()
        {
            var types = await _controller.GetCardTypes();

            TypesCollection.Clear();
            foreach (var item in types)
            {
                TypesCollection.Add(item);
            }
        }


        [RelayCommand]
        private async Task SetSearch()
        {
            var sets = await _controller.GetSets(SetSearchString);

            SetsCollection.Clear();
            foreach (var item in sets)
            {
                SetsCollection.Add(item);
            }
        }
    }
}