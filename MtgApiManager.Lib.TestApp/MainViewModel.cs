using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;

namespace MtgApiManager.Lib.TestApp
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<Card> _cardsCollection = null;
        private RelayCommand _cardSearchCommand;
        private string _cardSearchString = null;
        private RelayCommand _findSelectedCardCommand;
        private RelayCommand _findSelectedSetCommand;
        private RelayCommand _generateBoosterCommand;
        private ObservableCollection<Card> _generatedBoosterCollection = null;
        private RelayCommand _getCardSubTypesCommand;
        private RelayCommand _getCardSuperTypesCommand;
        private RelayCommand _getCardTypesCommand;
        private bool _isLoading = false;
        private Card _selectedCard = null;
        private string _selectedCardId = null;
        private Set _selectedSet = null;
        private string _selectedSetCode = null;
        private ObservableCollection<Set> _setsCollection = null;
        private RelayCommand _setSearchCommand;
        private string _setSearchString = null;

        private ObservableCollection<string> _typesCollection = null;

        public MainViewModel()
        {
            this._cardsCollection = new ObservableCollection<Card>();
            this._setsCollection = new ObservableCollection<Set>();
        }

        public ObservableCollection<Card> CardsCollection
        {
            get
            {
                return _cardsCollection;
            }
            set
            {
                Set(() => CardsCollection, ref _cardsCollection, value);
            }
        }

        public RelayCommand CardSearchCommand
        {
            get
            {
                return _cardSearchCommand
                    ?? (_cardSearchCommand = new RelayCommand(
                    async () =>
                    {
                        CardService cardService = new CardService();

                        if (!string.IsNullOrWhiteSpace(this._cardSearchString))
                        {
                            cardService.Where(c => c.Name, this._cardSearchString);
                        }

                        this.IsLoading = true;

                        var result = await cardService.AllAsync();

                        this._cardsCollection.Clear();

                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                this._cardsCollection.Add(item);
                            }
                        }

                        this.IsLoading = false;
                    },
                    () => true));
            }
        }

        public string CardSearchString
        {
            get
            {
                return _cardSearchString;
            }
            set
            {
                Set(() => CardSearchString, ref _cardSearchString, value);
            }
        }

        public RelayCommand FindSelectedCardCommand
        {
            get
            {
                return _findSelectedCardCommand
                    ?? (_findSelectedCardCommand = new RelayCommand(
                    async () =>
                    {
                        this.IsLoading = true;

                        CardService cardService = new CardService();
                        var result = await cardService.FindAsync(this._selectedCardId);

                        if (result.IsSuccess)
                        {
                            this.SelectedCard = result.Value;
                        }

                        this.IsLoading = false;
                    },
                    () => !string.IsNullOrWhiteSpace(this._selectedCardId)));
            }
        }

        public RelayCommand FindSelectedSetCommand
        {
            get
            {
                return _findSelectedSetCommand
                    ?? (_findSelectedSetCommand = new RelayCommand(
                    async () =>
                    {
                        this.IsLoading = true;

                        SetService setService = new SetService();
                        var result = await setService.FindAsync(this._selectedSetCode);

                        if (result.IsSuccess)
                        {
                            this.SelectedSet = result.Value;
                        }

                        this.IsLoading = false;
                    },
                    () => !string.IsNullOrWhiteSpace(this._selectedSetCode)));
            }
        }

        public RelayCommand GenerateBoosterCommand
        {
            get
            {
                return _generateBoosterCommand
                    ?? (_generateBoosterCommand = new RelayCommand(
                    async () =>
                    {
                        this.IsLoading = true;

                        SetService setService = new SetService();
                        var result = await setService.GenerateBoosterAsync(this._selectedSetCode);

                        if (result.IsSuccess)
                        {
                            this.GeneratedBoosterCollection = new ObservableCollection<Card>(result.Value);
                        }

                        this.IsLoading = false;
                    },
                    () => !string.IsNullOrWhiteSpace(this.SelectedSetCode)));
            }
        }

        public ObservableCollection<Card> GeneratedBoosterCollection
        {
            get
            {
                return _generatedBoosterCollection;
            }
            set
            {
                Set(() => GeneratedBoosterCollection, ref _generatedBoosterCollection, value);
            }
        }

        public RelayCommand GetCardSubTypesCommand
        {
            get
            {
                return _getCardSubTypesCommand
                    ?? (_getCardSubTypesCommand = new RelayCommand(
                    async () =>
                    {
                        this.IsLoading = true;

                        CardService cardService = new CardService();
                        var result = await cardService.GetCardSubTypesAsync();

                        if (result.IsSuccess)
                        {
                            this.TypesCollection = new ObservableCollection<string>(result.Value);
                        }

                        this.IsLoading = false;
                    }));
            }
        }

        public RelayCommand GetCardSuperTypesCommand
        {
            get
            {
                return _getCardSuperTypesCommand
                    ?? (_getCardSuperTypesCommand = new RelayCommand(
                    async () =>
                    {
                        this.IsLoading = true;

                        CardService cardService = new CardService();
                        var result = await cardService.GetCardSuperTypesAsync();

                        if (result.IsSuccess)
                        {
                            this.TypesCollection = new ObservableCollection<string>(result.Value);
                        }

                        this.IsLoading = false;
                    }));
            }
        }

        public RelayCommand GetCardTypesCommand
        {
            get
            {
                return _getCardTypesCommand
                    ?? (_getCardTypesCommand = new RelayCommand(
                    async () =>
                    {
                        this.IsLoading = true;

                        CardService cardService = new CardService();
                        var result = await cardService.GetCardTypesAsync();

                        if (result.IsSuccess)
                        {
                            this.TypesCollection = new ObservableCollection<string>(result.Value);
                        }

                        this.IsLoading = false;
                    }));
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                Set(() => IsLoading, ref _isLoading, value);
            }
        }

        public Card SelectedCard
        {
            get
            {
                return _selectedCard;
            }
            set
            {
                Set(() => SelectedCard, ref _selectedCard, value);
            }
        }

        public string SelectedCardId
        {
            get
            {
                return _selectedCardId;
            }
            set
            {
                Set(() => SelectedCardId, ref _selectedCardId, value);
            }
        }

        public Set SelectedSet
        {
            get
            {
                return _selectedSet;
            }
            set
            {
                Set(() => SelectedSet, ref _selectedSet, value);
            }
        }

        public string SelectedSetCode
        {
            get
            {
                return _selectedSetCode;
            }
            set
            {
                Set(() => SelectedSetCode, ref _selectedSetCode, value);
            }
        }

        public ObservableCollection<Set> SetsCollection
        {
            get
            {
                return _setsCollection;
            }
            set
            {
                Set(() => SetsCollection, ref _setsCollection, value);
            }
        }

        public RelayCommand SetSearchCommand
        {
            get
            {
                return _setSearchCommand
                    ?? (_setSearchCommand = new RelayCommand(
                    async () =>
                    {
                        SetService setService = new SetService();

                        if (!string.IsNullOrWhiteSpace(this._setSearchString))
                        {
                            setService.Where(c => c.Name, this._setSearchString);
                        }

                        this.IsLoading = true;

                        var result = await setService.AllAsync();

                        this._setsCollection.Clear();

                        if (result.IsSuccess)
                        {
                            foreach (var item in result.Value)
                            {
                                this._setsCollection.Add(item);
                            }
                        }

                        this.IsLoading = false;
                    },
                    () => true));
            }
        }

        public string SetSearchString
        {
            get
            {
                return _setSearchString;
            }
            set
            {
                Set(() => SetSearchString, ref _setSearchString, value);
            }
        }

        public ObservableCollection<string> TypesCollection
        {
            get
            {
                return _typesCollection;
            }
            set
            {
                Set(() => TypesCollection, ref _typesCollection, value);
            }
        }
    }
}