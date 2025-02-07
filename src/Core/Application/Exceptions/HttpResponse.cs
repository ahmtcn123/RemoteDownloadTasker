namespace Core.Application.Exceptions;

public class UnauthorizedException(string message) : Exception(message);

public class NotFoundException(string message) : Exception(message);

public class BadRequestException(string message) : Exception(message);

public class InternalServerErrorException(string message) : Exception(message);

public class ConflictException(string message) : Exception(message);

public class ForbiddenException(string message) : Exception(message);