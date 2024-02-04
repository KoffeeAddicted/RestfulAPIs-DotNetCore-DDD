using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class StoryNotFound(Int64 storyId) : NotFoundException(
    $"The story type with the identifier {storyId} was not found."); 