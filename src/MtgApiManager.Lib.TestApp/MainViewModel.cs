using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.TestApp.Core;

namespace MtgApiManager.Lib.TestApp
{
    public class MainViewModel : ObservableObject
    {
        private readonly MtgController _controller;
        private RelayCommand _cardSearchCommand;
        private string _cardSearchString = null;
        private RelayCommand _findSelectedCardCommand;
        private RelayCommand _findSelectedSetCommand;
        private RelayCommand _generateBoosterCommand;
        private RelayCommand _getCardFormatsCommand;
        private RelayCommand _getCardSubTypesCommand;
        private RelayCommand _getCardSuperTypesCommand;
        private RelayCommand _getCardTypesCommand;
        private ICard _selectedCard = null;
        private string _selectedCardId = null;
        private ISet _selectedSet = null;
        private string _selectedSetCode = null;
        private RelayCommand _setSearchCommand;
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

        public ObservableCollection<ICard> CardsCollection { get; }

        public RelayCommand CardSearchCommand
        {
            get
            {
                return _cardSearchCommand ??= new RelayCommand(
                    async () =>
                    {
                        var cards = await _controller.GetCards(_cardSearchString);

                        CardsCollection.Clear();
                        foreach (var item in cards)
                        {
                            CardsCollection.Add(item);
                        }
                    });
            }
        }

        public string CardSearchString
        {
            get => _cardSearchString;
            set => SetProperty(ref _cardSearchString, value);
        }

        public RelayCommand FindSelectedCardCommand
        {
            get
            {
                return _findSelectedCardCommand ??= new RelayCommand(
                    async () =>
                    {
                        var card = await _controller.FindCardById(_selectedCardId);
                        SelectedCard = card;
                    },
                    () => !string.IsNullOrWhiteSpace(_selectedCardId));
            }
        }

        public RelayCommand FindSelectedSetCommand =>
            _findSelectedSetCommand ??= new RelayCommand(
                    async () =>
                    {
                        var set = await _controller.FindSetById(_selectedSetCode);
                        SelectedSet = set;
                    },
                    () => !string.IsNullOrWhiteSpace(_selectedSetCode));

        public RelayCommand GenerateBoosterCommand =>
            _generateBoosterCommand ??= new RelayCommand(
                    async () =>
                    {
                        var cards = await _controller.GenerateBooster(_selectedSetCode);

                        GeneratedBoosterCollection.Clear();
                        foreach (var item in cards)
                        {
                            GeneratedBoosterCollection.Add(item);
                        }
                    },
                    () => !string.IsNullOrWhiteSpace(_selectedSetCode));

        public ObservableCollection<ICard> GeneratedBoosterCollection { get; }

        public RelayCommand GetCardFormatsCommand =>
            _getCardFormatsCommand ??= new RelayCommand(
                    async () =>
                    {
                        var formats = await _controller.GetCardFormats();

                        TypesCollection.Clear();
                        foreach (var item in formats)
                        {
                            TypesCollection.Add(item);
                        }
                    });

        public RelayCommand GetCardSubTypesCommand =>
            _getCardSubTypesCommand ??= new RelayCommand(
                    async () =>
                    {
                        var subTypes = await _controller.GetCardSubTypes();

                        TypesCollection.Clear();
                        foreach (var item in subTypes)
                        {
                            TypesCollection.Add(item);
                        }
                    });

        public RelayCommand GetCardSuperTypesCommand =>
            _getCardSuperTypesCommand ??= new RelayCommand(
                    async () =>
                    {
                        var superTypes = await _controller.GetCardSuperTypes();

                        TypesCollection.Clear();
                        foreach (var item in superTypes)
                        {
                            TypesCollection.Add(item);
                        }
                    });

        public RelayCommand GetCardTypesCommand =>
            _getCardTypesCommand ??= new RelayCommand(
                    async () =>
                    {
                        var types = await _controller.GetCardTypes();

                        TypesCollection.Clear();
                        foreach (var item in types)
                        {
                            TypesCollection.Add(item);
                        }
                    });

        public bool IsLoading => _controller.IsSearching;

        public ICard SelectedCard
        {
            get => _selectedCard;
            set => SetProperty(ref _selectedCard, value);
        }

        public string SelectedCardId
        {
            get => _selectedCardId;
            set
            {
                if (SetProperty(ref _selectedCardId, value))
                {
                    FindSelectedCardCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public ISet SelectedSet
        {
            get => _selectedSet;
            set => SetProperty(ref _selectedSet, value);
        }

        public string SelectedSetCode
        {
            get => _selectedSetCode;
            set
            {
                if (SetProperty(ref _selectedSetCode, value))
                {
                    GenerateBoosterCommand.NotifyCanExecuteChanged();
                    FindSelectedSetCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public ObservableCollection<ISet> SetsCollection { get; }

        public RelayCommand SetSearchCommand =>
            _setSearchCommand ??= new RelayCommand(
                    async () =>
                    {
                        var sets = await _controller.GetSets(_setSearchString);

                        SetsCollection.Clear();
                        foreach (var item in sets)
                        {
                            SetsCollection.Add(item);
                        }
                    });

        public string SetSearchString
        {
            get => _setSearchString;
            set => SetProperty(ref _setSearchString, value);
        }

        public ObservableCollection<string> TypesCollection { get; }
    }
}