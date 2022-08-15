using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using EasySoft.UtilityTools.ExtensionMethods;

namespace EasySoft.UtilityTools.Assists
{
    public static class ReflectionAssist
    {
        public static string GetClassName<T>(T o)
        {
            return o.GetClassName();
        }

        #region GetTypeName

        /// <summary>
        /// Returns the type's name
        /// </summary>
        /// <param name="objectType">object type</param>
        /// <returns>string name of the type</returns>
        public static string GetTypeName(Type objectType)
        {
            var output = new StringBuilder();

            if (objectType.Name == "Void")
            {
                output.Append("void");
            }
            else
            {
                if (objectType.Name.Contains("`"))
                {
                    var genericTypes = objectType.GetGenericArguments();

                    output.Append(objectType.Name.Remove(objectType.Name.IndexOf("`", StringComparison.Ordinal)))
                        .Append("<");

                    var seperator = "";

                    foreach (var genericType in genericTypes)
                    {
                        output.Append(seperator).Append(GetTypeName(genericType));
                        seperator = ",";
                    }

                    output.Append(">");
                }
                else
                {
                    output.Append(objectType.Name);
                }
            }

            return output.ToString();
        }

        #endregion GetTypeName

        #region GetPropertyName

        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            return GetPropertyName(propertyLambda, out _, out _);
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyLambda)
        {
            return GetPropertyName(propertyLambda, out var _, out var _);
        }

        public static string GetPropertyName<T>(
            Expression<Func<T>> propertyLambda,
            out Type? type,
            out PropertyInfo? propertyInfo
        )
        {
            dynamic? me;
            propertyInfo = null;

            if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                me = propertyLambda.Body as MemberExpression;

                if (me == null)
                {
                    throw new ArgumentException(
                        "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                    );
                }

                var classWithMemberAccess = me.Expression;

                type = null;

                if (me.Member != null)
                {
                    if (me.Member.PropertyType != null)
                    {
                        type = classWithMemberAccess.Type;

                        propertyInfo = me.Member as PropertyInfo;

                        if (propertyInfo != null)
                        {
                            return propertyInfo.Name;
                        }

                        throw new ArgumentException("PropertyInfo is null'");
                    }
                }
            }

            if (propertyLambda.Body.NodeType != ExpressionType.Convert)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (!(propertyLambda.Body is UnaryExpression cov))
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );
            }

            var classLam = me.Expression;

            type = null;

            if (me.Member == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (me.Member.PropertyType == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            type = classLam.Type;

            propertyInfo = me.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return propertyInfo.Name;
            }

            throw new ArgumentException("PropertyInfo is null'");
        }

        public static string GetPropertyName<T>(
            Expression<Func<T, object>> propertyLambda,
            out Type? type,
            out PropertyInfo? propertyInfo
        )
        {
            dynamic? me;
            propertyInfo = null;

            if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                me = propertyLambda.Body as MemberExpression;

                if (me == null)
                {
                    throw new ArgumentException(
                        "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'"
                    );
                }

                var classWithMemberAccess = me.Expression;

                type = null;

                if (me.Member != null)
                {
                    if (me.Member.PropertyType != null)
                    {
                        type = classWithMemberAccess.Type;

                        propertyInfo = me.Member as PropertyInfo;

                        if (propertyInfo != null)
                        {
                            return propertyInfo.Name;
                        }

                        throw new ArgumentException("PropertyInfo is null'");
                    }
                }
            }

            if (propertyLambda.Body.NodeType != ExpressionType.Convert)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (!(propertyLambda.Body is UnaryExpression cov))
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'");
            }

            var classLam = me.Expression;

            type = null;

            if (me.Member == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (me.Member.PropertyType == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            type = classLam.Type;

            propertyInfo = me.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return propertyInfo.Name;
            }

            throw new ArgumentException("PropertyInfo is null'");
        }

        #endregion

        #region GetClassAndPropertyName

        /// <summary>
        /// GetClassAndPropertyName
        /// </summary>
        /// <param name="propertyLambda"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetClassAndPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            return GetClassAndPropertyName(propertyLambda, out _, out _);
        }

        /// <summary>
        /// GetClassAndPropertyName
        /// </summary>
        /// <param name="propertyLambda"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetClassAndPropertyName<T>(Expression<Func<T, object>> propertyLambda)
        {
            return GetClassAndPropertyName(propertyLambda, out _, out _);
        }

        /// <summary>
        /// GetClassAndPropertyName
        /// </summary>
        /// <param name="propertyLambda"></param>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetClassAndPropertyName<T>(
            Expression<Func<T>> propertyLambda,
            out Type? type,
            out PropertyInfo? propertyInfo
        )
        {
            dynamic? me;
            propertyInfo = null;

            if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                me = propertyLambda.Body as MemberExpression;

                if (me == null)
                {
                    throw new ArgumentException(
                        "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
                }

                var classWithMemberAccess = me.Expression;

                type = null;

                if (me.Member != null)
                {
                    if (me.Member.PropertyType != null)
                    {
                        type = classWithMemberAccess.Type;

                        propertyInfo = me.Member as PropertyInfo;

                        if (propertyInfo != null)
                        {
                            return type.Name + "." + propertyInfo.Name;
                        }

                        throw new ArgumentException("PropertyInfo is null'");
                    }
                }
            }

            if (propertyLambda.Body.NodeType != ExpressionType.Convert)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (!(propertyLambda.Body is UnaryExpression cov))
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'");
            }

            var classLam = me.Expression;

            type = null;

            if (me.Member == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (me.Member.PropertyType == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            type = classLam.Type;

            propertyInfo = me.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return type.Name + "." + propertyInfo.Name;
            }

            throw new ArgumentException("PropertyInfo is null'");
        }

        /// <summary>
        /// GetClassAndPropertyName
        /// </summary>
        /// <param name="propertyLambda"></param>
        /// <param name="type"></param>
        /// <param name="propertyInfo"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string GetClassAndPropertyName<T>(
            Expression<Func<T, object>> propertyLambda,
            out Type? type,
            out PropertyInfo? propertyInfo
        )
        {
            dynamic? me;
            propertyInfo = null;

            if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                me = propertyLambda.Body as MemberExpression;

                if (me == null)
                {
                    throw new ArgumentException(
                        "You must pass a lambda of the form: '() => Class.Property' or '() => object.Property'");
                }

                var classWithMemberAccess = me.Expression;

                type = null;

                if (me.Member != null)
                {
                    if (me.Member.PropertyType != null)
                    {
                        type = classWithMemberAccess.Type;

                        propertyInfo = me.Member as PropertyInfo;

                        if (propertyInfo != null)
                        {
                            return type.Name + "." + propertyInfo.Name;
                        }

                        throw new ArgumentException("PropertyInfo is null'");
                    }
                }
            }

            if (propertyLambda.Body.NodeType != ExpressionType.Convert)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (!(propertyLambda.Body is UnaryExpression cov))
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            me = cov.Operand as MemberExpression;

            if (me == null)
            {
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );
            }

            var classLam = me.Expression;

            type = null;

            if (me.Member == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            if (me.Member.PropertyType == null)
            {
                throw new ArgumentException("Cannot analyze type get name ");
            }

            type = classLam.Type;

            propertyInfo = me.Member as PropertyInfo;

            if (propertyInfo != null)
            {
                return type.Name + "." + propertyInfo.Name;
            }

            throw new ArgumentException("PropertyInfo is null'");
        }

        #endregion GetClassAndPropertyName
    }
}