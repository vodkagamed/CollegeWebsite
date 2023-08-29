using Microsoft.AspNetCore.DataProtection;
using System.Reflection;

namespace SchoolWebsite.shared;
public class DataProtector
{
    private IDataProtector _dataProtector;

    public DataProtector(IDataProtectionProvider protectionProvider) => _dataProtector = protectionProvider.CreateProtector(GetType().FullName);
    public Student Encrypt(Student student)
    {
        return new Student
        {
            Id = student.Id,
            Name = _dataProtector.Protect(student.Name),
            Age = _dataProtector.Protect(student.Age),
            Phone = _dataProtector.Protect(student.Phone)
        };
    }
    public List<Student> Encrypt(List<Student> students) =>
        students.Select(student => Encrypt(student)).ToList();

    public object Encrypt(object obj)
    {
        Type objectType = obj.GetType();
        PropertyInfo[] properties = objectType.GetProperties();
        object encryptedObject = Activator.CreateInstance(objectType);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj).ToString();
            var encryptedValue = _dataProtector.Protect(value);
            prop.SetValue(encryptedObject, encryptedValue);
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
            var value = prop.GetValue(obj).ToString();
            var encryptedValue = _dataProtector.Unprotect(value);
            prop.SetValue(decryptedObject, encryptedValue);
        }

        return decryptedObject;
    }
    public IEnumerable<object> ListDecrypt(IEnumerable<object> objects) =>
        objects.Select(Decrypt);

    public Student Decrypt(Student student)
    {
        return new Student
        {
            Id = student.Id,
            Name = _dataProtector.Unprotect(student.Name),
            Age = _dataProtector.Unprotect(student.Age),
            Phone = _dataProtector.Unprotect(student.Phone)
        };
    }
    public IEnumerable<Student> Decrypt(List<Student> students) =>
        students.Select(Decrypt);
}

