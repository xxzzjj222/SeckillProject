using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Common.AutoMappers
{
    /// <summary>
    /// 对象自动转换工具类
    /// </summary>
    public static class AutoMapperHelper
    {
        /// <summary>
        /// 类型映射，默认字段名字一一对应
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TDestination AutoMapperTo<TDestination>(this object obj)
        {
            if (obj == null)
                return default(TDestination);
            var config = new MapperConfiguration(config => config.CreateMap(obj.GetType(), typeof(TDestination)));
            return config.CreateMapper().Map<TDestination>(obj);
        }

        /// <summary>
        /// 类型映射，可指定映射字段的配置信息
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="cfgExp">可为null,则自动一一映射</param>
        /// <returns></returns>
        public static TDestination AutoMapperTo<TSource, TDestination>(this TSource source, Action<IMapperConfigurationExpression> cfgExp)
            where TSource:class
            where TDestination:class
        {
            if (source == null)
                return default(TDestination);
            var config = new MapperConfiguration(cfgExp ?? (config => config.CreateMap<TSource, TDestination>()));
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 类型映射，字段自动一一对应
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TDestination AutoMapperTo<TSource,TDestination>(this TSource source)
            where TSource:class
            where TDestination:class
        {
            if (source == null)
                return default(TDestination);
            var config = new MapperConfiguration(config => config.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        public static IEnumerable<TDestination> AutoMapperTo<TSource, TDestination>(this IEnumerable<TSource> sources)
            where TSource:class
            where TDestination:class
        {
            if (sources == null)
                return new List<TDestination>();
            var config = new MapperConfiguration(config => config.CreateMap<TSource, TDestination>());
            var mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(sources);
        }
    }
}
