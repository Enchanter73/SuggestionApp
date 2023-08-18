namespace SuggestionAppLibrary.DataAccess
{
    public interface ISuggestionData
    {
        Task CreateSuggestion(SuggestionModel suggestion);
        Task<List<SuggestionModel>> GetAllAprovedSuggestions();
        Task<List<SuggestionModel>> GetAllSuggestion();
        Task<List<SuggestionModel>> GetAllSuggestionsWaitingForApproval();
        Task<SuggestionModel> GetSuggestion(string id);
        Task UpdateSuggestion(SuggestionModel suggestion);
        Task UpvoteSuggestion(string suggestionId, string userId);
    }
}