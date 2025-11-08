using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;

namespace MtgApiManager.Lib.TestApp
{
    public class MainViewModel : ObservableRecipient
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
            CardsCollection = [];
            SetsCollection = [];
            TypesCollection = [];
            GeneratedBoosterCollection = [];
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

        public RelayCommand FindSelectedSetCommand =>
            _findSelectedSetCommand ??= new RelayCommand(
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

        public RelayCommand GenerateBoosterCommand =>
            _generateBoosterCommand ??= new RelayCommand(
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
                    () => !string.IsNullOrWhiteSpace(_selectedSetCode));

        public ObservableCollection<ICard> GeneratedBoosterCollection { get; }

        public RelayCommand GetCardFormatsCommand =>
            _getCardFormatsCommand ??= new RelayCommand(
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

        public RelayCommand GetCardSubTypesCommand =>
            _getCardSubTypesCommand ??= new RelayCommand(
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

        public RelayCommand GetCardSuperTypesCommand =>
            _getCardSuperTypesCommand ??= new RelayCommand(
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

        public RelayCommand GetCardTypesCommand =>
            _getCardTypesCommand ??= new RelayCommand(
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

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

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
                    });

        public string SetSearchString
        {
            get => _setSearchString;
            set => SetProperty(ref _setSearchString, value);
        }

        public ObservableCollection<string> TypesCollection { get; }
    }
}