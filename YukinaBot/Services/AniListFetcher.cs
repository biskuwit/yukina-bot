using YukinaBot.ResponseModels;
using YukinaBot.Utility;

namespace YukinaBot.Services;

public class AniListFetcher
{
    private readonly GraphQLUtility _graphQLUtility = new("https://graphql.anilist.co");

    public async Task<PageResponse> SearchMediaAsync(string searchCriteria, int startPage = 1, int entriesPerPage = 25)
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
                        season
                        seasonYear
                        episodes
                        duration
                        chapters
                        volumes
                        coverImage {
                            extraLarge
                        }
                        genres
                        synonyms
                        meanScore
                        popularity
                        favourites
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
        int entriesPerPage = 25)
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
                            idMal
                            title {
                                english
                                romaji
                                native
                            }
                            type
                            status
                            description
                            season
                            seasonYear
                            episodes
                            duration
                            chapters
                            volumes
                            coverImage {
                                extraLarge
                            }
                            genres
                            synonyms
                            meanScore
                            popularity
                            favourites
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
                        idMal
                        title {
                            english
                            romaji
                            native
                        }
                        type
                        status
                        description
                        season
                        seasonYear
                        episodes
                        duration
                        chapters
                        volumes
                        coverImage {
                            extraLarge
                        }
                        genres
                        synonyms
                        meanScore
                        popularity
                        favourites
                        siteUrl
                    }
                }";

        object variables = new
        {
            search = searchCriteria
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<MediaResponse>(query, variables);
    }

    public async Task<MediaResponse> FindMediaAsync(int mediaId)
    {
        var query = @"
                query ($id: Int) {
                    Media (id: $id) {
                        id
                        idMal
                        title {
                            english
                            romaji
                            native
                        }
                        type
                        status
                        description
                        season
                        seasonYear
                        episodes
                        duration
                        chapters
                        volumes
                        coverImage {
                            extraLarge
                        }
                        genres
                        synonyms
                        meanScore
                        popularity
                        favourites
                        siteUrl
                    }
                }";

        object variables = new
        {
            id = mediaId
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<MediaResponse>(query, variables);
    }

    public async Task<MediaResponse> FindMediaTypeAsync(string searchCriteria, string mediaType)
    {
        var query = @"
                query ($search: String, $type: MediaType) {
                    Media (search: $search, type: $type) {
                        id
                        idMal
                        title {
                            english
                            romaji
                            native
                        }
                        type
                        status
                        description
                        season
                        seasonYear
                        episodes
                        duration
                        chapters
                        volumes
                        coverImage {
                            extraLarge
                        }
                        genres
                        synonyms
                        meanScore
                        popularity
                        favourites
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

    public async Task<MediaResponse> FindMediaTypeAsync(int mediaId, string mediaType)
    {
        var query = @"
                query ($id: Int, $type: MediaType) {
                    Media (id: $id, type: $type) {
                        id
                        idMal
                        title {
                            english
                            romaji
                            native
                        }
                        type
                        status
                        description
                        season
                        seasonYear
                        episodes
                        duration
                        chapters
                        volumes
                        coverImage {
                            extraLarge
                        }
                        genres
                        synonyms
                        meanScore
                        popularity
                        favourites
                        siteUrl
                    }
                }";

        object variables = new
        {
            id = mediaId,
            type = mediaType
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<MediaResponse>(query, variables);
    }

    public async Task<UserResponse> FindUserAsync(string anilistName)
    {
        var query = @"
                query ($name: String) {
                    User(name: $name) {
                        id
                        name
                    }
                }";

        object variables = new
        {
            name = anilistName
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<UserResponse>(query, variables);
    }

    public async Task<UserResponse> FindUserAsync(int userId)
    {
        var query = @"
                query ($id: Int) {
                    User(id: $id) {
                        id
                        name
                    }
                }";

        object variables = new
        {
            id = userId
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<UserResponse>(query, variables);
    }

    public async Task<UserResponse> FindUserStatisticsAsync(string anilistName)
    {
        var query = @"
                query ($name: String) {
                    User(name: $name) {
                        name
                        about
                        avatar {
                            large
                            medium
                        }
                        bannerImage
                        statistics {
                            anime {
                                count
                                meanScore
                                standardDeviation
                                minutesWatched
                                episodesWatched
                                chaptersRead
                                volumesRead
                            }
                            manga {
                                count
                                meanScore
                                standardDeviation
                                minutesWatched
                                episodesWatched
                                chaptersRead
                                volumesRead
                            }
                        }
                        siteUrl
                        updatedAt
                    }
                }";

        object variables = new
        {
            name = anilistName
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<UserResponse>(query, variables);
    }

    public async Task<UserResponse> FindUserStatisticsAsync(long anilistId)
    {
        var query = @"
                query ($id: Int) {
                  User(id: $id) {
                    id
                    name
                    about
                    avatar {
                      large
                      medium
                    }
                    bannerImage
                    statistics {
                      anime {
                        count
                        meanScore
                        standardDeviation
                        minutesWatched
                        episodesWatched
                        chaptersRead
                        volumesRead
                      }
                      manga {
                        count
                        meanScore
                        standardDeviation
                        minutesWatched
                        episodesWatched
                        chaptersRead
                        volumesRead
                      }
                    }
                    siteUrl
                    updatedAt
                  }
                }";

        object variables = new
        {
            id = anilistId
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<UserResponse>(query, variables);
    }

    public async Task<MediaListCollectionResponse> FindUserListAsync(string anilistName, string mediaType)
    {
        var query = @"
                query ($userName: String, $type: MediaType) {
                    MediaListCollection(userName: $userName, type: $type) {
                        lists {
                            entries {
                                mediaId
                                status
                                score(format:POINT_100)
                                progress
                                media {
                                    id
                                    title {
                                        english
                                        romaji
                                        native
                                    }
                                    type
                                    status
                                }
                            }
                            name
                            status
                        }
                    }
                }";

        object variables = new
        {
            userName = anilistName,
            type = mediaType
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<MediaListCollectionResponse>(query, variables);
    }

    public async Task<MediaListCollectionResponse> FindUserListAsync(long anilistId, string mediaType)
    {
        var query = @"
                query ($id: Int, $type: MediaType) {
                    MediaListCollection(userId: $id, type: $type) {
                        lists {
                            entries {
                                mediaId
                                status
                                score(format:POINT_100)
                                progress
                                media {
                                    id
                                    title {
                                        english
                                        romaji
                                        native
                                    }
                                    type
                                    status
                                }
                            }
                            name
                            status
                        }
                    }
                }";

        object variables = new
        {
            id = anilistId,
            type = mediaType
        };

        return await _graphQLUtility.ExecuteGraphQLRequest<MediaListCollectionResponse>(query, variables);
    }
}