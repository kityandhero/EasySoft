using AutoMapper;

namespace EasySoft.Core.AutoMapper.Interfaces;

// https://blog.csdn.net/Joyhen/article/details/40188035
// http://www.2cto.com/kf/201206/136883.html

public interface IAutoConverter<in TSource, TTarget> : ITypeConverter<TSource, TTarget>
{
}