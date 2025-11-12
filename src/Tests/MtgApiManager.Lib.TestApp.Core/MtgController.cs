using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;

namespace MtgApiManager.Lib.TestApp.Core;

public class MtgController(IMtgServiceProvider serviceProvider)
{
    private bool _isSearching;
    private readonly IMtgServiceProvider _serviceProvider = serviceProvider;

    public event EventHandler<bool>? IsSearchingChanged;

    public bool IsSearching
    {
        get => _isSearching;
        private set
        {
            if (_isSearching != value)
            {
                _isSearching = value;
                IsSearchingChanged?.Invoke(this, value);
            }
        }
    }

    public async Task<List<ICard>> GetCards(string searchContent)
    {
        IsSearching = true;

        var cardService = _serviceProvider.GetCardService();

        if (!string.IsNullOrWhiteSpace(searchContent))
        {
            cardService.Where(x => x.Name, searchContent);
        }

        var result = await cardService.AllAsync();

        IsSearching = false;

        if (!result.IsSuccess)
        {
            return [];
        }

        return result.Value;
    }

    public async Task<List<ISet>> GetSets(string searchContent)
    {
        IsSearching = true;

        var setService = _serviceProvider.GetSetService();

        if (!string.IsNullOrWhiteSpace(searchContent))
        {
            setService.Where(x => x.Name, searchContent);
        }

        var result = await setService.AllAsync();

        IsSearching = false;

        if (!result.IsSuccess)
        {
            return [];
        }

        return result.Value;
    }

    public async Task<ICard> FindCardById(string id)
    {
        IsSearching = true;

        var cardService = _serviceProvider.GetCardService();

        var result = await cardService.FindAsync(id);

        IsSearching = false;

        return result.Value;
    }

    public async Task<ISet> FindSetById(string id)
    {
        IsSearching = true;

        var setService = _serviceProvider.GetSetService();

        var result = await setService.FindAsync(id);

        IsSearching = false;

        return result.Value;
    }

    public async Task<List<string>> GetCardFormats()
    {
        IsSearching = true;

        var cardService = _serviceProvider.GetCardService();

        var result = await cardService.GetFormatsAsync();

        IsSearching = false;

        if (!result.IsSuccess)
        {
            return [];
        }

        return result.Value;
    }

    public async Task<List<string>> GetCardSubTypes()
    {
        IsSearching = true;

        var cardService = _serviceProvider.GetCardService();

        var result = await cardService.GetCardSubTypesAsync();

        IsSearching = false;

        if (!result.IsSuccess)
        {
            return [];
        }

        return result.Value;
    }

    public async Task<List<string>> GetCardSuperTypes()
    {
        IsSearching = true;

        var cardService = _serviceProvider.GetCardService();

        var result = await cardService.GetCardSuperTypesAsync();

        IsSearching = false;

        if (!result.IsSuccess)
        {
            return [];
        }

        return result.Value;
    }

    public async Task<List<string>> GetCardTypes()
    {
        IsSearching = true;

        var cardService = _serviceProvider.GetCardService();

        var result = await cardService.GetCardTypesAsync();

        IsSearching = false;

        if (!result.IsSuccess)
        {
            return [];
        }

        return result.Value;
    }
    
    public async Task<List<ICard>> GenerateBooster(string setCode)
    {
        IsSearching = true;

        var setService = _serviceProvider.GetSetService();

        var result = await setService.GenerateBoosterAsync(setCode);

        IsSearching = false;

        if (!result.IsSuccess)
        {
            return [];
        }

        return result.Value;
    }
}
