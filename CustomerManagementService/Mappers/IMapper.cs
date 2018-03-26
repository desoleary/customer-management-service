using System;

namespace CustomerManagement.Mappers
{
    public interface IMapper<TInput, TOutput>
    {
        TOutput MapFrom(TInput input);
    }
}