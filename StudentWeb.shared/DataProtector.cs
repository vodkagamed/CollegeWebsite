using Microsoft.AspNetCore.DataProtection;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace SchoolWebsite.shared;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

public class DataProtector
{
    private IDataProtector _dataProtector;

    public DataProtector(IDataProtectionProvider protectionProvider) => _dataProtector = protectionProvider.CreateProtector(GetType().FullName);

    public object Encrypt(object obj)
    {
        Type objectType = obj.GetType();
        PropertyInfo[] properties = objectType.GetProperties();
        object encryptedObject = Activator.CreateInstance(objectType);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj);
            if (value is string)
            {
                var encryptedValue = _dataProtector.Protect(value.ToString());
                prop.SetValue(encryptedObject, encryptedValue);
            }
            else
                prop.SetValue(encryptedObject, value);
        }

        return encryptedObject;
    }
    public object Decrypt(object obj)
    {
        Type objectType = obj.GetType();
        PropertyInfo[] properties = objectType.GetProperties();
        object decryptedObject = Activator.CreateInstance(objectType);

        foreach (var prop in properties)
        {

            var value = prop.GetValue(obj);
            if (value is string)
            {
                var decryptedValue = _dataProtector.Unprotect(value.ToString());
                prop.SetValue(decryptedObject, decryptedValue);
            }
            else if (value is IEnumerable<object> || value is IEnumerable<IEnumerable<object>>)
                Decrypt(value);

            else
                prop.SetValue(decryptedObject, value);

        }

        return decryptedObject;

    }
    public IEnumerable<object> Decrypt(IEnumerable<object> objects)
    {
        if (objects.Any())
            return objects.Select(Decrypt);
        return objects;
    }
    public IEnumerable<IEnumerable<object>> Decrypt(IEnumerable<IEnumerable<object>> objects)
    {
        if (objects.Any())
            return (IEnumerable<IEnumerable<object>>)objects.SelectMany(Decrypt);
        return objects;
    }
    public byte[] Encrypt<T>(T obj)
    {
        string serializedData = JsonConvert.SerializeObject(obj);
        byte[] serializedBytes = Encoding.UTF8.GetBytes(serializedData);

        return _dataProtector.Protect(serializedBytes);
    }
    public T Decrypt<T>(byte[] encryptedData)
    {
        byte[] decryptedBytes = _dataProtector.Unprotect(encryptedData);
        string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
        return JsonConvert.DeserializeObject<T>(decryptedData);
    }
}

//public Student Encrypt(Student student)
//{
//    return new Student
//    {
//        Id = student.Id,
//        Name = _dataProtector.Protect(student.Name),
//        Age = _dataProtector.Protect(student.Age),
//        Phone = _dataProtector.Protect(student.Phone)
//    };
//}

//public List<Student> Encrypt(List<Student> students) =>
//    students.Select(student => Encrypt(student)).ToList();

//public Student Decrypt(Student student)
//{
//    return new Student
//    {
//        Id = student.Id,
//        Name = _dataProtector.Unprotect(student.Name),
//        Age = _dataProtector.Unprotect(student.Age),
//        Phone = _dataProtector.Unprotect(student.Phone)
//    };
//}

//public IEnumerable<Student> Decrypt(List<Student> students) =>
//    students.Select(Decrypt);


