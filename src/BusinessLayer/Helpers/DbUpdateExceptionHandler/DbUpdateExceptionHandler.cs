using BusinessLayer.Enums;
using BusinessLayer.Services.Result;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BusinessLayer.Helpers.DbUpdateExceptionHandler;

public static partial class DbUpdateExceptionHandler
{
    public static ServiceResult<T>? GetServiceResultForException<T>(
        DbUpdateException dbUpdateException
    )
    {
        return dbUpdateException.InnerException switch
        {
            PostgresException postgresException
                => GetServiceResultForPostgresException<T>(postgresException),
            SqliteException sqliteException
                => GetServiceResultForSqliteException<T>(sqliteException),
            _ => null
        };
    }

    private static ServiceResult<T> GetServiceResultForSqliteException<T>(
        SqliteException sqliteException
    )
    {
        var sqliteExtendedErrorCode = (SQLiteExtendedErrorCode)
            sqliteException.SqliteExtendedErrorCode;
        return sqliteExtendedErrorCode switch
        {
            SQLiteExtendedErrorCode.ConstraintForeignKey
                => new ServiceResult<T>(sqliteException.Message, ServiceResultCode.Conflict),
            SQLiteExtendedErrorCode.ConstraintUnique
                => new ServiceResult<T>(sqliteException.Message, ServiceResultCode.Conflict),
            _
                => new ServiceResult<T>(
                    sqliteException.Message,
                    ServiceResultCode.InternalServerError
                )
        };
    }

    private static ServiceResult<T> GetServiceResultForPostgresException<T>(
        PostgresException postgresException
    )
    {
        return postgresException.SqlState switch
        {
            PostgresErrorCodes.ForeignKeyViolation
                => new ServiceResult<T>(postgresException.MessageText, ServiceResultCode.Conflict),
            PostgresErrorCodes.UniqueViolation
                => new ServiceResult<T>(postgresException.MessageText, ServiceResultCode.Conflict),
            _
                => new ServiceResult<T>(
                    postgresException.MessageText,
                    ServiceResultCode.InternalServerError
                )
        };
    }
}
