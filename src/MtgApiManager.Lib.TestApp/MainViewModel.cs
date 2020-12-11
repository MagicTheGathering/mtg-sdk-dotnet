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
        private RelayCommand _cardSearchCommand;
        private string _cardSearchString = null;
        private RelayCommand _findSelectedCardCommand;
        private RelayCommand _findSelectedSetCommand;
        private RelayCommand _generateBoosterCommand;
        private RelayCommand _getCardFormatsCommand;
        private RelayCommand _getCardSubTypesCommand;
        private RelayCommand _getCardSuperTypesCommand;
        private RelayCommand _getCardTypesCommand;
        private bool _isLoading = false;
        private ICard _selectedCard = null;
        private string _selectedCardId = null;
        private ISet _selectedSet = null;
        private string _selectedSetCode = null;
        private RelayCommand _setSearchCommand;
        private string _setSearchString = null;

        public MainViewModel()
        {
            CardsCollection = new ObservableCollection<ICard>();
            SetsCollection = new ObservableCollection<ISet>();
            TypesCollection = new ObservableCollection<string>();
            GeneratedBoosterCollection = new ObservableCollection<ICard>();
            _serviceProvider = new MtgServiceProvider();
        }

        public ObservableCollection<ICard> CardsCollection { get; }

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

                        CardsCollection.Clear();

                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                CardsCollection.Add(item);
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

                        GeneratedBoosterCollection.Clear();
                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                GeneratedBoosterCollection.Add(item);
                            }
                        }

                        IsLoading = false;
                    },
                    () => !string.IsNullOrWhiteSpace(SelectedSetCode));
            }
        }

        public ObservableCollection<ICard> GeneratedBoosterCollection { get; }

        public RelayCommand GetCardFormatsCommand
        {
            get
            {
                return _getCardFormatsCommand ??= new RelayCommand(
                    async () =>
                    {
                        IsLoading = true;

                        ICardService cardService = _serviceProvider.GetCardService();
                        var result = await cardService.GetFormatsAsync();

                        TypesCollection.Clear();
                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                TypesCollection.Add(item);
                            }
                        }

                        IsLoading = false;
                    });
            }
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

                        TypesCollection.Clear();
                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                TypesCollection.Add(item);
                            }
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

                        TypesCollection.Clear();
                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                TypesCollection.Add(item);
                            }
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

                        TypesCollection.Clear();
                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                TypesCollection.Add(item);
                            }
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

        public ObservableCollection<ISet> SetsCollection { get; }

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

                        SetsCollection.Clear();

                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                SetsCollection.Add(item);
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

        public ObservableCollection<string> TypesCollection { get; }
    }
}