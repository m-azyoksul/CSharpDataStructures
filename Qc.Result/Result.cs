using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Qc.Result
{
    public class ResultError<TErrorType>
    {
        public TErrorType Type { get; init; } = default!;
        public string Message { get; init; } = null!;
    }

    public class Result<TValue, TErrorType> : IEquatable<Result<TValue, TErrorType>>
    {
        public TValue? Value { get; protected init; }
        public ResultError<TErrorType>? Error { get; protected init; }

        protected Result()
        {
        }

        public static implicit operator Result<TValue, TErrorType>(TValue value) => new()
        {
            Value = value,
        };

        public static implicit operator Result<TValue, TErrorType>(TErrorType error) => new()
        {
            Error = new ResultError<TErrorType>
            {
                Type = error,
                Message = error!.ToString() ?? "null",
            }
        };

        public static implicit operator Result<TValue, TErrorType>((TErrorType type, string message) error) => new()
        {
            Error = new ResultError<TErrorType>
            {
                Type = error.type,
                Message = error.message,
            }
        };

        [MemberNotNullWhen(true, nameof(Value))]
        [MemberNotNullWhen(false, nameof(Error))]
        public bool IsSuccess => Error is null;

        [MemberNotNullWhen(false, nameof(Value))]
        [MemberNotNullWhen(true, nameof(Error))]
        public bool IsNotSuccess => !IsSuccess;

        public void Execute(Action<TValue> valueAction, Action<ResultError<TErrorType>> errorFunction)
        {
            if (IsSuccess)
                valueAction(Value);
            else
                errorFunction(Error);
        }

        public T Select<T>(Func<TValue, T> valueFunction, Func<ResultError<TErrorType>, T> errorFunction)
        {
            if (IsSuccess)
                return valueFunction(Value);
            return errorFunction(Error);
        }

        /// <summary>
        /// This method helps quickly convert the result to a <see cref="TValue"/> type object.
        /// It is effectively the same as `result.Value ?? defaultValue`
        /// </summary>
        /// <param name="defaultValue">The value to be returned if the result is not successful</param>
        /// <returns>A valid value object</returns>
        public TValue OrDefault(TValue defaultValue) => IsSuccess ? Value : defaultValue;

        public bool TryGetValue([NotNullWhen(true)] out TValue? value)
        {
            if (IsSuccess)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        public bool TryGetError([NotNullWhen(true)] out ResultError<TErrorType>? error)
        {
            if (IsNotSuccess)
            {
                error = Error;
                return true;
            }

            error = default;
            return false;
        }

        public override string ToString()
        {
            if (IsSuccess)
                return Value.ToString() ?? "null";

            return $"Error type: {Error.Type}, Message: {Error.Message}";
        }

        public bool Equals(Result<TValue, TErrorType>? other)
        {
            // Check the reference equalities
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            
            // If their success states are different
            if (IsSuccess != other.IsSuccess) return false;
            
            // If their success states are the same
            if (IsSuccess && other.IsSuccess) return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
            return EqualityComparer<TErrorType>.Default.Equals(Error!.Type, other.Error!.Type) && Error!.Message == other.Error!.Message;
        }

        public override bool Equals(object? other) => other?.GetType() == GetType() && Equals((Result<TValue, TErrorType>) other);
        public override int GetHashCode() => HashCode.Combine(Value, Error);
        public static bool operator ==(Result<TValue, TErrorType>? a, Result<TValue, TErrorType>? b) => a?.Equals(b) ?? false;
        public static bool operator !=(Result<TValue, TErrorType>? a, Result<TValue, TErrorType>? b) => !a?.Equals(b) ?? true;
    }

    public class Result<TValue> : Result<TValue, ServiceError>
    {
        public static implicit operator Result<TValue>(TValue value) => new()
        {
            Value = value
        };

        public static implicit operator Result<TValue>(ServiceError error) => new()
        {
            Error = new ResultError<ServiceError>
            {
                Type = error,
                Message = error.ToString()
            }
        };

        public static implicit operator Result<TValue>((ServiceError type, string message) error) => new()
        {
            Error = new ResultError<ServiceError>
            {
                Type = error.type,
                Message = error.message
            }
        };

        public override bool Equals(object? other) => other?.GetType() == GetType() && Equals((Result<TValue>) other);

        public override int GetHashCode() => HashCode.Combine(Value, Error);

        public static bool operator ==(Result<TValue>? a, Result<TValue>? b) => a?.Equals(b) ?? false;

        public static bool operator !=(Result<TValue>? a, Result<TValue>? b) => !a?.Equals(b) ?? true;
    }
}