using Projects.Cores.Middleware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Cores.Utils
{
    /// <summary>
    /// 转换工具类
    /// </summary>
    public  class ConvertUtil
    {
        /// <summary>
        /// 中台结果转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="middleResult"></param>
        /// <returns></returns>
        public static T MiddleResultToObject<T>(MiddleResult middleResult)where T:new()
        {
            return DicToObject<T>(middleResult.resultDic);
        }

        /// <summary>
        /// 中台结果转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="middleResult"></param>
        /// <returns></returns>
        public static IList<T> MiddleResultToList<T>(MiddleResult middleResult)where T:new()
        {
            return ListToObject<T>(middleResult.resultList);
        }

        /// <summary>
        /// 字典转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T DicToObject<T>(IDictionary<string,object> dic)where T:new()
        {
            Type type = typeof(T);
            T entity = new T();
            var props = type.GetProperties();
            string val;
            object obj = null;
            foreach (var prop in props)
            {
                if (!dic.ContainsKey(prop.Name))
                    continue;
                val = dic[prop.Name].ToString();

                object defaultVal;
                if (prop.PropertyType.Name.Equals("String"))
                {
                    defaultVal = "";
                }
                else if (prop.PropertyType.Name.Equals("Boolean"))
                {
                    defaultVal = false;
                    val = (val.Equals("1") || val.Equals("on")).ToString();
                }
                else if (prop.PropertyType.Name.Equals("Decimal"))
                {
                    defaultVal = 0M;
                }
                else
                {
                    defaultVal = 0;
                }

                if (!prop.PropertyType.IsGenericType)
                {
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, prop.PropertyType);
                }
                else
                {
                    Type genericTypeDefinition = prop.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition==typeof(Nullable<>))
                    {
                        obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, Nullable.GetUnderlyingType(prop.PropertyType));
                    }
                }

                prop.SetValue(entity, obj, null);
            }

            return entity;
        }

        /// <summary>
        /// 字典转换成对象
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static dynamic DicToObject(IDictionary<string,object> dic,Type type)
        {
            var entity = Activator.CreateInstance(type);
            var props = type.GetProperties();
            string val;
            object obj = null;
            foreach (var prop in props)
            {
                if (!dic.ContainsKey(prop.Name))
                    continue;
                val = dic[prop.Name].ToString();

                object defaultVal;
                if (prop.PropertyType.Name.Equals("String"))
                {
                    defaultVal = "";
                }
                else if (prop.PropertyType.Name.Equals("Boolean"))
                {
                    defaultVal = false;
                    val = (val.Equals("1") || val.Equals("on")).ToString();
                }
                else if (prop.PropertyType.Name.Equals("Decimal"))
                {
                    defaultVal = 0M;
                }
                else
                {
                    defaultVal = 0;
                }

                if (!prop.PropertyType.IsGenericType)
                {
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, prop.PropertyType);
                }
                else
                {
                    Type genericTypeDefinition = prop.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                    {
                        obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, Nullable.GetUnderlyingType(prop.PropertyType));
                    }
                }

                prop.SetValue(entity, obj, null);
            }

            return entity;
        }

        /// <summary>
        /// List集合字典对象转换成集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resultList"></param>
        /// <returns></returns>
        public static IList<T> ListToObject<T>(IList<IDictionary<string,object>> resultList) where T:new()
        {
            IList<T> list = new List<T>();

            foreach (var result in resultList)
            {
                list.Add(DicToObject<T>(result));
            }

            return list;
        }

        /// <summary>
        /// list集合字典对象转换成集合对象
        /// </summary>
        /// <param name="resultList"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static dynamic ListToObject(IList<IDictionary<string,object>> resultList,Type type)
        {
            Type listType = typeof(List<>).MakeGenericType(type);
            dynamic value = Activator.CreateInstance(listType);
            foreach (var result in resultList)
            {
                value.Add(DicToObject(result, type));
            }
            return value;
        }
    }
}
