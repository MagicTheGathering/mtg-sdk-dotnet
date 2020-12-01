using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;

namespace MtgApiManager.Lib.TestApp
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMtgServiceProvider _serviceProvider;
        private ObservableCollection<ICard> _cardsCollection = null;
        private RelayCommand _cardSearchCommand;
        private string _cardSearchString = null;
        private RelayCommand _findSelectedCardCommand;
        private RelayCommand _findSelectedSetCommand;
        private RelayCommand _generateBoosterCommand;
        private ObservableCollection<ICard> _generatedBoosterCollection = null;
        private RelayCommand _getCardSubTypesCommand;
        private RelayCommand _getCardSuperTypesCommand;
        private RelayCommand _getCardTypesCommand;
        private bool _isLoading = false;
        private ICard _selectedCard = null;
        private string _selectedCardId = null;
        private ISet _selectedSet = null;
        private string _selectedSetCode = null;
        private ObservableCollection<ISet> _setsCollection = null;
        private RelayCommand _setSearchCommand;
        private string _setSearchString = null;

        private ObservableCollection<string> _typesCollection = null;

        public MainViewModel()
        {
            _cardsCollection = new ObservableCollection<ICard>();
            _setsCollection = new ObservableCollection<ISet>();
            _serviceProvider = new MtgServiceProvider();
        }

        public ObservableCollection<ICard> CardsCollection
        {
            get => _cardsCollection;
            set => Set(() => CardsCollection, ref _cardsCollection, value);
        }

        public RelayCommand CardSearchCommand
        {
            get
            {
                return _cardSearchCommand ??= new RelayCommand(
                    async () =>
                    {
                        ICardService cardService = _serviceProvider.GetCardService();

                        if (!string.IsNullOrWhiteSpace(_cardSearchString))
                        {
                            cardService.Where(c => c.Name, _cardSearchString);
                        }

                        IsLoading = true;

                        var result = await cardService.AllAsync();

                        _cardsCollection.Clear();

                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                _cardsCollection.Add(item);
                            }
                        }

                        IsLoading = false;
                    },
                    () => true);
            }
        }

        public string CardSearchString
        {
            get => _cardSearchString;
            set => Set(() => CardSearchString, ref _cardSearchString, value);
        }

        public RelayCommand FindSelectedCardCommand
        {
            get
            {
                return _findSelectedCardCommand ??= new RelayCommand(
                    async () =>
                    {
                        IsLoading = true;

                        ICardService cardService = _serviceProvider.GetCardService();
                        var result = await cardService.FindAsync(_selectedCardId);

                        if (result.IsSuccess)
                        {
                            SelectedCard = result.Value;
                        }

                        IsLoading = false;
                    },
                    () => !string.IsNullOrWhiteSpace(_selectedCardId));
            }
        }

        public RelayCommand FindSelectedSetCommand
        {
            get
            {
                return _findSelectedSetCommand ??= new RelayCommand(
                    async () =>
                    {
                        IsLoading = true;

                        ISetService setService = _serviceProvider.GetSetService();
                        var result = await setService.FindAsync(_selectedSetCode);

                        if (result.IsSuccess)
                        {
                            SelectedSet = result.Value;
                        }

                        IsLoading = false;
                    },
                    () => !string.IsNullOrWhiteSpace(_selectedSetCode));
            }
        }

        public RelayCommand GenerateBoosterCommand
        {
            get
            {
                return _generateBoosterCommand ??= new RelayCommand(
                    async () =>
                    {
                        IsLoading = true;

                        ISetService setService = _serviceProvider.GetSetService();
                        var result = await setService.GenerateBoosterAsync(_selectedSetCode);

                        if (result.IsSuccess)
                        {
                            GeneratedBoosterCollection = new ObservableCollection<ICard>(result.Value);
                        }

                        IsLoading = false;
                    },
                    () => !string.IsNullOrWhiteSpace(SelectedSetCode));
            }
        }

        public ObservableCollection<ICard> GeneratedBoosterCollection
        {
            get => _generatedBoosterCollection;
            set => Set(() => GeneratedBoosterCollection, ref _generatedBoosterCollection, value);
        }

        public RelayCommand GetCardSubTypesCommand
        {
            get
            {
                return _getCardSubTypesCommand ??= new RelayCommand(
                    async () =>
                    {
                        IsLoading = true;

                        ICardService cardService = _serviceProvider.GetCardService();
                        var result = await cardService.GetCardSubTypesAsync();

                        if (result.IsSuccess)
                        {
                            TypesCollection = new ObservableCollection<string>(result.Value);
                        }

                        IsLoading = false;
                    });
            }
        }

        public RelayCommand GetCardSuperTypesCommand
        {
            get
            {
                return _getCardSuperTypesCommand ??= new RelayCommand(
                    async () =>
                    {
                        IsLoading = true;

                        ICardService cardService = _serviceProvider.GetCardService();
                        var result = await cardService.GetCardSuperTypesAsync();

                        if (result.IsSuccess)
                        {
                            TypesCollection = new ObservableCollection<string>(result.Value);
                        }

                        IsLoading = false;
                    });
            }
        }

        public RelayCommand GetCardTypesCommand
        {
            get
            {
                return _getCardTypesCommand ??= new RelayCommand(
                    async () =>
                    {
                        IsLoading = true;

                        ICardService cardService = _serviceProvider.GetCardService();
                        var result = await cardService.GetCardTypesAsync();

                        if (result.IsSuccess)
                        {
                            TypesCollection = new ObservableCollection<string>(result.Value);
                        }

                        IsLoading = false;
                    });
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => Set(() => IsLoading, ref _isLoading, value);
        }

        public ICard SelectedCard
        {
            get => _selectedCard;
            set => Set(() => SelectedCard, ref _selectedCard, value);
        }

        public string SelectedCardId
        {
            get => _selectedCardId;
            set => Set(() => SelectedCardId, ref _selectedCardId, value);
        }

        public ISet SelectedSet
        {
            get => _selectedSet;
            set => Set(() => SelectedSet, ref _selectedSet, value);
        }

        public string SelectedSetCode
        {
            get => _selectedSetCode;
            set => Set(() => SelectedSetCode, ref _selectedSetCode, value);
        }

        public ObservableCollection<ISet> SetsCollection
        {
            get => _setsCollection;
            set => Set(() => SetsCollection, ref _setsCollection, value);
        }

        public RelayCommand SetSearchCommand
        {
            get
            {
                return _setSearchCommand ??= new RelayCommand(
                    async () =>
                    {
                        ISetService setService = _serviceProvider.GetSetService();

                        if (!string.IsNullOrWhiteSpace(_setSearchString))
                        {
                            setService.Where(c => c.Name, _setSearchString);
                        }

                        IsLoading = true;

                        var result = await setService.AllAsync();

                        _setsCollection.Clear();

                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                _setsCollection.Add(item);
                            }
                        }

                        IsLoading = false;
                    },
                    () => true);
            }
        }

        public string SetSearchString
        {
            get => _setSearchString;
            set => Set(() => SetSearchString, ref _setSearchString, value);
        }

        public ObservableCollection<string> TypesCollection
        {
            get => _typesCollection;
            set => Set(() => TypesCollection, ref _typesCollection, value);
        }
    }
}