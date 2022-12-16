using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ReflectionExtensions
/// </summary>
public static class ReflectionExtensions
{
    #region Functions

    #region HasProperty

    /// <summary>
    /// HasProperty
    /// </summary>
    /// <returns></returns>
    public static bool HasProperty(this object source, string propertyName, bool ignoringCase = false)
    {
        if (propertyName.IsNullOrEmpty()) throw new Exception("必须指定属性名");

        var result = false;
        var type = source.GetType();
        var properties = type.GetProperties();

        foreach (var p in properties)
            if (!ignoringCase)
            {
                if (p.Name != propertyName) continue;

                result = true;

                break;
            }
            else
            {
                if (p.Name.ToLower() != propertyName.ToLower()) continue;

                result = true;

                break;
            }

        return result;
    }

    #endregion

    #region SetValueByPropertyName

    /// <summary>
    /// SetValueByPropertyName
    /// </summary>
    /// <returns></returns>
    public static void SetValueByPropertyName(
        this object source,
        string propertyName,
        object propertyValue,
        bool ignoringCase = false
    )
    {
        if (source.HasProperty(propertyName, ignoringCase))
        {
            var properties = source.GetType().GetProperties();

            foreach (var p in properties)
                if (!ignoringCase)
                {
                    if (p.Name != propertyName) continue;

                    var v = Convert.ChangeType(propertyValue, p.PropertyType);

                    p.SetValue(source, v, null);

                    break;
                }
                else
                {
                    if (p.Name.ToLower() != propertyName.ToLower()) continue;

                    var v = Convert.ChangeType(propertyValue, p.PropertyType);

                    p.SetValue(source, v, null);

                    break;
                }
        }
        else
        {
            throw new Exception("该属性不存在");
        }
    }

    #endregion

    #region GetValueByPropertyName

    /// <summary>
    /// GetValueByPropertyName
    /// </summary>
    /// <returns></returns>
    public static T GetValueByPropertyName<T>(this object source, string propertyName, bool ignoringCase = false)
    {
        if (!source.HasProperty(propertyName, ignoringCase)) throw new Exception("该属性不存在");

        var type = typeof(T);

        var properties = source.GetType().GetProperties();

        foreach (var p in properties)
            if (!ignoringCase)
            {
                if (p.Name != propertyName) continue;

                var v = p.GetValue(source, null);

                var r = Convert.ChangeType(v, type);

                if (r == null)
                    throw new Exception("Convert.ChangeType not allow return null in GetValueByPropertyName");

                return (T)r;
            }
            else
            {
                if (!string.Equals(p.Name, propertyName, StringComparison.CurrentCultureIgnoreCase)) continue;

                var v = p.GetValue(source, null);

                var r = Convert.ChangeType(v, type);

                if (r == null)
                    throw new Exception("Convert.ChangeType not allow return null in GetValueByPropertyName");

                return (T)r;
            }

        throw new Exception("该属性不存在");
    }

    #endregion

    #region CopyPropertyTo

    /// <summary>
    /// 复制属性值到目标
    /// </summary>
    /// <returns></returns>
    public static void CopyPropertyTo<T>(this object sources, ref T target) where T : class
    {
        var type = sources.GetType();
        var propertyInfos = target.GetType().GetProperties();
        var properties = type.GetProperties();

        foreach (var tp in propertyInfos)
        foreach (var p in properties)
        {
            if (tp.Name != p.Name || tp.PropertyType != p.PropertyType) continue;

            var v = p.GetValue(sources, null);

            tp.SetValue(target, v, null);
        }
    }

    #endregion

    #region EqualsByProperty

    /// <summary>
    /// 通过比较属性值判断两者属性一不一致
    /// </summary>
    /// <param name="sources"></param>
    /// <param name="target"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool EqualsByProperty<T>(this T sources, object target) where T : class?
    {
        var sourceType = sources?.GetType();
        var targetType = target.GetType();
        var sourcePropertyList = sourceType?.GetProperties();
        var targetPropertyList = targetType.GetProperties();

        if (sourcePropertyList == null) return false;

        foreach (var propertyItem in sourcePropertyList)
        {
            var v = propertyItem.GetValue(sources, null);

            if (v == null) continue;

            var contain = false;

            foreach (var t in targetPropertyList)
            {
                //比较属性和属性类型
                if (t.Name != propertyItem.Name || t.PropertyType != propertyItem.PropertyType) continue;

                contain = true;

                var tv = t.GetValue(target, null);

                if (tv == null) continue;

                if (v.IsDefinedBaseType())
                {
                    if (!v.Equals(tv)) return false;
                }
                else
                {
                    if (!v.EqualsByProperty(tv)) return false;
                }
            }

            if (!contain) return false;
        }

        return true;
    }

    #endregion

    #region GetClone

    /// <summary>
    /// 获取Array深度复制副本
    /// </summary>
    /// <param name="sources"></param>
    /// <returns></returns>
    private static Array GetCloneByArray(this Array sources)
    {
        var result = sources.Clone() as Array ?? Array.Empty<object>();

        if (sources.Length <= 0) return result;

        var first = sources.GetValue(0);

        if (first == null) return result;

        result = Array.CreateInstance(first.GetType(), sources.Length);

        for (var index = 0; index < sources.Length; index++)
        {
            var item = sources.GetValue(index);
            result.SetValue(item, index);
        }

        return result;
    }

    /// <summary>
    /// 获取IList深度复制副本
    /// </summary>
    /// <param name="sources"></param>
    /// <returns></returns>
    private static IList GetCloneByList(this IEnumerable? sources)
    {
        if (sources == null) return new ArrayList();

        var result = sources.GetType().Create() as IList ?? new ArrayList();

        foreach (var item in sources) result.Add(item.GetClone());

        return result;
    }

    /// <summary>
    /// 获取深度复制副本,对于引用,复制后的结果与与结果内存地址不相同
    /// </summary>
    /// <returns></returns>
    public static T GetClone<T>(this T sources) where T : class
    {
        var type = sources.GetType();
        var typeName = type.FullName;

        if (typeName == null) throw new Exception("typeName is null");

        //判断数组
        if (type.IsArray)
        {
            if (sources is not Array array) throw new Exception("error");

            if (GetCloneByArray(array) is not T result) throw new Exception("GetClone result is null");

            return result;
        }

        //判断集合等组合类型
        var interfaces = type.GetInterfaces();
        var collectionType = new List<string> { "IList" };
        var isCollection = interfaces.Any(inter => collectionType.Contains(inter.Name));

        if (isCollection)
        {
            var result = GetCloneByList(sources as IList) as T;

            if (result == null) throw new Exception("GetClone result is null");

            return result;
        }

        //一般实体属性复制
        var obj = type.Assembly.CreateInstance(typeName) as T;

        var properties = type.GetProperties();

        foreach (var propertyInfo in properties)
        {
            var value = propertyInfo.GetValue(sources, null);
            propertyInfo.SetValue(obj, value, null);
        }

        if (obj == null) throw new Exception("GetClone result is null");

        return obj;
    }

    #endregion

    #region Attribute

    /// <summary>
    /// Gets the attribute from the item
    /// </summary>
    /// <typeparam name="T">Attribute type</typeparam>
    /// <param name="provider">Attribute provider</param>
    /// <param name="inherit">When true, it looks up the heirarchy chain for the inherited custom attributes</param>
    /// <returns>Attribute specified if it exists</returns>
    public static T? Attribute<T>(this ICustomAttributeProvider provider, bool inherit = true) where T : Attribute
    {
        return provider.IsDefined(typeof(T), inherit) ? provider.Attributes<T>(inherit)[0] : default;
    }

    #endregion

    #region Attributes

    /// <summary>
    /// Gets the attributes from the item
    /// </summary>
    /// <typeparam name="T">Attribute type</typeparam>
    /// <param name="provider">Attribute provider</param>
    /// <param name="inherit">When true, it looks up the heirarchy chain for the inherited custom attributes</param>
    /// <returns>Array of attributes</returns>
    public static T?[] Attributes<T>(this ICustomAttributeProvider provider, bool inherit = true)
        where T : Attribute
    {
        return provider.IsDefined(typeof(T), inherit)
            ? provider.GetCustomAttributes(typeof(T), inherit).ToArray(x => (T)x)
            : Array.Empty<T>();
    }

    #endregion

    #region Call

    /// <summary>
    /// Calls a method on an object
    /// </summary>
    /// <param name="methodName">Method name</param>
    /// <param name="source">Object to call the method on</param>
    /// <param name="inputVariables">(Optional)input variables for the method</param>
    /// <typeparam name="TReturnType">Return type expected</typeparam>
    /// <returns>The returned value of the method</returns>
    public static TReturnType Call<TReturnType>(
        this object source,
        string methodName,
        params object[] inputVariables
    )
    {
        if (source.IsNull()) throw new ArgumentNullException(nameof(source));

        if (methodName.IsNullOrEmpty()) throw new ArgumentNullException(nameof(methodName));

        var objectType = source.GetType();
        var methodInputTypes = new Type[inputVariables.Length];
        for (var x = 0; x < inputVariables.Length; ++x) methodInputTypes[x] = inputVariables[x].GetType();

        var method = objectType.GetMethod(methodName, methodInputTypes);

        if (method == null)
            throw new InvalidOperationException(
                "Could not find method " + methodName + " with the appropriate input variables."
            );

        return (TReturnType)method.Invoke(source, inputVariables)!;
    }

    #endregion

    #region Create

    /// <summary>
    /// Creates an instance of the type and casts it to the specified type
    /// </summary>
    /// <typeparam name="TClassType">Class type to return</typeparam>
    /// <param name="type">Type to create an instance of</param>
    /// <param name="args">Arguments sent into the constructor</param>
    /// <returns>The newly created instance of the type</returns>
    public static TClassType Create<TClassType>(this Type type, params object[] args)
    {
        if (type.IsNull()) throw new ArgumentNullException(nameof(type));

        return (TClassType)type.Create(args)!;
    }

    /// <summary>
    /// Creates an instance of the type
    /// </summary>
    /// <param name="type">Type to create an instance of</param>
    /// <param name="args">Arguments sent into the constructor</param>
    /// <returns>The newly created instance of the type</returns>
    public static object? Create(this Type type, params object[] args)
    {
        if (type.IsNull()) throw new ArgumentNullException(nameof(type));

        return Activator.CreateInstance(type, args);
    }

    /// <summary>
    /// Creates an instance of the types and casts it to the specified type
    /// </summary>
    /// <typeparam name="TClassType">Class type to return</typeparam>
    /// <param name="types">Types to create an instance of</param>
    /// <param name="args">Arguments sent into the constructor</param>
    /// <returns>The newly created instance of the types</returns>
    public static IEnumerable<TClassType?> Create<TClassType>(this IEnumerable<Type> types, params object[] args)
    {
        if (types.IsNull()) throw new ArgumentNullException(nameof(types));

        return types.ForEach(x => x.Create<TClassType>(args));
    }

    /// <summary>
    /// Creates an instance of the types specified
    /// </summary>
    /// <param name="types">Types to create an instance of</param>
    /// <param name="args">Arguments sent into the constructor</param>
    /// <returns>The newly created instance of the types</returns>
    public static IEnumerable<object?> Create(this IEnumerable<Type> types, params object[] args)
    {
        if (types.IsNull()) throw new ArgumentNullException(nameof(types));

        return types.ForEach(x => x.Create(args));
    }

    #endregion

    #region GetName

    /// <summary>
    /// Returns the type's name (Actual C# name, not the funky version from
    /// the Name property)
    /// </summary>
    /// <param name="objectType">Type to get the name of</param>
    /// <returns>string name of the type</returns>
    public static string GetName(this Type objectType)
    {
        if (objectType.IsNull()) throw new ArgumentNullException(nameof(objectType));

        var output = new StringBuilder();
        if (objectType.Name == "Void")
        {
            output.Append("void");
        }
        else
        {
            if (objectType.Name.Contains('`'))
            {
                var genericTypes = objectType.GetGenericArguments();
                output.Append(
                        objectType.Name.Remove(objectType.Name.IndexOf("`", StringComparison.InvariantCulture))
                    )
                    .Append('<');
                var seperator = "";

                foreach (var genericType in genericTypes)
                {
                    output.Append(seperator).Append(genericType.GetName());
                    seperator = ",";
                }

                output.Append('>');
            }
            else
            {
                output.Append(objectType.Name);
            }
        }

        return output.ToString();
    }

    #endregion

    #region HasDefaultConstructor

    /// <summary>
    /// Determines if the type has a default constructor
    /// </summary>
    /// <param name="type">Type to check</param>
    /// <returns>True if it does, false otherwise</returns>
    public static bool HasDefaultConstructor(this Type type)
    {
        if (type.IsNull()) throw new ArgumentNullException(nameof(type));

        return type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
            .Any(x => x.GetParameters().Length == 0);
    }

    #endregion

    #region Is

    /// <summary>
    /// Determines if an object is of a specific type
    /// </summary>
    /// <param name="source">Object</param>
    /// <param name="type">Type</param>
    /// <returns>True if it is, false otherwise</returns>
    public static bool Is(this object source, Type type)
    {
        if (source.IsNull()) throw new ArgumentNullException(nameof(source));

        if (type.IsNull()) throw new ArgumentNullException(nameof(type));

        return source.GetType().Is(type);
    }

    /// <summary>
    /// Determines if an object is of a specific type
    /// </summary>
    /// <param name="objectType">Object type</param>
    /// <param name="type">Type</param>
    /// <returns>True if it is, false otherwise</returns>
    public static bool Is(this Type? objectType, Type type)
    {
        if (type.IsNull()) throw new ArgumentNullException(nameof(type));

        if (objectType == null) return false;

        if (type == objectType || objectType.GetInterfaces().Any(x => x == type)) return true;

        return objectType.BaseType != null && objectType.BaseType.Is(type);
    }

    /// <summary>
    /// 是系统已经定义的基础非object类型
    /// </summary>
    /// <param name="obj">要判断的实体</param>
    /// <returns></returns>
    public static bool IsDefinedBaseType<T>(this T obj)
    {
        var type = obj?.GetType();

        return type != null && type.IsDefinedBaseType();
    }

    /// <summary>
    /// 是系统已经定义的基础非object类型
    /// </summary>
    /// <param name="type">要判断的类型</param>
    /// <returns></returns>
    public static bool IsDefinedBaseType(this Type type)
    {
        var result = false;
        var typeCode = Type.GetTypeCode(type);

        switch (typeCode)
        {
            case TypeCode.Boolean:
                result = true;
                break;
            case TypeCode.Byte:
                result = true;
                break;
            case TypeCode.Char:
                result = true;
                break;
            case TypeCode.DateTime:
                result = true;
                break;
            case TypeCode.DBNull:
                result = true;
                break;
            case TypeCode.Decimal:
                result = true;
                break;
            case TypeCode.Double:
                result = true;
                break;
            case TypeCode.Empty:
                result = true;
                break;
            case TypeCode.Int16:
                result = true;
                break;
            case TypeCode.Int32:
                result = true;
                break;
            case TypeCode.Int64:
                result = true;
                break;
            case TypeCode.SByte:
                result = true;
                break;
            case TypeCode.Single:
                result = true;
                break;
            case TypeCode.String:
                result = true;
                break;
            case TypeCode.UInt16:
                result = true;
                break;
            case TypeCode.UInt32:
                result = true;
                break;
            case TypeCode.UInt64:
                result = true;
                break;
        }

        return result;
    }

    #endregion

    #region Load

    /// <summary>
    /// Loads an assembly by its name
    /// </summary>
    /// <param name="name">Name of the assembly to return</param>
    /// <returns>The assembly specified if it exists</returns>
    public static Assembly? Load(this AssemblyName name)
    {
        if (name.IsNull()) throw new ArgumentNullException(nameof(name));

        try
        {
            return AppDomain.CurrentDomain.Load(name);
        }
        catch (BadImageFormatException)
        {
            return null;
        }
    }

    #endregion

    #region LoadAssemblies

    /// <summary>
    /// Loads assemblies within a directory and returns them in an array.
    /// </summary>
    /// <param name="directory">The directory to search in</param>
    /// <param name="recursive">Determines whether to search recursively or not</param>
    /// <returns>Array of assemblies in the directory</returns>
    public static IEnumerable<Assembly> LoadAssemblies(this DirectoryInfo directory, bool recursive = false)
    {
        var assemblies = new List<Assembly>();

        foreach (var file in directory.GetFiles(
                     "*.dll",
                     recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
                 ))
            try
            {
                var assembly = AssemblyName.GetAssemblyName(file.FullName).Load();

                if (assembly == null) continue;

                assemblies.Add(assembly);
            }
            catch (BadImageFormatException)
            {
            }

        return assemblies;
    }

    #endregion

    #region MarkedWith

    /// <summary>
    /// Goes through a list of types and determines if they're marked with a specific attribute
    /// </summary>
    /// <typeparam name="T">Attribute type</typeparam>
    /// <param name="types">Types to check</param>
    /// <param name="inherit">When true, it looks up the heirarchy chain for the inherited custom attributes</param>
    /// <returns>The list of types that are marked with an attribute</returns>
    public static IEnumerable<Type>? MarkedWith<T>(
        this IEnumerable<Type>? types,
        bool inherit = true
    )
        where T : Attribute
    {
        return types?.Where(x => x.IsDefined(typeof(T), inherit) && !x.IsAbstract);
    }

    #endregion

    #region MakeShallowCopy

    /// <summary>
    /// Makes a shallow copy of the object
    /// </summary>
    /// <param name="source">Object to copy</param>
    /// <param name="simpleTypesOnly">If true, it only copies simple types (no classes, only items like int, string, etc.), false copies everything.</param>
    /// <returns>A copy of the object</returns>
    public static T? MakeShallowCopy<T>(this T source, bool simpleTypesOnly = false)
    {
        if (source == null) return default;

        var objectType = source.GetType();
        var classInstance = objectType.Create<T>();

        foreach (var property in objectType.GetProperties())
            if (property.CanRead
                && property.CanWrite
                && simpleTypesOnly
                && property.PropertyType.IsValueType)
                property.SetValue(classInstance, property.GetValue(source, null), null);
            else if (!simpleTypesOnly
                     && property.CanRead
                     && property.CanWrite)
                property.SetValue(classInstance, property.GetValue(source, null), null);

        foreach (var field in objectType.GetFields())
            if (simpleTypesOnly && field.IsPublic)
                field.SetValue(classInstance, field.GetValue(source));
            else if (!simpleTypesOnly && field.IsPublic) field.SetValue(classInstance, field.GetValue(source));

        return classInstance;
    }

    #endregion

    #region Objects

    /// <summary>
    /// Returns an instance of all classes that it finds within an assembly
    /// that are of the specified base type/interface.
    /// </summary>
    /// <typeparam name="TClassType">Base type/interface searching for</typeparam>
    /// <param name="assembly">Assembly to search within</param>
    /// <returns>A list of objects that are of the type specified</returns>
    public static IEnumerable<TClassType?> Objects<TClassType>(this Assembly assembly)
    {
        if (assembly.IsNull()) throw new ArgumentNullException(nameof(assembly));

        return assembly.Types<TClassType>().Where(x => !x.ContainsGenericParameters).Create<TClassType>();
    }

    /// <summary>
    /// Returns an instance of all classes that it finds within a group of assemblies
    /// that are of the specified base type/interface.
    /// </summary>
    /// <typeparam name="TClassType">Base type/interface searching for</typeparam>
    /// <param name="assemblies">Assemblies to search within</param>
    /// <returns>A list of objects that are of the type specified</returns>
    public static IEnumerable<TClassType?> Objects<TClassType>(this IEnumerable<Assembly> assemblies)
    {
        if (assemblies.IsNull()) throw new ArgumentNullException(nameof(assemblies));

        var returnValues = new List<TClassType?>();

        foreach (var assembly in assemblies) returnValues.AddRange(assembly.Objects<TClassType>());

        return returnValues;
    }

    /// <summary>
    /// Returns an instance of all classes that it finds within a directory
    /// that are of the specified base type/interface.
    /// </summary>
    /// <typeparam name="TClassType">Base type/interface searching for</typeparam>
    /// <param name="directory">Directory to search within</param>
    /// <param name="recursive">Should this be recursive</param>
    /// <returns>A list of objects that are of the type specified</returns>
    public static IEnumerable<TClassType?> Objects<TClassType>(this DirectoryInfo directory, bool recursive = false)
    {
        if (directory.IsNull()) throw new ArgumentNullException(nameof(directory));

        return directory.LoadAssemblies(recursive).Objects<TClassType>();
    }

    #endregion

    #region Property

    /// <summary>
    /// Gets the value of property
    /// </summary>
    /// <param name="source">The object to get the property of</param>
    /// <param name="property">The property to get</param>
    /// <returns>Returns the property's value</returns>
    public static object? Property(this object source, PropertyInfo? property)
    {
        if (source.IsNull()) throw new ArgumentNullException(nameof(source));

        if (property.IsNull()) throw new ArgumentNullException(nameof(property));

        return property?.GetValue(source, null);
    }

    /// <summary>
    /// Gets the value of property
    /// </summary>
    /// <param name="source">The object to get the property of</param>
    /// <param name="property">The property to get</param>
    /// <returns>Returns the property's value</returns>
    public static object? Property(this object source, string property)
    {
        if (source.IsNull()) throw new ArgumentNullException(nameof(source));

        if (property.IsNullOrEmpty()) throw new ArgumentNullException(nameof(property));

        var properties = property.Split(new[] { "." }, StringSplitOptions.None);
        var tempObject = source;
        var tempObjectType = tempObject.GetType();
        PropertyInfo? destinationProperty;

        for (var x = 0; x < properties.Length - 1; ++x)
        {
            destinationProperty = tempObjectType.GetProperty(properties[x]);

            if (destinationProperty == null) continue;

            tempObjectType = destinationProperty.PropertyType;
            tempObject = destinationProperty.GetValue(tempObject, null);

            if (tempObject == null) return null;
        }

        destinationProperty = tempObjectType.GetProperty(properties[properties.Length - 1]);

        return tempObject.Property(destinationProperty);
    }

    #endregion

    #region PropertyGetter

    /// <summary>
    /// Gets a lambda expression that calls a specific property's getter function
    /// </summary>
    /// <typeparam name="TClassType">Class type</typeparam>
    /// <typeparam name="TDataType">Data type expecting</typeparam>
    /// <param name="property">Property</param>
    /// <returns>A lambda expression that calls a specific property's getter function</returns>
    public static Expression<Func<TClassType, TDataType>> PropertyGetter<TClassType, TDataType>(
        this PropertyInfo property)
    {
        if (!property.PropertyType.Is(typeof(TDataType)))
            throw new ArgumentException("Property is not of the type specified");

        if (property.DeclaringType != null && !property.DeclaringType.Is(typeof(TClassType)) &&
            !typeof(TClassType).Is(property.DeclaringType))
            throw new ArgumentException("Property is not from the declaring class type specified");

        if (property.DeclaringType == null) throw new Exception("property.DeclaringType is null");

        var objectInstance = Expression.Parameter(property.DeclaringType, "x");
        var propertyGet = Expression.Property(objectInstance, property);

        if (property.PropertyType == typeof(TDataType))
            return Expression.Lambda<Func<TClassType, TDataType>>(propertyGet, objectInstance);

        var convert = Expression.Convert(propertyGet, typeof(TDataType));

        return Expression.Lambda<Func<TClassType, TDataType>>(convert, objectInstance);
    }

    /// <summary>
    /// Gets a lambda expression that calls a specific property's getter function
    /// </summary>
    /// <typeparam name="TClassType">Class type</typeparam>
    /// <param name="property">Property</param>
    /// <returns>A lambda expression that calls a specific property's getter function</returns>
    public static Expression<Func<TClassType, object>> PropertyGetter<TClassType>(this PropertyInfo property)
    {
        return property.PropertyGetter<TClassType, object>();
    }

    #endregion

    #region PropertyName

    /// <summary>
    /// Gets a property name
    /// </summary>
    /// <param name="expression">LINQ expression</param>
    /// <returns>The name of the property</returns>
    public static string PropertyName(this LambdaExpression expression)
    {
        if (expression.Body is UnaryExpression unaryExpression &&
            unaryExpression.NodeType == ExpressionType.Convert)
        {
            var temp = (MemberExpression)unaryExpression.Operand;

            if (temp.Expression != null) return temp.Expression.PropertyName() + temp.Member.Name;
        }

        if (expression.Body is not MemberExpression memberExpression)
            throw new ArgumentException("Expression.Body is not a MemberExpression");

        if (memberExpression.Expression != null)
            return memberExpression.Expression.PropertyName() +
                   memberExpression.Member.Name;

        throw new Exception("memberExpression.Expression is null");
    }

    /// <summary>
    /// Gets a property name
    /// </summary>
    /// <param name="expression">LINQ expression</param>
    /// <returns>The name of the property</returns>
    public static string PropertyName(this Expression expression)
    {
        if (expression is not MemberExpression tempExpression) return "";

        if (tempExpression.Expression != null)
            return tempExpression.Expression.PropertyName() + tempExpression.Member.Name + ".";

        throw new Exception("tempExpression.Expression is null");
    }

    #endregion

    #region PropertySetter

    /// <summary>
    /// Gets a lambda expression that calls a specific property's setter function
    /// </summary>
    /// <typeparam name="TClassType">Class type</typeparam>
    /// <typeparam name="TDataType">Data type expecting</typeparam>
    /// <param name="property">Property</param>
    /// <returns>A lambda expression that calls a specific property's setter function</returns>
    public static Expression<Action<TClassType, TDataType>> PropertySetter<TClassType, TDataType>(
        this Expression<Func<TClassType, TDataType>> property
    )
    {
        if (property.IsNull()) throw new ArgumentNullException(nameof(property));

        var propertyName = property.PropertyName();
        var splitName = propertyName.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
        var propertyInfo = typeof(TClassType).GetProperty(splitName[0]);

        if (propertyInfo == null) throw new Exception("propertyInfo is null");

        if (propertyInfo.DeclaringType == null) throw new Exception("propertyInfo.DeclaringType is null");

        var objectInstance = Expression.Parameter(propertyInfo.DeclaringType, "x");
        var propertySet = Expression.Parameter(typeof(TDataType), "y");
        MethodCallExpression setterCall;
        MemberExpression? propertyGet = null;

        if (splitName.Length > 1)
        {
            propertyGet = Expression.Property(objectInstance, propertyInfo);

            for (var x = 1; x < splitName.Length - 1; ++x)
            {
                propertyInfo = propertyInfo?.PropertyType.GetProperty(splitName[x]);

                if (propertyInfo != null) propertyGet = Expression.Property(propertyGet, propertyInfo);
            }

            propertyInfo = propertyInfo?.PropertyType.GetProperty(splitName[^1]);
        }

        if (propertyInfo == null) throw new Exception("propertyInfo is null");

        if (propertyInfo.PropertyType == null) throw new Exception("propertyInfo.PropertyType is null");

        var setMethod = propertyInfo.GetSetMethod();

        if (setMethod == null) throw new Exception("setMethod is null");

        if (propertyInfo.PropertyType != typeof(TDataType))
        {
            var convert = Expression.Convert(propertySet, propertyInfo.PropertyType);

            setterCall = propertyGet == null
                ? Expression.Call(objectInstance, setMethod, convert)
                : Expression.Call(propertyGet, setMethod, convert);

            return Expression.Lambda<Action<TClassType, TDataType>>(setterCall, objectInstance, propertySet);
        }

        setterCall = propertyGet == null
            ? Expression.Call(objectInstance, setMethod, propertySet)
            : Expression.Call(propertyGet, setMethod, propertySet);

        return Expression.Lambda<Action<TClassType, TDataType>>(setterCall, objectInstance, propertySet);
    }

    /// <summary>
    /// Gets a lambda expression that calls a specific property's setter function
    /// </summary>
    /// <typeparam name="TClassType">Class type</typeparam>
    /// <param name="property">Property</param>
    /// <returns>A lambda expression that calls a specific property's setter function</returns>
    public static Expression<Action<TClassType, object>> PropertySetter<TClassType>(
        this Expression<Func<TClassType, object>> property)
    {
        return property.PropertySetter<TClassType, object>();
    }

    #endregion

    #region PropertyType

    /// <summary>
    /// Gets a property's type
    /// </summary>
    /// <param name="source">object who contains the property</param>
    /// <param name="propertyPath">Path of the property (ex: Prop1.Prop2.Prop3 would be
    /// the Prop1 of the source object, which then has a Prop2 on it, which in turn
    /// has a Prop3 on it.)</param>
    /// <returns>The type of the property specified or null if it can not be reached.</returns>
    public static Type? PropertyType(this object? source, string propertyPath)
    {
        if (source == null || string.IsNullOrEmpty(propertyPath)) return null;

        return source.GetType().PropertyType(propertyPath);
    }

    /// <summary>
    /// Gets a property's type
    /// </summary>
    /// <param name="objectType">Object type</param>
    /// <param name="propertyPath">Path of the property (ex: Prop1.Prop2.Prop3 would be
    /// the Prop1 of the source object, which then has a Prop2 on it, which in turn
    /// has a Prop3 on it.)</param>
    /// <returns>The type of the property specified or null if it can not be reached.</returns>
    public static Type? PropertyType(this Type? objectType, string propertyPath)
    {
        if (objectType == null || string.IsNullOrEmpty(propertyPath)) return null;

        var sourceProperties = propertyPath.Split(new[] { "." }, StringSplitOptions.None);

        foreach (var t in sourceProperties)
        {
            var propertyInfo = objectType?.GetProperty(t);

            objectType = propertyInfo?.PropertyType;
        }

        return objectType;
    }

    #endregion

    #region ToString

    /// <summary>
    /// Gets the version information in a string format
    /// </summary>
    /// <param name="assembly">Assembly to get version information from</param>
    /// <param name="infoType">Version info type</param>
    /// <returns>The version information as a string</returns>
    public static string? ToString(this Assembly assembly, VersionInfo infoType)
    {
        if (assembly.IsNull()) throw new ArgumentNullException(nameof(assembly));

        if (!infoType.HasFlag(VersionInfo.ShortVersion)) return assembly.GetName().Version?.ToString();

        var version = assembly.GetName().Version;

        if (version != null) return version.Major + "." + version.Minor;

        throw new Exception("version is null");
    }

    /// <summary>
    /// Gets the version information in a string format
    /// </summary>
    /// <param name="assemblies">Assemblies to get version information from</param>
    /// <param name="infoType">Version info type</param>
    /// <returns>The version information as a string</returns>
    public static string ToString(this IEnumerable<Assembly> assemblies, VersionInfo infoType)
    {
        var builder = new StringBuilder();

        assemblies.OrderBy(x => x.FullName)
            .ForEach<Assembly>(x => builder.AppendLine(x.GetName().Name + ": " + x.ToString(infoType)));

        return builder.ToString();
    }

    /// <summary>
    /// Gets assembly information for all currently loaded assemblies
    /// </summary>
    /// <param name="assemblies">Assemblies to dump information from</param>
    /// <param name="htmlOutput">Should HTML output be used</param>
    /// <returns>An HTML formatted string containing the assembly information</returns>
    public static string ToString(this IEnumerable<Assembly> assemblies, bool htmlOutput)
    {
        var builder = new StringBuilder();

        builder.Append(htmlOutput ? "<strong>Assembly Information</strong><br />" : "Assembly Information\r\n");
        assemblies.ForEach<Assembly>(x => builder.Append(x.ToString(htmlOutput)));

        return builder.ToString();
    }

    /// <summary>
    /// Dumps the property names and current values from an object
    /// </summary>
    /// <param name="source">Object to dunp</param>
    /// <param name="htmlOutput">Determines if the output should be HTML or not</param>
    /// <returns>An HTML formatted table containing the information about the object</returns>
    public static string ToString(this object source, bool htmlOutput)
    {
        if (source.IsNull()) throw new ArgumentNullException(nameof(source));

        var tempValue = new StringBuilder();

        tempValue.Append(htmlOutput
            ? "<table><thead><tr><th>Property Name</th><th>Property Value</th></tr></thead><tbody>"
            : "Property Name\t\t\t\tProperty Value");

        var objectType = source.GetType();

        foreach (var property in objectType.GetProperties())
        {
            tempValue.Append(htmlOutput ? "<tr><td>" : "").Append(property.Name)
                .Append(htmlOutput ? "</td><td>" : "\t\t\t\t");
            var parameters = property.GetIndexParameters();
            if (property.CanRead && parameters.Length == 0)
            {
                var value = property.GetValue(source, null);
                tempValue.Append(value == null ? "null" : value.ToString());
            }

            tempValue.Append(htmlOutput ? "</td></tr>" : "");
        }

        tempValue.Append(htmlOutput ? "</tbody></table>" : "");
        return tempValue.ToString();
    }

    /// <summary>
    /// Dumps the properties names and current values
    /// from an object type (used for static classes)
    /// </summary>
    /// <param name="objectType">Object type to dunp</param>
    /// <param name="htmlOutput">Should this be output as an HTML string</param>
    /// <returns>An HTML formatted table containing the information about the object type</returns>
    public static string ToString(this Type objectType, bool htmlOutput)
    {
        if (objectType.IsNull()) throw new ArgumentNullException(nameof(objectType));

        var tempValue = new StringBuilder();

        tempValue.Append(htmlOutput
            ? "<table><thead><tr><th>Property Name</th><th>Property Value</th></tr></thead><tbody>"
            : "Property Name\t\t\t\tProperty Value");
        var properties = objectType.GetProperties();

        foreach (var property in properties)
        {
            tempValue.Append(htmlOutput ? "<tr><td>" : "").Append(property.Name)
                .Append(htmlOutput ? "</td><td>" : "\t\t\t\t");
            if (property.GetIndexParameters().Length == 0)
                tempValue.Append(property.GetValue(null, null) == null
                    ? "null"
                    : property.GetValue(null, null)?.ToString());

            tempValue.Append(htmlOutput ? "</td></tr>" : "");
        }

        tempValue.Append(htmlOutput ? "</tbody></table>" : "");

        return tempValue.ToString();
    }

    #endregion

    #region Types

    /// <summary>
    /// Gets a list of types based on an interface
    /// </summary>
    /// <param name="assembly">Assembly to check</param>
    /// <typeparam name="TBaseType">Class type to search for</typeparam>
    /// <returns>List of types that use the interface</returns>
    public static IEnumerable<Type> Types<TBaseType>(this Assembly assembly)
    {
        if (assembly.IsNull()) throw new ArgumentNullException(nameof(assembly));

        return assembly.Types(typeof(TBaseType));
    }

    /// <summary>
    /// Gets a list of types based on an interface
    /// </summary>
    /// <param name="assembly">Assembly to check</param>
    /// <param name="baseType">Base type to look for</param>
    /// <returns>List of types that use the interface</returns>
    public static IEnumerable<Type> Types(this Assembly assembly, Type baseType)
    {
        if (assembly.IsNull()) throw new ArgumentNullException(nameof(assembly));

        if (baseType.IsNull()) throw new ArgumentNullException(nameof(baseType));

        try
        {
            return assembly.GetTypes().Where(x => x.Is(baseType) && x.IsClass && !x.IsAbstract);
        }
        catch
        {
            return new List<Type>();
        }
    }

    /// <summary>
    /// Gets a list of types based on an interface
    /// </summary>
    /// <param name="assemblies">Assemblies to check</param>
    /// <typeparam name="TBaseType">Class type to search for</typeparam>
    /// <returns>List of types that use the interface</returns>
    public static IEnumerable<Type> Types<TBaseType>(this IEnumerable<Assembly> assemblies)
    {
        if (assemblies.IsNull()) throw new ArgumentNullException(nameof(assemblies));

        return assemblies.Types(typeof(TBaseType));
    }

    /// <summary>
    /// Gets a list of types based on an interface
    /// </summary>
    /// <param name="assemblies">Assemblies to check</param>
    /// <param name="baseType">Base type to look for</param>
    /// <returns>List of types that use the interface</returns>
    public static IEnumerable<Type> Types(this IEnumerable<Assembly> assemblies, Type baseType)
    {
        if (assemblies.IsNull()) throw new ArgumentNullException(nameof(assemblies));

        if (baseType.IsNull()) throw new ArgumentNullException(nameof(baseType));

        var returnValues = new List<Type>();

        assemblies.ForEach(y => returnValues.AddRange(y.Types(baseType)));

        return returnValues;
    }

    #endregion

    #region HasAttribute

    /// <summary>
    /// HasAttribute
    /// </summary>
    /// <param name="type"></param>
    /// <param name="attributeType"></param>
    /// <returns></returns>
    public static bool HasAttribute(this Type type, Type attributeType)
    {
        return type.IsDefined(attributeType, true);
    }

    /// <summary>
    /// HasAttribute
    /// </summary>
    /// <param name="type"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool HasAttribute<T>(this Type type) where T : Attribute
    {
        return type.HasAttribute(typeof(T));
    }

    /// <summary>
    /// HasAttribute
    /// </summary>
    /// <param name="type"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool HasAttribute<T>(this Type type, Func<T, bool> predicate) where T : Attribute
    {
        return type.GetCustomAttributes<T>(true).Any(predicate);
    }

    #endregion

    /// <summary>
    /// IsNotAbstractClass
    /// </summary>
    /// <param name="type"></param>
    /// <param name="publicOnly"></param>
    /// <returns></returns>
    public static bool IsNotAbstractClass(this Type type, bool publicOnly)
    {
        if (type.IsSpecialName)
            return false;

        if (type.IsClass && !type.IsAbstract)
        {
            if (type.HasAttribute<CompilerGeneratedAttribute>())
                return false;

            if (publicOnly)
                return type.IsPublic || type.IsNestedPublic;

            return true;
        }

        return false;
    }

    #endregion
}

/// <summary>
/// Version info
/// </summary>
public enum VersionInfo
{
    /// <summary>
    /// Short version
    /// </summary>
    ShortVersion = 1,

    /// <summary>
    /// Long version
    /// </summary>
    LongVersion = 2
}