using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using HHECS.Model.Entities;
using Expression = System.Linq.Expressions.Expression;

namespace HHECS.Model.Common
{
    public static class CommonHelper
    {
        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public static List<string> GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                return IpEntry.AddressList.Where(t => t.AddressFamily == AddressFamily.InterNetwork).Select(t => t.ToString()).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ///// <summary>
        ///// 枚举转字典集合
        ///// </summary>
        ///// <typeparam name="T">枚举类名称</typeparam>
        ///// <param name="keyDefault">默认key值</param>
        ///// <param name="valueDefault">默认value值</param>
        ///// <returns>返回生成的字典集合</returns>
        //public static Dictionary<string, object> EnumListDic<T>(string keyDefault, string valueDefault = "")
        //{
        //    Dictionary<string, object> dicEnum = new Dictionary<string, object>();
        //    Type enumType = typeof(T);
        //    if (!enumType.IsEnum)
        //    {
        //        return dicEnum;
        //    }
        //    if (!string.IsNullOrEmpty(keyDefault)) //判断是否添加默认选项
        //    {
        //        dicEnum.Add(keyDefault, valueDefault);
        //    }
        //    string[] fieldstrs = Enum.GetNames(enumType); //获取枚举字段数组
        //    foreach (var item in fieldstrs)
        //    {
        //        string description = string.Empty;
        //        var field = enumType.GetField(item);
        //        object[] arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true); //获取属性字段数组
        //        if (arr != null && arr.Length > 0)
        //        {
        //            description = ((DescriptionAttribute)arr[0]).Description;   //属性描述
        //        }
        //        else
        //        {
        //            description = item;  //描述不存在取字段名称
        //        }
        //        dicEnum.Add(description, (int)Enum.Parse(enumType, item));  //不用枚举的value值作为字典key值的原因从枚举例子能看出来，其实这边应该判断他的值不存在，默认取字段名称
        //    }
        //    return dicEnum;
        //}

        /// <summary>
        /// 枚举转字典集合,值作为kye，描述（Description特性）作为value）
        /// </summary>
        /// <typeparam name="T">枚举类名称</typeparam>
        /// <param name="keyDefault">默认key值</param>
        /// <param name="valueDefault">默认value值</param>
        /// <returns>返回生成的字典集合</returns>
        public static Dictionary<string, string> EnumListDic<T>(string keyDefault = "", string valueDefault = "")
        {
            Dictionary<string, string> dicEnum = new Dictionary<string, string>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }
            //判断是否添加默认选项
            if (!string.IsNullOrEmpty(keyDefault))
            {
                dicEnum.Add(keyDefault, valueDefault);
            }
            //获取枚举字段数组
            string[] names = Enum.GetNames(enumType);
            //获取枚举值数组
            Array values = Enum.GetValues(enumType);
            for (var i = 0; i < names.Length; i++)
            {
                string description = string.Empty;
                var field = enumType.GetField(names[i]);
                //获取属性字段数组
                object[] arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (arr != null && arr.Length > 0)
                {
                    //属性描述
                    description = ((DescriptionAttribute)arr[0]).Description;
                }
                else
                {
                    //描述不存在取字段名称
                    description = names[i];
                }
                string key = Convert.ToInt32(values.GetValue(i)).ToString();
                //添加到字典中
                dicEnum.Add(key, description);
            }
            return dicEnum;
        }


        /// <summary>
        /// 将枚举转为List INT的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<int> EnumListInt<T>()
        {
            List<int> list = new List<int>();
            foreach (var e in Enum.GetNames(typeof(T)))
            {
                int v = (int)Enum.Parse(typeof(T), e.ToString());
                list.Add(v);
            }
            return list;
        }


        /// <summary>
        /// 将枚举转为List INT的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> EnumListString<T>()
        {
            List<string> list = new List<string>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                string v = ((int)Enum.Parse(typeof(T), e.ToString())).ToString();
                list.Add(v);
            }
            return list;
        }

        /// <summary>
        /// 对象克隆，反射
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="tIn"></param>
        /// <returns></returns>
        public static TOut TransReflection<TIn, TOut>(TIn tIn)
        {
            TOut tOut = Activator.CreateInstance<TOut>();
            var tInType = tIn.GetType();
            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                var itemIn = tInType.GetProperty(itemOut.Name); ;
                if (itemIn != null)
                {
                    try
                    {
                        itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return tOut;
        }

    }


    /// <summary>
    /// 对象克隆
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <typeparam name="TOut"></typeparam>
    public static class TransExpObject<TIn, TOut>
    {
        private static readonly Func<TIn, TOut> cache = GetFunc();
        private static Func<TIn, TOut> GetFunc()
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var item in typeof(TOut).GetProperties())
            {
                if (!item.CanWrite)
                    continue;

                MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                MemberBinding memberBinding = Expression.Bind(item, property);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
            Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[] { parameterExpression });

            return lambda.Compile();
        }

        public static TOut Trans(TIn tIn)
        {
            return cache(tIn);
        }
    }

}
