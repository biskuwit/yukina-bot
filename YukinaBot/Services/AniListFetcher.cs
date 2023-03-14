using YukinaBot.ResponseModels;
using YukinaBot.Utility;

namespace YukinaBot.Services
{
    public class AniListFetcher
    {
        private readonly GraphQLUtility _graphQLUtility = new("https://graphql.anilist.co");

        public async Task<PageResponse> SearchMediaAsync(string searchCriteria, int startPage = 1,
            int entriesPerPage = 10)
        {
            var query = @"
            query ($page: Int, $perPage: Int, $search: String) {
                Page (page: $page, perPage: $perPage) {
                    pageInfo {
                        total
                        currentPage
                        lastPage
                        hasNextPage
                        perPage
                    }
                    media (search: $search) {
                        id
                        title {
                            english
                            romaji
                            native
                        }
                        type
                        status
                        description
                        episodes
                        duration
                        chapters
                        volumes
                        coverImage {
                            extraLarge
                        }
                        genres
                        meanScore
                        popularity
                        siteUrl
                    }
                }
            }";

            object variables = new
            {
                search = searchCriteria,
                page = startPage,
                perPage = entriesPerPage
            };

            return await _graphQLUtility.ExecuteGraphQLRequest<PageResponse>(query, variables);
        }

        public async Task<PageResponse> SearchMediaTypeAsync(string searchCriteria, string mediaType, int startPage = 1,
            int entriesPerPage = 10)
        {
            var query = @"
                query ($page: Int, $perPage: Int, $search: String, $type: MediaType) {
                    Page (page: $page, perPage: $perPage) {
                        pageInfo {
                            total
                            currentPage
                            lastPage
                            hasNextPage
                            perPage
                        }
                        media (search: $search, type: $type) {
                            id
                            title {
                                english
                                romaji
                                native
                            }
                            type
                            status
                            description
                            episodes
                            duration
                            chapters
                            volumes
                            coverImage {
                                extraLarge
                            }
                            genres
                            meanScore
                            popularity
                            siteUrl
                        }
                    }
                }";

            object variables = new
            {
                search = searchCriteria,
                type = mediaType,
                page = startPage,
                perPage = entriesPerPage
            };

            return await _graphQLUtility.ExecuteGraphQLRequest<PageResponse>(query, variables);
        }

        public async Task<MediaResponse> FindMediaAsync(string searchCriteria)
        {
            var query = @"
                query ($search: String) {
                    Media (search: $search) {
                        id
                        title {
                            english
                            romaji
                            native
                        }
                        type
                        status
                        description
                        episodes
                        duration
                        chapters
                        volumes
                        coverImage {
                            extraLarge
                        }
                        genres
                        meanScore
                        popularity
                        siteUrl
                    }
                }";

            object variables = new
            {
                search = searchCriteria
            };

            return await _graphQLUtility.ExecuteGraphQLRequest<MediaResponse>(query, variables);
        }

        public async Task<MediaResponse> FindMediaTypeAsync(string searchCriteria, string mediaType)
        {
            var query = @"
                query ($search: String, $type: MediaType) {
                    Media (search: $search, type: $type) {
                        id
                        title {
                            english
                            romaji
                            native
                        }
                        type
                        status
                        description
                        episodes
                        duration
                        chapters
                        volumes
                        coverImage {
                            extraLarge
                        }
                        genres
                        meanScore
                        popularity
                        siteUrl
                    }
                }";

            object variables = new
            {
                search = searchCriteria,
                type = mediaType
            };

            return await _graphQLUtility.ExecuteGraphQLRequest<MediaResponse>(query, variables);
        }
    }
}
