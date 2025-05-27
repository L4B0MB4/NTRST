namespace NTRST.Models.Exceptions;

public class TokenExpiredException(string message) : Exception(message);